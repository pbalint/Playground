package arty.domain;

public class Artifact {
    private String groupId;
    private String artifactId;
    private String version;
    private String releaseDate;
    private String license;

    public Artifact(String groupId, String artifactId, String version, String releaseDate, String license) {
        this.groupId = groupId;
        this.artifactId = artifactId;
        this.version = version;
        this.releaseDate = releaseDate;
        this.license = license;
    }

    public String getGroupId() {
        return groupId;
    }

    public void setGroupId(String groupId) {
        this.groupId = groupId;
    }

    public String getArtifactId() {
        return artifactId;
    }

    public void setArtifactId(String artifactId) {
        this.artifactId = artifactId;
    }

    public String getVersion() {
        return version;
    }

    public void setVersion(String version) {
        this.version = version;
    }

    public String getReleaseDate() {
        return releaseDate;
    }

    public void setReleaseDate(String releaseDate) {
        this.releaseDate = releaseDate;
    }

    public String getLicense() {
        return license;
    }

    public void setLicense(String license) {
        this.license = license;
    }

    @Override
    public String toString() {
        return groupId + ":" + artifactId + (version != null ? ":" + version : "");
    }
}
