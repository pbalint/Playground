package arty;

import java.io.IOException;
import java.net.URI;
import java.net.URISyntaxException;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse.BodyHandlers;
import java.util.ArrayList;
import java.util.List;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;

import arty.domain.Artifact;

public class Main {
    private static final String QUERY_PATTERN_ARTIFACT_SEARCH = "https://mvnrepository.com/search?q=%s";
    private static final String QUERY_PATTERN_ARTIFACT_DETAILS = "https://mvnrepository.com/artifact/%s/%s";

    public static void main(String[] args) throws IOException, InterruptedException, URISyntaxException {
        String targetArtifactId = "commons-lang3";
        String targetVersion = "3.8.1";

        List<Artifact> artifacts = findArtifact(targetArtifactId);
        for (Artifact artifact : artifacts) {
            // If artifact is found for multiple groupIds, decide which one to use...
            artifact.setVersion(targetVersion);
            populateLicenseAndReleaseDate(artifact);
            System.out.printf("%s %s %s %s %s\n", artifact.getGroupId(), artifact.getArtifactId(), artifact.getVersion(), artifact.getLicense(), artifact.getReleaseDate());
        }
    }

    private static void populateLicenseAndReleaseDate(Artifact artifact) {
        HttpClient httpClient = HttpClient.newBuilder().build();
        String query = String.format(QUERY_PATTERN_ARTIFACT_DETAILS, artifact.getGroupId(), artifact.getArtifactId());
        String response;
        try {
            response = httpClient.send(HttpRequest.newBuilder().uri(new URI(query)).build(), BodyHandlers.ofString()).body();
            Document doc = Jsoup.parse(response);
            String license = doc.select("#maincontent span.lic").text();
            artifact.setLicense(license);
            
            Elements detailNodes = doc.select("#maincontent .versions tbody tr");
            for (Element detailNode : detailNodes) {
                Elements columnNodes = detailNode.select("td");
                String version = columnNodes.get((columnNodes.size() - 1)  - 3).text();
                String releaseDate = columnNodes.get((columnNodes.size()-1)).text();
                if (version.equals(artifact.getVersion())) {
                    artifact.setVersion(version);
                    artifact.setReleaseDate(releaseDate);
                    break;
                }
            }
        }
        catch (IOException | InterruptedException | URISyntaxException e) {
            throw new RuntimeException(e);
        }

    }

    private static List<Artifact> findArtifact(String targetArtifactId) {
        List<Artifact> artifacts = new ArrayList<>();
        HttpClient httpClient = HttpClient.newBuilder().build();
        String query = String.format(QUERY_PATTERN_ARTIFACT_SEARCH, targetArtifactId);
        String response;
        try {
            response = httpClient.send(HttpRequest.newBuilder().uri(new URI(query)).build(), BodyHandlers.ofString()).body();
            Document doc = Jsoup.parse(response);
            Elements artifactNodes = doc.select("#maincontent>.im");
            for (Element artifactNode : artifactNodes) {
                Elements groupIdArtifactIdNode = artifactNode.select(".im-header>.im-subtitle");
                Elements links = groupIdArtifactIdNode.select("a");

                String groupId = links.get(0).text();
                String artifactId = links.get(1).text();

                if (!artifactId.equals(targetArtifactId)) {
                    break;
                }
                artifacts.add(new Artifact(groupId, targetArtifactId, null, null, null));
            }
        }
        catch (IOException | InterruptedException | URISyntaxException e) {
            throw new RuntimeException(e);
        }

        return artifacts;
    }

}
