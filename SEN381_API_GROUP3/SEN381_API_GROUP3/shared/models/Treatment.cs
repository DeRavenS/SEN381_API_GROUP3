using SEN381_API_GROUP3.shared.models;

namespace SEN381_API_Group3.shared.models;

public class Treatment
{
    string treatmentID;
    string treatmentName;
    string treatmentDescription;
    List<MedicalServiceProvider> medicalServiceProviders;
    List<MedicalServiceProviderTreatment> medicalServiceProviderTreatments;

    public Treatment(string treatmentID, string treatmentName, string treatmentDescription, List<MedicalServiceProvider> medicalServiceProviders)
    {
        this.treatmentID = treatmentID;
        this.treatmentName = treatmentName;
        this.treatmentDescription = treatmentDescription;
        this.medicalServiceProviders = medicalServiceProviders;
    }
    public Treatment(string treatmentID, string treatmentName, string treatmentDescription, List<MedicalServiceProvider> medicalServiceProviders, List<MedicalServiceProviderTreatment> medicalServiceProviderTreatments)
    {
        this.treatmentID = treatmentID;
        this.treatmentName = treatmentName;
        this.treatmentDescription = treatmentDescription;
        this.medicalServiceProviders = medicalServiceProviders;
        this.medicalServiceProviderTreatments = medicalServiceProviderTreatments;
    }
    public Treatment()
    {
        
    }

    public string TreatmentID { get => treatmentID; set => treatmentID = value; }
    public string TreatmentName { get => treatmentName; set => treatmentName = value; }
    public string TreatmentDescription { get => treatmentDescription; set => treatmentDescription = value; }
    public List<MedicalServiceProvider> MedicalServiceProviders { get => medicalServiceProviders; set => medicalServiceProviders = value; }
    public List<MedicalServiceProviderTreatment> MedicalServiceProviderTreatments { get => medicalServiceProviderTreatments; set => medicalServiceProviderTreatments = value; }
}