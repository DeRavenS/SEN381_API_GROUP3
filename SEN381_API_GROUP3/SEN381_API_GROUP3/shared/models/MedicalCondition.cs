namespace SEN381_API_Group3.shared.models;

public class MedicalCondition
{
    String medicalConditionID;
    String medicalConditionName;
    List<Treatment> treatments;

    public MedicalCondition(string medicalConditionID, string medicalConditionName, List<Treatment> treatments)
    {
        this.medicalConditionID = medicalConditionID;
        this.medicalConditionName = medicalConditionName;
        this.treatments = treatments;
    }

    public string MedicalConditionID { get => medicalConditionID; set => medicalConditionID = value; }
    public string MedicalConditionName { get => medicalConditionName; set => medicalConditionName = value; }
    public List<Treatment> Treatments { get => treatments; set => treatments = value; }

}