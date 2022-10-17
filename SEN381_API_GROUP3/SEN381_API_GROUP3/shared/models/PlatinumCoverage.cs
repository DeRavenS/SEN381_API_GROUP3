namespace SEN381_API_Group3.shared.models;

public class PlatinumCoverage : TreatmentCoverage
{
    public PlatinumCoverage(int coverageID, string coverageDescription, int numberOfGeneralVisits, int numberOfSpecialistsVisits, int totalCoverageUser) : base(coverageID, coverageDescription, numberOfGeneralVisits, numberOfSpecialistsVisits, totalCoverageUser)
    {
    }

}

