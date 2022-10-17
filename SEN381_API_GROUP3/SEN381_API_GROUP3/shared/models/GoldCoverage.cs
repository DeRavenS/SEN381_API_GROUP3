namespace SEN381_API_Group3.shared.models;

public class GoldCoverage : TreatmentCoverage
{
    public GoldCoverage(int coverageID, string coverageDescription, int numberOfGeneralVisits, int numberOfSpecialistsVisits, int totalCoverageUser) : base(coverageID, coverageDescription, numberOfGeneralVisits, numberOfSpecialistsVisits, totalCoverageUser)
    {
    }

}