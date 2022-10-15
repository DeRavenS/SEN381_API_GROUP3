namespace SEN381_API_Group3.shared.models;

public class Policy
{
    private String policyId;
    private String policyName;
    private String policyStatus;
    private Package[] package;

    public Policy(string policyId, string policyName, string policyStatus, Package[] package)
    {
        this.PolicyId = policyId;
        this.PolicyName = policyName;
        this.PolicyStatus = policyStatus;
        this.Package = package;
    }
    public Policy()
    {
        
    }

    public string PolicyId { get => policyId; set => policyId = value; }
    public string PolicyName { get => policyName; set => policyName = value; }
    public string PolicyStatus { get => policyStatus; set => policyStatus = value; }
    public Package[] Package { get => package; set => package = value; }
}