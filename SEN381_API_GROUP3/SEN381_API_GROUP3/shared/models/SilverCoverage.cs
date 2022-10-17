namespace SEN381_API_Group3.shared.models;

public class SilverCoverage : TreatmentCoverage
{
    public SilverCoverage(int coverageID, string coverageDescription, int numberOfGeneralVisits, int numberOfSpecialistsVisits, int totalCoverageUser) : base(coverageID, coverageDescription, numberOfGeneralVisits, numberOfSpecialistsVisits, totalCoverageUser)
    {
    }

}