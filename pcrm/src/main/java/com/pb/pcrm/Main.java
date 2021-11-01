package com.pb.pcrm;

import java.io.InputStream;
import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpRequest.BodyPublishers;
import java.net.http.HttpResponse;
import java.net.http.HttpResponse.BodyHandlers;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.StandardCopyOption;
import java.nio.file.StandardOpenOption;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;

import org.apache.poi.hssf.usermodel.HSSFClientAnchor;
import org.apache.poi.hssf.usermodel.HSSFWorkbook;
import org.apache.poi.hssf.util.HSSFColor.HSSFColorPredefined;
import org.apache.poi.ss.usermodel.Cell;
import org.apache.poi.ss.usermodel.CellStyle;
import org.apache.poi.ss.usermodel.ClientAnchor;
import org.apache.poi.ss.usermodel.ClientAnchor.AnchorType;
import org.apache.poi.ss.usermodel.Drawing;
import org.apache.poi.ss.usermodel.FillPatternType;
import org.apache.poi.ss.usermodel.Font;
import org.apache.poi.ss.usermodel.IndexedColors;
import org.apache.poi.ss.usermodel.Picture;
import org.apache.poi.ss.usermodel.Row;
import org.apache.poi.ss.usermodel.Sheet;
import org.apache.poi.ss.usermodel.Workbook;
import org.apache.poi.ss.util.SheetUtil;
import org.apache.poi.util.Units;

import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.pb.pcrm.domain.Recipe;
import com.pb.pcrm.domain.Recipe.DirectionWithImage;
import com.pb.pcrm.domain.Recipe.Ingredient;
import com.pb.pcrm.domain.RecipeId;

public class Main {
    private static final String URL = "https://www.pcrm.org/graphql";

    private static final HttpClient HTTP_CLIENT = HttpClient.newBuilder().build();
    private static final ObjectMapper OBJECT_MAPPER = new ObjectMapper();

    public static void main(String[] args) throws Exception {
        List<RecipeId> recipeIds = getRecipeList();
        Map<String, List<Recipe>> recipes = new HashMap<>();
        int recipeIndex = 0;
        for (RecipeId recipeId : recipeIds) {
            System.out.println("Downloading recipe: " + recipeIndex++ + "/" + recipeIds.size());

            String requestBody = String.format(Queries.REQUEST_DISH_PATTERN, recipeId.getId());
            HttpRequest request = HttpRequest.newBuilder(new URI(URL)).POST(BodyPublishers.ofString(requestBody)).build();
            HttpResponse<String> response = HTTP_CLIENT.send(request, BodyHandlers.ofString());
            if (response.statusCode() != 200) {
                throw new RuntimeException("HTTP response is not OK: " + response.statusCode());
            }
            
            JsonNode content = OBJECT_MAPPER.readTree(response.body());
            JsonNode rootNode = content.get("data").get("nodeById").get("entityTranslation");
            Recipe recipe = new Recipe();
            recipe.setTitle(getNodeValueAsText(rootNode, "title"));
            String mealName = getNodeValueAsText(rootNode, "fieldRecipeMeal");
            if (mealName.isBlank()) {
                mealName = "Unknown";
            }
            recipe.setMeal(mealName);
            
            recipe.setNutritionFacts(cleanHtmlTags(getNodeValueAsText(rootNode, "fieldRecipeNutritionFacts", "processed")));
            recipe.setIngredients(getIngredients(rootNode.get("fieldRecipeIngredients")));
            String imageUrl = rootNode.get("fieldFeaturedImage").get("entity").get("fieldMediaImage").get("derivative").get("url").asText();
            recipe.setImageData(getContent(imageUrl));
            recipe.setDirections(getDirections(rootNode.get("fieldRecipeDirections")));
            
            List<Recipe> recipesForMeal = recipes.computeIfAbsent(recipe.getMeal(), meal -> new ArrayList<>());
            recipesForMeal.add(recipe);
        }

        Map<String, CellStyle> cellStyles = new HashMap<>();
        Map<String, Font> fontStyles = new HashMap<>();
        try (Workbook workbook = new HSSFWorkbook()) {
            setupFontStyles(workbook, fontStyles);
            setupCellStyles(workbook, cellStyles, fontStyles);
            recipeIndex = 0;
            for (Entry<String, List<Recipe>> recipeEntry : recipes.entrySet()) {
                int longestIngredientList = 0;
                String meal = recipeEntry.getKey();
                List<Recipe> recipeList = recipeEntry.getValue();
                for (int i = 0; i < recipeList.size(); i++) {
                    if (recipeList.get(i).getIngredients().size() > longestIngredientList) {
                        longestIngredientList = recipeList.get(i).getIngredients().size();
                    }
                }
                for (int i = 0; i < recipeList.size(); i++) {
                    System.out.print("Writing recipe: " + recipeIndex++);
                    Recipe recipe = recipeList.get(i);
                    writeRecipe(workbook, cellStyles, meal, recipe, i, longestIngredientList);
                    System.out.println(" Done.");
                }
            }
            workbook.write(Files.newOutputStream(Path.of("recipes.xls"), StandardOpenOption.CREATE, StandardOpenOption.TRUNCATE_EXISTING));
        }
    }

