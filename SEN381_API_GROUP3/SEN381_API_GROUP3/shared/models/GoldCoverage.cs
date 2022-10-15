namespace SEN381_API_Group3.shared.models;

public class GoldCoverage : TreatmentCoverage
{
    int coverageID;
    string coverageDescription;
    int numberOfGeneralVisits;
    int numberOfSpecialistsVisits;
    int totalCoverageUser;
    public GoldCoverage(int coverageID, string coverageDescription, int numberOfGeneralVisits, int numberOfSpecialistsVisits, int totalCoverageUser) : base(coverageID, coverageDescription, numberOfGeneralVisits, numberOfSpecialistsVisits, totalCoverageUser)
    {
        this.CoverageID1 = coverageID;
        this.CoverageDescription1 = coverageDescription;
        this.NumberOfGeneralVisits1 = numberOfGeneralVisits;   
        this.NumberOfSpecialistsVisits1 = numberOfSpecialistsVisits;
        this.TotalCoverageUser1 = totalCoverageUser;
    }

    public int CoverageID1 { get => coverageID; set => coverageID = value; }
    public string CoverageDescription1 { get => coverageDescription; set => coverageDescription = value; }
    public int NumberOfGeneralVisits1 { get => numberOfGeneralVisits; set => numberOfGeneralVisits = value; }
    public int NumberOfSpecialistsVisits1 { get => numberOfSpecialistsVisits; set => numberOfSpecialistsVisits = value; }
    public int TotalCoverageUser1 { get => totalCoverageUser; set => totalCoverageUser = value; }
}