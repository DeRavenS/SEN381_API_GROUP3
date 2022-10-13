namespace SEN381_API_Group3.shared.models;

public class Claim
{
    String claimID;
    Client client;
    List<MedicalCondition> medicalConditions;
    String placeOfTreatment;
    CallDetails? callDetails = null;

    public Claim(string claimID, Client client, List<MedicalCondition> medicalConditions, string placeOfTreatment, CallDetails? callDetails)
    {
        this.claimID = claimID;
        this.client = client;
        this.medicalConditions = medicalConditions;
        this.placeOfTreatment = placeOfTreatment;
        this.callDetails = callDetails;
    }

    public string ClaimID { get => claimID; set => claimID = value; }
    public Client Client { get => client; set => client = value; }
    public List<MedicalCondition> MedicalConditions { get => medicalConditions; set => medicalConditions = value; }
    public string PlaceOfTreatment { get => placeOfTreatment; set => placeOfTreatment = value; }
    public CallDetails? CallDetails { get => callDetails; set => callDetails = value; }


    public Boolean isValid()//Make sure claim is valid
    {
        //Implement Logic
        return true;
    }

    public void approveClaim()
    {
        //Implement Logic
    }

    public void rejectClaim()
    {
        //Implement Logic
    }

    public void pendAwaitingClaimForm()//When Claim needs to use a form instead of the new system
    {
        //Implement Logic
    }

    public void sendConfirmation()//Send sms or email confirmation to client
    {
        //Implement Logic
    }

}