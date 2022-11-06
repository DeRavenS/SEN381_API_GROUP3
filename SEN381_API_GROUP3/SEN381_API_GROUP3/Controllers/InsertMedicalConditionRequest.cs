namespace SEN381_API_GROUP3.Controllers
{
    public class InsertMedicalConditionRequest
    {
        string name;
        string description;
        List<string> IDs;
        int counts;
        public InsertMedicalConditionRequest()
        {

        }
        public InsertMedicalConditionRequest(string name, string description, List<string> iDs, int counts)
        {
            this.Name = name;
            this.Description = description;
            IDs1 = iDs;
            this.Counts = counts;
        }

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public List<string> IDs1 { get => IDs; set => IDs = value; }
        public int Counts { get => counts; set => counts = value; }
    }
}
