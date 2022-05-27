package arty.service;

import java.io.IOException;
import java.net.URI;
import java.net.URISyntaxException;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;
import java.net.http.HttpResponse.BodyHandlers;

import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.ObjectMapper;

import arty.domain.Artifact;
import arty.domain.NpmPackageDetails;

public class NpmJsArtifactInfoProvider {
    private static final HttpClient HTTP_CLIENT = HttpClient.newBuilder().build();

    //private static final String QUERY_PATTERN_ARTIFACT_SEARCH = "http://registry.npmjs.com/-/v1/search?text=%s&from=0&size=100";
    private static final String QUERY_PATTERN_ARTIFACT_DETAILS = "http://registry.npmjs.com/%s";
    private static final ObjectMapper OBJECT_MAPPER = new ObjectMapper();
    
    static {
        OBJECT_MAPPER.configure(DeserializationFeature.FAIL_ON_UNKNOWN_PROPERTIES, false);
    }

    public Artifact findArtifact(String targetArtifactId, String targetVersion) {
        Artifact artifact = null;
        String query = String.format(QUERY_PATTERN_ARTIFACT_DETAILS, targetArtifactId);
        try {
            HttpResponse<String> response = HTTP_CLIENT.send(HttpRequest.newBuilder().uri(new URI(query)).build(), BodyHandlers.ofString());
            if (response.statusCode() == 200) {
                NpmPackageDetails packageDetails = OBJECT_MAPPER.readValue(response.body(), NpmPackageDetails.class);
                String packageName = packageDetails.getName();
                if (packageDetails.getScope() == null && packageName.startsWith("@") && packageName.contains("/")) {
                    packageDetails.setScope(packageName.substring(0, packageName.indexOf('/')));
                }
                if (packageDetails.getTime().containsKey(targetVersion)) {
                    artifact = new Artifact(packageDetails.getScope(), packageDetails.getName(), targetVersion, packageDetails.getTime().get(targetVersion), packageDetails.getLicense());
                }
            }
        }
        catch (IOException | InterruptedException | URISyntaxException e) {
            e.printStackTrace();
        }

        return artifact;
    }

}
