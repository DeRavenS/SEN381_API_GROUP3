namespace SEN381_API_Group3.shared.models;

public class FamilyClient: Client{
    private FamilyMember[]? familyMembers;

    public FamilyClient(string clientID, string clientName, string clientSurname, string clientAddress, string clientEmail, string clientPhoneNumber, string policies, string clientStatus, string clientAdHocNotes, FamilyMember[]? familyMembers) : base(clientID, clientName, clientSurname, clientAddress, clientEmail, clientPhoneNumber, policies, clientStatus, clientAdHocNotes)
    {
        this.FamilyMembers = familyMembers;
    }

    public FamilyMember[]? FamilyMembers { get => familyMembers; set => familyMembers = value; }
}