
public class FamilyMember{

    private string memberId;
    private string memberName;
    private string memberSurname;
    private string phoneNumber;
    private string email;
    private string address;
    private string IdNumber;
    private string role;
    private int clientID;

    public FamilyMember(string memberId, string memberName, string memberSurname, string phoneNumber, string email, string address, string idNumber, string role, int clientID)
    {
        this.memberId = memberId;
        this.memberName = memberName;
        this.memberSurname = memberSurname;
        this.phoneNumber = phoneNumber;
        this.email = email;
        this.address = address;
        this.IdNumber = idNumber;
        this.role = role;
        this.clientID = clientID;
    }

    public string MemberId { get => memberId; set => memberId = value; }
    public string MemberName { get => memberName; set => memberName = value; }
    public string MemberSurname { get => memberSurname; set => memberSurname = value; }
    public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
    public string Email { get => email; set => email = value; }
    public string Address { get => address; set => address = value; }
    public string IdNumber1 { get => IdNumber; set => IdNumber = value; }
    public string Role { get => role; set => role = value; }
}

    