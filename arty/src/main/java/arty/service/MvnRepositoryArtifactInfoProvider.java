package arty.service;

import java.io.IOException;
import java.net.URI;
import java.net.URISyntaxException;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;
import java.net.http.HttpResponse.BodyHandlers;
import java.util.ArrayList;
import java.util.List;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;

import arty.domain.Artifact;

public class MvnRepositoryArtifactInfoProvider implements ArtifactInfoProvider {
    private static final HttpClient HTTP_CLIENT = HttpClient.newBuilder().build();

    private static final int SLEEP_AMOUNT = 3000;
    private static final String QUERY_PATTERN_ARTIFACT_SEARCH = "https://mvnrepository.com/search?q=%s";
    private static final String QUERY_PATTERN_ARTIFACT_DETAILS = "https://mvnrepository.com/artifact/%s/%s";

    @Override
    public List<Artifact> findArtifact(String targetArtifactId) {
        List<Artifact> artifacts = new ArrayList<>();
        String query = String.format(QUERY_PATTERN_ARTIFACT_SEARCH, targetArtifactId);
        try {
            HttpResponse<String> response = HTTP_CLIENT.send(HttpRequest.newBuilder().uri(new URI(query)).build(), BodyHandlers.ofString());
            if (response.statusCode() != 200) {
                throw new RuntimeException("HTTP response is not OK: " + response.statusCode());
            }
            Document doc = Jsoup.parse(response.body());
            Elements artifactNodes = doc.select("#maincontent>.im");
            for (Element artifactNode : artifactNodes) {
                Elements groupIdArtifactIdNode = artifactNode.select(".im-header>.im-subtitle");
                Elements links = groupIdArtifactIdNode.select("a");

                String groupId = links.get(0).text();
                String artifactId = links.get(1).text();

                if (artifactId.equals(targetArtifactId)) {
                    artifacts.add(new Artifact(groupId, targetArtifactId, null, null, null));
                }
            }
            Thread.sleep(SLEEP_AMOUNT);
        }
        catch (IOException | InterruptedException | URISyntaxException e) {
            throw new RuntimeException(e);
        }

        return artifacts;
    }

    @Override
    public void populateLicenseAndReleaseDate(Artifact artifact) {
        HttpClient httpClient = HttpClient.newBuilder().build();
        String query = String.format(QUERY_PATTERN_ARTIFACT_DETAILS, artifact.getGroupId(), artifact.getArtifactId());
        try {
            HttpResponse<String> response = httpClient.send(HttpRequest.newBuilder().uri(new URI(query)).build(), BodyHandlers.ofString());
            if (response.statusCode() != 200) {
                throw new RuntimeException("HTTP response is not OK: " + response.statusCode());
            }
            Document doc = Jsoup.parse(response.body());
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
            Thread.sleep(SLEEP_AMOUNT);
        }
        catch (IOException | InterruptedException | URISyntaxException e) {
            throw new RuntimeException(e);
        }
    }
}
