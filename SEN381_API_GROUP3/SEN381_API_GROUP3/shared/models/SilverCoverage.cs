namespace SEN381_API_Group3.shared.models;

public class SilverCoverage : TreatmentCoverage
{
    public SilverCoverage()
    {
        this.CoverageID = "";
        this.CoverageDescription = "";
        this.NumberOfGeneralVisits = 0;
        this.NumberOfSpecialistsVisits = 0;//Default values to be defined later
    }

    
}