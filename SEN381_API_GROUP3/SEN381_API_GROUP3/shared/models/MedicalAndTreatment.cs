namespace SEN381_API_GROUP3.shared.models
{
    public class MedicalAndTreatment
    {
        int medicalConditionID;
        string medicalConditionName;
        string medicalConditionDescription;
        List<string> medicalConditionTreatments;

        public MedicalAndTreatment(int medicalConditionID, string medicalConditionName, string medicalConditionDescription, List<string> medicalConditionTreatments)
        {
            this.MedicalConditionID = medicalConditionID;
            this.MedicalConditionName = medicalConditionName;
            this.MedicalConditionDescription = medicalConditionDescription;
            this.MedicalConditionTreatments = medicalConditionTreatments;
        }

        public int MedicalConditionID { get => medicalConditionID; set => medicalConditionID = value; }
        public string MedicalConditionName { get => medicalConditionName; set => medicalConditionName = value; }
        public string MedicalConditionDescription { get => medicalConditionDescription; set => medicalConditionDescription = value; }
        public List<string> MedicalConditionTreatments { get => medicalConditionTreatments; set => medicalConditionTreatments = value; }
    }
}
