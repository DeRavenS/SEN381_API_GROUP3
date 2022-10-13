namespace SEN381_API_Group3.shared.models;

public class Package
    {
        String packageID;
        List<TreatmentCoverage> treatment;
        private String st;



       public string PackageID { get => packageID; set => packageID = value; }
        public List<TreatmentCoverage> Treatment { get => treatment; set => treatment = value; }
        public string St { get => st; set => st = value; }



       public Package(String packageID, List<TreatmentCoverage> treatment, String st)
        {
            this.PackageID = packageID;
            this.Treatment = treatment;
            this.St = st;
        }
    }