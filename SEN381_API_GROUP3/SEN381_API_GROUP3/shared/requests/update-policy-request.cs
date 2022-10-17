namespace SEN381_API_GROUP3.shared.requests
{
    public class IUpdatePolicyRequest
    {
        int policyID;
        string policyName;
        DateTime? startTime;
        DateTime? endTime;

        public IUpdatePolicyRequest(int policyID, string policyName, DateTime? startTime, DateTime? endTime)
        {
            this.policyID = policyID;
            this.policyName = policyName;
            this.startTime = startTime;
            this.endTime = endTime;
        }

        public int PolicyID { get => policyID; set => policyID = value; }
        public string PolicyName { get => policyName; set => policyName = value; }
        public DateTime? StartTime { get => startTime; set => startTime = value; }
        public DateTime? EndTime { get => endTime; set => endTime = value; }
    }
}
