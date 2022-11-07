namespace SEN381_API_Group3.shared.models;

public class Client
{
    string clientID;
    string clientName;
    string clientSurname;
    string clientAddress;
    string clientEmail;
    string clientPhoneNumber;
    ClientPolicy? policy;
    string clientStatus;
    string clientAdHocNotes;

    public Client()
    {
    }

    public Client(string clientID, string clientName, string clientSurname, string clientAddress, string clientEmail, string clientPhoneNumber, ClientPolicy policy, string clientStatus, string clientAdHocNotes)
    {
        this.clientID = clientID;
        this.clientName = clientName;
        this.clientSurname = clientSurname;
        this.clientAddress = clientAddress;
        this.clientEmail = clientEmail;
        this.clientPhoneNumber = clientPhoneNumber;
        this.policy = policy;
        this.clientStatus = clientStatus;
        this.clientAdHocNotes = clientAdHocNotes;
    }

    public string ClientID { get => clientID; set => clientID = value; }
    public string ClientName { get => clientName; set => clientName = value; }
    public string ClientSurname { get => clientSurname; set => clientSurname = value; }
    public string ClientAddress { get => clientAddress; set => clientAddress = value; }
    public string ClientEmail { get => clientEmail; set => clientEmail = value; }
    public string ClientPhoneNumber { get => clientPhoneNumber; set => clientPhoneNumber = value; }
    public ClientPolicy? Policy { get => policy; set => policy = value; }
    public string ClientStatus { get => clientStatus; set => clientStatus = value; }
    public string ClientAdHocNotes { get => clientAdHocNotes; set => clientAdHocNotes = value; }

}

public class ClientPolicy
{
    private string? policyID;
    private string? policyName;
    private string? policyStatus;

    public ClientPolicy(string policyID, string policyName, string policyStatus)
    {
        this.policyID = policyID;
        this.policyName = policyName;
        this.policyStatus = policyStatus;
    }

    public ClientPolicy()
    {
    }

    public string? PolicyID { get => policyID; set => policyID = value; }
    public string? PolicyName { get => policyName; set => policyName = value; }
    public string? PolicyStatus { get => policyStatus; set => policyStatus = value; }
}