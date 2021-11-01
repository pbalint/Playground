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
import arty.service.ArtifactInfoProvider;
import arty.service.MvnRepositoryArtifactInfoProvider;
import arty.service.NpmJsArtifactInfoProvider;

public class Main {

    public static void main(String[] args) throws IOException, InterruptedException, URISyntaxException {
        String[] artifacts = {
                " spring-amqp , 1.3.6.RELEASE",
                " spring-amqp ,1.5.7.RELEASE",
                " spring-amqp , 1.7.3.RELEASE",
                " spring-aop , 3.0.5.RELEASE",
                " spring-aop , 3.1.2.RELEASE",
                " spring-aop , 3.1.4.RELEASE",
                " spring-aop , 4.3.7.RELEASE",
                " spring-asm , 3.1.2.RELEASE",
                " spring-asm , 3.1.4.RELEASE",
                " spring-beans , 3.0.5.RELEASE",
                " spring-beans , 3.1.1.RELEASE",
                " spring-beans , 3.1.2.RELEASE",
                " spring-beans , 3.1.4.RELEASE",
                " spring-beans , 4.3.10.RELEASE",
                " spring-beans , 4.3.6.RELEASE",
                " spring-beans , 4.3.7.RELEASE",
                " spring-beans , 5.1.6.RELEASE",
                " spring-boot , 1.5.2.RELEASE",
                " spring-boot , 1.5.6.RELEASE",
                " spring-boot , 2.1.4.RELEASE",
                " spring-boot-autoconfigure , 1.5.2.RELEASE",
                " spring-boot-autoconfigure , 1.5.6.RELEASE",
                " spring-boot-autoconfigure , 2.1.4.RELEASE",
                " spring-boot-configuration-processor , 1.5.6.RELEASE",
                " spring-boot-starter , 1.5.2.RELEASE",
                " spring-boot-starter-actuator , 1.5.6.RELEASE",
                " spring-boot-starter-actuator , 2.1.4.RELEASE",
                " spring-boot-starter-amqp , 1.5.6.RELEASE",
                " spring-boot-starter-aop , 2.1.4.RELEASE",
                " spring-boot-starter-cache , 1.5.6.RELEASE",
                " spring-boot-starter-data-jpa , 1.5.6.RELEASE",
                " spring-boot-starter-data-jpa , 2.1.4.RELEASE",
                " spring-boot-starter-hateoas , 1.5.6.RELEASE",
                " spring-boot-starter-integration , 1.5.2.RELEASE",
                " spring-boot-starter-jdbc , 1.5.6.RELEASE",
                " spring-boot-starter-jdbc , 2.1.4.RELEASE",
                " spring-boot-starter-jersey , 1.5.6.RELEASE",
                " spring-boot-starter-log4j2 ,1.5.2.RELEASE",
                " spring-boot-starter-security , 2.1.4.RELEASE",
                " spring-boot-starter-web ,1.5.2.RELEASE",
                " spring-boot-starter-web ,1.5.6.RELEASE",
                " spring-boot-starter-webflux , 2.1.4.RELEASE",
                " spring-cloud-aws-messaging ,1.2.1.RELEASE",
                " spring-cloud-starter-oauth2 , 1.1.3.RELEASE",
                " spring-cloud-starter-sleuth , 2.1.1.RELEASE",
                " spring-context , 3.0.5.RELEASE",
                " spring-context , 3.1.1.RELEASE",
                " spring-context , 3.1.2.RELEASE",
                " spring-context , 3.1.4.RELEASE",
                " spring-context , 4.3.10.RELEASE",
                " spring-context , 4.3.6.RELEASE",
                " spring-context , 4.3.7.RELEASE",
                " spring-context , 5.1.6.RELEASE",
                " spring-context-support , 3.1.2.RELEASE",
                " spring-context-support , 4.3.7.RELEASE",
                " spring-core , 3.0.5.RELEASE",
                " spring-core , 3.1.1.RELEASE",
                " spring-core , 3.1.2.RELEASE",
                " spring-core , 3.1.4.RELEASE",
                " spring-core , 4.3.10.RELEASE",
                " spring-core , 4.3.7.RELEASE",
                " spring-core , 5.1.6.RELEASE",
                " spring-data-commons , 1.12.7.RELEASE",
                " spring-data-commons , 1.13.6.RELEASE",
                " spring-data-commons , 2.1.6.RELEASE",
                " spring-data-jpa , 1.10.7.RELEASE",
                " spring-data-jpa , 1.11.6.RELEASE",
                " spring-data-jpa , 2.1.6.RELEASE",
                " spring-expression , 3.1.2.RELEASE",
                " spring-hateoas ,0.23.0.RELEASE",
                " spring-hateoas , 0.24.0.RELEASE",
                " spring-integration-core , 4.3.8.RELEASE",
                " spring-integration-file , 4.3.8.RELEASE",
                " spring-integration-sftp , 4.3.8.RELEASE",
                " spring-jdbc , 3.0.5.RELEASE",
                " spring-jdbc , 3.1.2.RELEASE",
                " spring-jdbc , 3.1.4.RELEASE",
                " spring-jdbc , 4.3.10.RELEASE",
                " spring-jdbc , 4.3.10.RELEASE",
                " spring-jdbc , 4.3.6.RELEASE",
                " spring-jms ,3.0.5.RELEASE",
                " spring-jms , 3.0.7.RELEASE",
                " spring-jms , 3.1.2.RELEASE",
                " spring-jms , 3.1.4.RELEASE",
                " spring-messaging , 4.2.9.RELEASE",
                " spring-messaging , 4.3.7.RELEASE",
                " spring-orm , 3.1.2.RELEASE",
                " spring-orm , 4.3.10.RELEASE",
                " spring-orm , 4.3.6.RELEASE",
                " spring-osgi-core ,1.2.1",
                " spring-rabbit ,1.3.6.RELEASE",
                " spring-rabbit, 1.5.7.RELEASE",
                " spring-rabbit , 1.7.3.RELEASE",
                " spring-retry ,1.0.3.RELEASE",
                " spring-retry , 1.1.2.RELEASE",
                " spring-retry , 1.2.0.RELEASE",
                " spring-security-config , 5.1.5.RELEASE",
                " spring-security-oauth2-autoconfigure , 2.1.2.RELEASE",
                " spring-security-oauth2-client , 5.1.5.RELEASE",
                " spring-security-oauth2-core , 5.1.5.RELEASE",
                " spring-security-oauth2-jose ,5.1.5.RELEASE",
                " spring-security-oauth2-resource-server , 5.1.5.RELEASE",
                " spring-security-web , 5.1.5.RELEASE",
                " spring-test , 3.1.2.RELEASE",
                " spring-test , 3.1.4.RELEASE",
                " spring-tx , 3.0.5.RELEASE",
                " spring-tx , 3.1.2.RELEASE",
                " spring-tx , 3.1.4.RELEASE",
                " spring-tx , 4.2.9.RELEASE",
                " spring-tx , 4.3.10.RELEASE",
                " spring-tx , 4.3.6.RELEASE",
                " spring-tx ,4.3.7.RELEASE",
                " spring-web , 3.0.6.RELEASE",
                " spring-web ,3.1.1.RELEASE",
                " spring-web , 3.1.2.RELEASE",
                " spring-web , 3.1.4.RELEASE",
                " spring-web , 4.2.9.RELEASE",
                " spring-web , 4.3.10.RELEASE",
                " spring-web , 4.3.6.RELEASE",
                " spring-web , 4.3.7.RELEASE",
                " spring-web , 5.1.6.RELEASE",
                " sqljdbc4 ,2.0",
                " sqljdbc4 , 3.0",
                " sqljdbc4 , 4.0",
                " sshd-sftp , 2.3.0",
                " sshj ,0.27.0",
                " stax-api , 1.0.1",
                " swagger-annotations , 1.5.16",
                " swagger-annotations , 2.1.1",
                " tap , 4.4",
                " tika-core , 1.6",
                " tomcat-annotations-api ,7.0.28",
                " tomcat-embed-core, 8.5.11",
                " tomcat-embed-core , 8.5.16",
                " tslib , 1.11.2",
                " twilio , 3.67.0",
                " twilio ,8.12.0",
                " validation-api ,1.1.0.Final",
                " validation-api , 2.0.1.Final",
                " web-animations-js , 2.3.2",
                " winston , 3.3.3",
                " xml-apis ,1.3.02",
                " xml-apis ,1.4.01",
                " xmlParserAPIs , 2.6.2",
                " xmltooling , 1.3.2-1",
                " xstream ,1.3"
        };
        
        NpmJsArtifactInfoProvider npmJsArtifactInfoProvider = new NpmJsArtifactInfoProvider();
        ArtifactInfoProvider mavenInfoProvider = new MvnRepositoryArtifactInfoProvider();
        List<Artifact> ambiguousArtifacts = new ArrayList<>();
        for (String artifactEntry : artifacts) {
            String[] entryParts = artifactEntry.split(",");
            String targetArtifactId = entryParts[0].trim();
            String targetVersion = entryParts[1].trim();
            
            Artifact artifact;
            /*Artifact artifact = npmJsArtifactInfoProvider.findArtifact(targetArtifactId, targetVersion);
            if (artifact != null) {
                System.out.printf("%s %s %s %s %s\n", artifact.getGroupId(), artifact.getArtifactId(), artifact.getVersion(), artifact.getLicense(), artifact.getReleaseDate());
            }
            else {*/
                List<Artifact> potentialArtifacts = mavenInfoProvider.findArtifact(targetArtifactId);
                if (potentialArtifacts.size() > 1) {
                    ambiguousArtifacts.addAll(potentialArtifacts);
                }
                else if (potentialArtifacts.size() == 1){
                    artifact = potentialArtifacts.get(0);
                    artifact.setVersion(targetVersion);
                    mavenInfoProvider.populateLicenseAndReleaseDate(artifact);
                    System.out.printf("%s %s %s %s %s\n", artifact.getGroupId(), artifact.getArtifactId(), artifact.getVersion(), artifact.getLicense(), artifact.getReleaseDate());
                }
                else {
                    System.out.println("No match for: " + targetArtifactId);
                }
            //}
        }
        
        for (Artifact ambiguousArtifact : ambiguousArtifacts) {
            System.out.println(ambiguousArtifact);
        }
    }

}
