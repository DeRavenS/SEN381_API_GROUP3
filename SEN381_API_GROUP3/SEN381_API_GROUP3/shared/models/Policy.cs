namespace SEN381_API_Group3.shared.models;

public class Policy
{
    private string policyId;
    private string policyName;
    private string policyStatus;
    private DateTime? statusStartDate;
    private DateTime? statusEndDate;
    private List<Package> package = new List<Package>();

    public Policy(string policyId, string policyName, string policyStatus, List<Package> package, DateTime? statusStartDate=null, DateTime? statusEndDate=null)
    {
        this.PolicyId = policyId;
        this.PolicyName = policyName;
        this.PolicyStatus = policyStatus;
        this.Package = package;
        this.StatusStartDate = statusStartDate;
        this.StatusEndDate = statusEndDate;
    }
    public Policy()
    {
        
    }

    public string PolicyId { get => policyId; set => policyId = value; }
    public string PolicyName { get => policyName; set => policyName = value; }
    public string PolicyStatus { get => policyStatus; set => policyStatus = value; }
    public List<Package> Package { get => package; set => package = value; }
    public DateTime? StatusStartDate { get => statusStartDate; set => statusStartDate = value; }
    public DateTime? StatusEndDate { get => statusEndDate; set => statusEndDate = value; }
}