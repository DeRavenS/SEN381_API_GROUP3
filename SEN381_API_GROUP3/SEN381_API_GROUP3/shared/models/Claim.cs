namespace SEN381_API_Group3.shared.models;

public class Claim
{
    int claimID;
    string client;
    string placeOfTreatment;
    int  callDetails;
    string claimeStatus;

    public Claim(int claimID, string client, string placeOfTreatment, int callDetails, string claimeStatus)
    {
        this.claimID = claimID;
        this.client = client;
        this.placeOfTreatment = placeOfTreatment;
        this.callDetails = callDetails;
        this.claimeStatus = claimeStatus;
    }


    public int ClaimID { get => claimID; set => claimID = value; }
    public string Client { get => client; set => client = value; }
    public string PlaceOfTreatment { get => placeOfTreatment; set => placeOfTreatment = value; }
    public int CallDetails { get => callDetails; set => callDetails = value; }
    public string ClaimeStatus { get => claimeStatus; set => claimeStatus = value; }


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