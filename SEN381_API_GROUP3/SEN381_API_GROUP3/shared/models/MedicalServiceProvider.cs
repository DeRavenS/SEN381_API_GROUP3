public class MedicalServiceProvider
{
    private string policyProviderID;
    private string policyProviderName;
    private string policyProviderAddresses;
    private string policProviderEmail;


   public MedicalServiceProvider(string policyProviderID, string policyProviderName, string policyProviderAddresses, string policProviderEmail)
    {
        this.policyProviderID = policyProviderID;
        this.policyProviderName = policyProviderName;  
        this.policyProviderAddresses = policyProviderAddresses;
        this.policProviderEmail = policProviderEmail;
    }
    



   public string PolicyProviderID { get => policyProviderID; set => policyProviderID = value; }
    public string PolicyProviderName { get => policyProviderName; set => policyProviderName = value; }
    public string PolicyProviderAddresses { get => policyProviderAddresses; set => policyProviderAddresses = value; }
    public string PolicyProviderEmail { get => policProviderEmail; set => policProviderEmail = value; }
}