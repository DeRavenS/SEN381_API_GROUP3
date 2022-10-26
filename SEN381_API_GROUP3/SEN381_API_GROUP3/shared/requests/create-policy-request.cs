namespace SEN381_API_GROUP3.shared.requests
{
    public class ICreatePolicyRequest
    {
        int policyID;
        string policyName;
        DateTime? startTime;
        DateTime? endTime;
        Dictionary<string, string> treatmentCoverages;//Treatment with their coverage level (id's)

        public ICreatePolicyRequest(int policyID, string policyName, DateTime? startTime, DateTime? endTime, Dictionary<string, string> treatmentCoverages)
        {
            this.policyID = policyID;
            this.policyName = policyName;
            this.startTime = startTime != null ? startTime : null;
            this.endTime = endTime != null ? endTime : null;
            this.treatmentCoverages = treatmentCoverages;
        }

        public int PolicyID { get => policyID; set => policyID = value; }
        public string PolicyName { get => policyName; set => policyName = value; }
        public DateTime? StartTime { get => startTime; set => startTime = value; }
        public DateTime? EndTime { get => endTime; set => endTime = value; }
        public Dictionary<string, string> TreatmentCoverages { get => treatmentCoverages; set => treatmentCoverages = value; }
    }
}
