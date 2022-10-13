namespace SEN381_API_Group3.shared.models;

public class Treatment
{
    String treatmentID;
    String treatmentName;
    String treatmentDescription;
    List<MedicalServiceProvider> medicalServiceProviders;

    public Treatment(string treatmentID, string treatmentName, string treatmentDescription, List<MedicalServiceProvider> medicalServiceProviders)
    {
        this.treatmentID = treatmentID;
        this.treatmentName = treatmentName;
        this.treatmentDescription = treatmentDescription;
        this.medicalServiceProviders = medicalServiceProviders;
    }

    public string TreatmentID { get => treatmentID; set => treatmentID = value; }
    public string TreatmentName { get => treatmentName; set => treatmentName = value; }
    public string TreatmentDescription { get => treatmentDescription; set => treatmentDescription = value; }
    public List<MedicalServiceProvider> MedicalServiceProviders { get => medicalServiceProviders; set => medicalServiceProviders = value; }
}