    private static String getNodeValueAsText(JsonNode node, String... fields) {
        String value = "";
        for (String field : fields) {
            node = node.get(field);
            if (node.isNull()) {
                return value;
            }
        }
        value = node.asText();
        return value;
    }

    private static void setupFontStyles(Workbook workbook, Map<String, Font> fontStyles) {
        Font defaultFont = workbook.createFont();
        defaultFont.setFontHeightInPoints((short)10);
        defaultFont.setFontName("Arial");
        defaultFont.setColor(IndexedColors.BLACK.getIndex());
        defaultFont.setBold(false);
        defaultFont.setItalic(false);
        fontStyles.put("default", defaultFont);

        Font titleFont = workbook.createFont();
        titleFont.setFontHeightInPoints((short)14);
        titleFont.setFontName("Arial");
        titleFont.setColor(IndexedColors.BLACK.getIndex());
        titleFont.setBold(true);
        titleFont.setItalic(false);
        fontStyles.put("title", titleFont);
    }

    private static void setupCellStyles(Workbook workbook, Map<String, CellStyle> cellStyles, Map<String, Font> fontStyles) {
        CellStyle wordWrapCellStyle = workbook.createCellStyle();
        wordWrapCellStyle.setWrapText(true);
        cellStyles.put("wordWrap", wordWrapCellStyle);
        
        CellStyle titleCellStyle = workbook.createCellStyle();
        titleCellStyle.setFillForegroundColor(HSSFColorPredefined.LIGHT_CORNFLOWER_BLUE.getIndex());
        titleCellStyle.setFont(fontStyles.get("title"));
        titleCellStyle.setFillPattern(FillPatternType.SOLID_FOREGROUND);
        cellStyles.put("title", titleCellStyle);
    }

    private static void writeRecipe(Workbook workbook,
                                    Map<String, CellStyle> cellStyles,
                                    String meal,
                                    Recipe recipe,
                                    int index,
                                    int longestIngredientList) {
        Sheet sheet = workbook.getSheet(meal);
        if (sheet == null) {
            sheet = workbook.createSheet(meal);
        }
        int rowIndex = 0;
        Cell cell = getCell(sheet, rowIndex++, index);
        cell.setCellValue(recipe.getTitle());
        cell.setCellStyle(cellStyles.get("title"));
        
        cell = getCell(sheet, rowIndex++, index);
        addImage(cell, recipe.getImageData());

        rowIndex++;
        cell = getCell(sheet, rowIndex++, index);
        cell.setCellValue("Ingredients");
        cell.setCellStyle(cellStyles.get("title"));
        
        for (Ingredient ingredient : recipe.getIngredients()) {
            cell = getCell(sheet, rowIndex++, index);
            cell.setCellValue(ingredient.toString());
        }

        rowIndex += longestIngredientList - recipe.getIngredients().size() + 1;
        cell = getCell(sheet, rowIndex++, index);
        cell.setCellValue("Directions");
        cell.setCellStyle(cellStyles.get("title"));
        
        for (DirectionWithImage direction : recipe.getDirections()) {
            cell = getCell(sheet, rowIndex++, index);
            cell.setCellValue(direction.getDirection());
            cell.setCellStyle(cellStyles.get("wordWrap"));
            if (direction.getImageData() != null) {
                cell = getCell(sheet, rowIndex++, index);
                addImage(cell, direction.getImageData());
            }
        }
        
        rowIndex++;
        cell = getCell(sheet, rowIndex++, index);
        cell.setCellValue("Nutrition facts");
        cell.setCellStyle(cellStyles.get("title"));

        cell = getCell(sheet,  rowIndex++, index);
        cell.setCellValue(recipe.getNutritionFacts().toString());
        cell.setCellStyle(cellStyles.get("wordWrap"));
    }

    private static void addImage(Cell cell, byte[] imageData) {
        if (imageData == null) return;

        Sheet sheet = cell.getSheet();
        Drawing<?> drawing = sheet.getDrawingPatriarch();
        if (drawing == null) {
            drawing = sheet.createDrawingPatriarch();
        }
        ClientAnchor anchor = new HSSFClientAnchor();
        anchor.setAnchorType(AnchorType.MOVE_AND_RESIZE);
        anchor.setCol1(cell.getColumnIndex());
        anchor.setRow1(cell.getRowIndex());
        anchor.setCol2(cell.getColumnIndex() + 1);
        anchor.setRow2(cell.getRowIndex() + 1);
        Picture picture = drawing.createPicture(anchor, sheet.getWorkbook().addPicture(imageData, Workbook.PICTURE_TYPE_JPEG));

        double pictureWidth = picture.getImageDimension().getWidth();
        double pictureHeight = picture.getImageDimension().getHeight();
        cell.getRow().setHeightInPoints((float)Units.pixelToPoints(pictureHeight));
        sheet.setColumnWidth(cell.getColumnIndex(), (int)(256 * pictureWidth / (2 + SheetUtil.getDefaultCharWidth(sheet.getWorkbook()))));
    }

