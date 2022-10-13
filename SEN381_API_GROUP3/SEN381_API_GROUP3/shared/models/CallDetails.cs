namespace SEN381_API_Group3.shared.models;

public class CallDetails
{
    String callDetailsID;
    DateTime startTime;
    DateTime endTime;

    public CallDetails(string callDetailsID, DateTime startTime, DateTime endTime)
    {
        this.callDetailsID = callDetailsID;
        this.startTime = startTime;
        this.endTime = endTime;
    }

    public string CallDetailsID { get => callDetailsID; set => callDetailsID = value; }
    public DateTime StartTime { get => startTime; set => startTime = value; }
    public DateTime EndTime { get => endTime; set => endTime = value; }

    public void startTimer()
    {
        //Implement Logic
    }

    public void endTimer()
    {
        //Implement Logic
    }
}