namespace SEN381_API_Group3.shared.models;

public class Treatment
{
    string treatmentID;
    string treatmentName;
    string treatmentDescription;
    string medicalServiceProviders;

    public Treatment(string treatmentID, string treatmentName, string treatmentDescription, string medicalServiceProviders)
    {
        this.treatmentID = treatmentID;
        this.treatmentName = treatmentName;
        this.treatmentDescription = treatmentDescription;
        this.medicalServiceProviders = medicalServiceProviders;
    }

    public string TreatmentID { get => treatmentID; set => treatmentID = value; }
    public string TreatmentName { get => treatmentName; set => treatmentName = value; }
    public string TreatmentDescription { get => treatmentDescription; set => treatmentDescription = value; }
    public string MedicalServiceProviders { get => medicalServiceProviders; set => medicalServiceProviders = value; }
}