    private static Cell getCell(Sheet sheet, int rowIndex, int index) {
        Row row = getRow(sheet, rowIndex);
        return getCell(row, index);
    }

    private static Row getRow(Sheet sheet, int rowIndex) {
        Row row = sheet.getRow(rowIndex);
        if (row == null) {
            row = sheet.createRow(rowIndex);
        }
        return row;
    }

    private static Cell getCell(Row row, int columnIndex) {
        Cell cell = row.getCell(columnIndex);
        if (cell == null) {
            cell = row.createCell(columnIndex);
        }
        return cell;
    }

    private static String cleanHtmlTags(String text) {
        return text.replaceAll("</?[a-zA-Z ]+/?>", "");
    }

    private static List<RecipeId> getRecipeList() throws Exception {
        List<RecipeId> recipeIds = new ArrayList<>();
        HttpRequest request = HttpRequest.newBuilder(new URI(URL)).POST(BodyPublishers.ofString(Queries.REQUEST_DISH_ID_LIST)).build();
        HttpResponse<String> response = HTTP_CLIENT.send(request, BodyHandlers.ofString());
        if (response.statusCode() != 200) {
            throw new RuntimeException("HTTP response is not OK: " + response.statusCode());
        }
        
        JsonNode content = OBJECT_MAPPER.readTree(response.body());
        JsonNode entitiesNode = content.get("data").get("nodeQuery").get("entities");
        for (int i = 0; i < entitiesNode.size(); i++) {
            JsonNode entity = entitiesNode.get(i).get("entityTranslation");
            int id = entity.get("nid").asInt();
            String title = entity.get("title").asText();
            recipeIds.add(new RecipeId(id, title));
        }
        return recipeIds;
    }

    private static List<DirectionWithImage> getDirections(JsonNode directionsNode) throws Exception {
        List<DirectionWithImage> directions = new ArrayList<>();
        for (int i = 0; i < directionsNode.size(); i++) {
            JsonNode directionNode = directionsNode.get(i).get("entity").get("entityTranslation");
            JsonNode imageNode = directionNode.get("fieldDirectionImage");
            String directionText = cleanHtmlTags(directionNode.get("fieldDirectionDescription").get("processed").asText());
            byte[] directionImage;
            if (!imageNode.isNull()) {
                String directionImageUrl = imageNode.get("entity").get("fieldMediaImage").get("derivative").get("url").asText();
                directionImage = getContent(directionImageUrl);
            }
            else {
                directionImage = null;
            }
            directions.add(new DirectionWithImage(directionText, directionImage));
       }
        return directions;
    }

    private static List<Ingredient> getIngredients(JsonNode ingredientsNode) {
        List<Ingredient> ingredients = new ArrayList<>();
        for (int i = 0; i < ingredientsNode.size(); i++) {
            JsonNode ingredientNode = ingredientsNode.get(i).get("entity").get("entityTranslation"); 
            ingredients.add(new Ingredient(ingredientNode.get("fieldRecipeIngredientQuantity").asText(),
                                           ingredientNode.get("fieldRecipeIngredientName").asText(),
                                           ingredientNode.get("fieldRecipeIngredientOptional").asBoolean()));
        }
        
        return ingredients;
    }

    private static byte[] getContent(String imageUrl) throws Exception {
        HttpRequest request = HttpRequest.newBuilder(new URI(imageUrl)).build();
        HttpResponse<byte[]> response = HTTP_CLIENT.send(request, BodyHandlers.ofByteArray());
        if (response.statusCode() != 200) {
            System.out.println("Unable to download: " + imageUrl);
        }
        return response.body();
    }
    
    private static void downloadTo(String imageUrl, Path dirPath, String filePrefix) throws Exception {
        int beginIndex = imageUrl.lastIndexOf('/') + 1;
        int endIndex = imageUrl.indexOf('?');
        if (endIndex < 0) {
            endIndex = imageUrl.length();
        }
        String fileName = imageUrl.substring(beginIndex, endIndex);
        if (filePrefix != null) {
            fileName = filePrefix + fileName;
        }
        HttpRequest request = HttpRequest.newBuilder(new URI(imageUrl)).build();
        HttpResponse<InputStream> response = HTTP_CLIENT.send(request, BodyHandlers.ofInputStream());
        if (response.statusCode() != 200) {
            System.out.println("Unable to download: " + imageUrl);
        }
        Files.copy(response.body(), dirPath.resolve(fileName), StandardCopyOption.REPLACE_EXISTING);
    }
}
