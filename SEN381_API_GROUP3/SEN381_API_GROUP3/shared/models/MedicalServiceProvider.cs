public class MedicalServiceProvider
{
    private string policyProviderID;
    private string policyProviderName;
    private string policyProviderAddresses;
    private string policProviderEmail;
    private string policProviderPhone;


   public MedicalServiceProvider(string policyProviderID, string policyProviderName, string policyProviderAddresses, string policProviderEmail, string policProviderPhone)
    {
        this.policyProviderID = policyProviderID;
        this.policyProviderName = policyProviderName;  
        this.policyProviderAddresses = policyProviderAddresses;
        this.policProviderEmail = policProviderEmail;
        this.policProviderPhone = policProviderPhone;
    }
    



   public string PolicyProviderID { get => policyProviderID; set => policyProviderID = value; }
    public string PolicyProviderName { get => policyProviderName; set => policyProviderName = value; }
    public string PolicyProviderAddresses { get => policyProviderAddresses; set => policyProviderAddresses = value; }
    public string PolicyProviderEmail { get => policProviderEmail; set => policProviderEmail = value; }
    public string PolicProviderPhone { get => policProviderPhone; set => policProviderPhone = value; }
}