namespace SEN381_API_Group3.shared.models;

public class Client
{
    string clientID;
    string clientName;
    string clientSurname;
    string clientAddress;
    string clientEmail;
    string clientPhoneNumber;
    string policies;
    string clientStatus;
    string clientAdHocNotes;

    public Client()
    {
    }

    public Client(string clientID, string clientName, string clientSurname, string clientAddress, string clientEmail, string clientPhoneNumber, string policies, string clientStatus, string clientAdHocNotes)
    {
        this.clientID = clientID;
        this.clientName = clientName;
        this.clientSurname = clientSurname;
        this.clientAddress = clientAddress;
        this.clientEmail = clientEmail;
        this.clientPhoneNumber = clientPhoneNumber;
        this.policies = policies;
        this.clientStatus = clientStatus;
        this.clientAdHocNotes = clientAdHocNotes;
    }

    public string ClientID { get => clientID; set => clientID = value; }
    public string ClientName { get => clientName; set => clientName = value; }
    public string ClientSurname { get => clientSurname; set => clientSurname = value; }
    public string ClientAddress { get => clientAddress; set => clientAddress = value; }
    public string ClientEmail { get => clientEmail; set => clientEmail = value; }
    public string ClientPhoneNumber { get => clientPhoneNumber; set => clientPhoneNumber = value; }
    public string Policies { get => policies; set => policies = value; }
    public string ClientStatus { get => clientStatus; set => clientStatus = value; }
    public string ClientAdHocNotes { get => clientAdHocNotes; set => clientAdHocNotes = value; }

    public void makeClaim()
    {
        //Implement Logic
    }
}