package com.pb.pcrm.domain;

import java.util.List;

public class Recipe {
    public static class DirectionWithImage {
        private String direction;
        private byte[] imageData;

        public DirectionWithImage(String direction, byte[] imageData) {
            this.direction = direction;
            this.imageData = imageData;
        }

        public String getDirection() {
            return direction;
        }

        public byte[] getImageData() {
            return imageData;
        }
    }

    public static class Ingredient {
        private String quantity;
        private String ingredient;
        private boolean optional;

        public Ingredient(String quantity, String ingredient, boolean optional) {
            this.quantity = quantity;
            this.ingredient = ingredient;
            this.optional = optional;
        }

        public String getQuantity() {
            return quantity;
        }

        public String getIngredient() {
            return ingredient;
        }

        public boolean isOptional() {
            return optional;
        }

        @Override
        public String toString() {
            return quantity + " " + ingredient + (optional ? "optional" : "");
        }
    }

    private String title;
    private String meal;
    private String nutritionFacts;
    private byte[] imageData;
    private List<Ingredient> ingredients;
    private List<DirectionWithImage> directions;

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getMeal() {
        return meal;
    }

    public void setMeal(String meal) {
        this.meal = meal;
    }

    public String getNutritionFacts() {
        return nutritionFacts;
    }

    public void setNutritionFacts(String nutritionFacts) {
        this.nutritionFacts = nutritionFacts;
    }

    public byte[] getImageData() {
        return imageData;
    }

    public void setImageData(byte[] imageData) {
        this.imageData = imageData;
    }

    public List<Ingredient> getIngredients() {
        return ingredients;
    }

    public void setIngredients(List<Ingredient> ingredients) {
        this.ingredients = ingredients;
    }

    public List<DirectionWithImage> getDirections() {
        return directions;
    }

    public void setDirections(List<DirectionWithImage> directions) {
        this.directions = directions;
    }
}
