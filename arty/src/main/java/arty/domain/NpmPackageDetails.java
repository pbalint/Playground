package arty.domain;

import java.util.Map;

public class NpmPackageDetails {
    private String name;
    private String scope;
    private String license;
    private Map<String, String> time;

    public String getName() {
        return name;
    }
    
    public void setName(String name) {
        this.name = name;
    }
    
    public String getLicense() {
        return license;
    }
    
    public void setLicense(String license) {
        this.license = license;
    }
    
    public Map<String, String> getTime() {
        return time;
    }
    
    public void setTime(Map<String, String> time) {
        this.time = time;
    }

    public String getScope() {
        return scope;
    }

    public void setScope(String scope) {
        this.scope = scope;
    }
    
}
