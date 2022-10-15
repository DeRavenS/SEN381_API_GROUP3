namespace SEN381_API_Group3.shared.models;

public abstract class TreatmentCoverage
{
    int coverageID;
    string coverageDescription;
    int numberOfGeneralVisits;
    int numberOfSpecialistsVisits;
    int totalCoverageUser;



    public TreatmentCoverage(int coverageID, string coverageDescription, int numberOfGeneralVisits, int numberOfSpecialistsVisits, int totalCoverageUser)
    {
        this.coverageID = coverageID;
        this.coverageDescription = coverageDescription;
        this.numberOfGeneralVisits = numberOfGeneralVisits;
        this.numberOfSpecialistsVisits = numberOfSpecialistsVisits;
        this.totalCoverageUser = totalCoverageUser;
    }

    public TreatmentCoverage()
    {
    }


    public int CoverageID { get => coverageID; set => coverageID = value; }
    public string CoverageDescription { get => coverageDescription; set => coverageDescription = value; }
    public int NumberOfGeneralVisits { get => numberOfGeneralVisits; set => numberOfGeneralVisits = value; }
    public int NumberOfSpecialistsVisits { get => numberOfSpecialistsVisits; set => numberOfSpecialistsVisits = value; }
    public int TotalCoverageUser { get => totalCoverageUser; set => totalCoverageUser = value; }



}