namespace SEN381_API_GROUP3.shared.models
{
    public class AddClaim
    {
        private int claimID;
        private string client;
        private List<int> medicalConditions;
        private string placeOfTreatment;
        private int callDetails;
        private string claimeStatus;
        private string startTime;
        private string endTime;

        public int ClaimID { get => claimID; set => claimID = value; }
        public string Client { get => client; set => client = value; }
        public List<int> MedicalConditions { get => medicalConditions; set => medicalConditions = value; }
        public string PlaceOfTreatment { get => placeOfTreatment; set => placeOfTreatment = value; }
        public int CallDetails { get => callDetails; set => callDetails = value; }
        public string ClaimeStatus { get => claimeStatus; set => claimeStatus = value; }
        public string StartTime { get => startTime; set => startTime = value; }
        public string EndTime { get => endTime; set => endTime = value; }

        public AddClaim(int claimID, string client, List<int> medicalConditions, string placeOfTreatment, int callDetails, string claimeStatus , string startTime, string endTime)
        {
            this.ClaimID = claimID;
            this.Client = client;
            this.MedicalConditions = medicalConditions;
            this.PlaceOfTreatment = placeOfTreatment;
            this.CallDetails = callDetails;
            this.ClaimeStatus = claimeStatus;
            this.StartTime = startTime;
            this.EndTime = endTime;
        }
    }
}
