namespace SEN381_API_Group3.shared.models;

public class PlatinumCoverage : TreatmentCoverage
{

    public PlatinumCoverage()
    {
        this.CoverageID = "";
        this.CoverageDescription = "";
        this.NumberOfGeneralVisits = 0;
        this.NumberOfSpecialistsVisits = 0;//Default values to be defined later
    }
    
}

