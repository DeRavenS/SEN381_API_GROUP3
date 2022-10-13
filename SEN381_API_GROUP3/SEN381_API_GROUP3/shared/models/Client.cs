namespace SEN381_API_Group3.shared.models;

public class Client
    {
        String clientID;
        String clientName;
        String clientIDNumber;
        String clientSurname;
        String clientAddress;
        String clientEmail;
        String clientPhoneNumber;
        List<Policy> policies;
        String clientStatus;
        List<String> clientAdHocNotes;

        public Client(string clientID, string clientName, string clientIDNumber, string clientSurname, string clientAddress, string clientEmail, string clientPhoneNumber, List<Policy> policies, string clientStatus, List<string> clientAdHocNotes)
        {
            this.clientID = clientID;
            this.clientName = clientName;
            this.clientIDNumber = clientIDNumber;
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
        public string ClientIDNumber { get => clientIDNumber; set => clientIDNumber = value; }
        public string ClientSurname { get => clientSurname; set => clientSurname = value; }
        public string ClientAddress { get => clientAddress; set => clientAddress = value; }
        public string ClientEmail { get => clientEmail; set => clientEmail = value; }
        public string ClientPhoneNumber { get => clientPhoneNumber; set => clientPhoneNumber = value; }
        public List<Policy> Policies { get => policies; set => policies = value; }
        public string ClientStatus { get => clientStatus; set => clientStatus = value; }
        public List<string> ClientAdHocNotes { get => clientAdHocNotes; set => clientAdHocNotes = value; }

        public void makeClaim()
        {
            //Implement Logic
        }
    }