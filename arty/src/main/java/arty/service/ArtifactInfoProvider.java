package arty.service;

import java.util.List;

import arty.domain.Artifact;

public interface ArtifactInfoProvider {

    List<Artifact> findArtifact(String targetArtifactId);

    void populateLicenseAndReleaseDate(Artifact artifact);

}