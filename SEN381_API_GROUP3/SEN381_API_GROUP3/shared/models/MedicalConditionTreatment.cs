namespace SEN381_API_GROUP3.shared.models
{
    public class MedicalConditionTreatment
    {
        string treatmentID;
        string treatmentName;
        string treatmentDescription;
        public MedicalConditionTreatment()
        {

        }

        public MedicalConditionTreatment(string treatmentID, string treatmentName, string treatmentDescription)
        {
            this.TreatmentID = treatmentID;
            this.TreatmentName = treatmentName;
            this.TreatmentDescription = treatmentDescription;
        }

        public string TreatmentID { get => treatmentID; set => treatmentID = value; }
        public string TreatmentName { get => treatmentName; set => treatmentName = value; }
        public string TreatmentDescription { get => treatmentDescription; set => treatmentDescription = value; }
    }
}
