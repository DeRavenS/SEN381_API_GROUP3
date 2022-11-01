using Newtonsoft.Json;
using SEN381_API_GROUP3.shared.models;

namespace SEN381_API_Group3.shared.models;

public class Treatment
{
    string treatmentID;
    string treatmentName;
    string treatmentDescription;
    List<MedicalServiceProviderTreatment> medicalServiceProviderTreatments;
    [JsonConstructor]
    public Treatment(string treatmentID, string treatmentName, string treatmentDescription, List<MedicalServiceProviderTreatment> medicalServiceProviderTreatments)
    {
        this.treatmentID = treatmentID;
        this.treatmentName = treatmentName;
        this.treatmentDescription = treatmentDescription;
        this.medicalServiceProviderTreatments = medicalServiceProviderTreatments;
    }
    public Treatment()
    {
        
    }

    public string TreatmentID { get => treatmentID; set => treatmentID = value; }
    public string TreatmentName { get => treatmentName; set => treatmentName = value; }
    public string TreatmentDescription { get => treatmentDescription; set => treatmentDescription = value; }
    public List<MedicalServiceProviderTreatment> MedicalServiceProviderTreatments { get => medicalServiceProviderTreatments; set => medicalServiceProviderTreatments = value; }
}