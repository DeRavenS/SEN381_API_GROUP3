using SEN381_API_Group3.shared.models;

namespace SEN381_API_GROUP3.shared.models
{
    public class PackageTreatmentCoverage
    {
        private Treatment treatment;
        private TreatmentCoverage coverage;

        public PackageTreatmentCoverage(Treatment treatment, TreatmentCoverage coverage)
        {
            this.treatment = treatment;
            this.coverage = coverage;
        }

        public Treatment Treatment { get => treatment; set => treatment = value; }
        public TreatmentCoverage Coverage { get => coverage; set => coverage = value; }
    }
}
