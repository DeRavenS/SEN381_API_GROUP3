using Microsoft.AspNetCore.DataProtection.KeyManagement;
using SEN381_API_Group3.shared.models;
using System.Security.Cryptography.Xml;

namespace SEN381_API_GROUP3.shared.models
{
    public class MedicalServiceProviderTreatment
    {
        int mSPTID;
        string providerStatus;
        MedicalServiceProvider medicalServiceProvidorID;

        public int MSPTID { get => mSPTID; set => mSPTID = value; }
        public string ProviderStatus { get => providerStatus; set => providerStatus = value; }
        public MedicalServiceProvider MedicalServiceProvidorID { get => medicalServiceProvidorID; set => medicalServiceProvidorID = value; }

        public MedicalServiceProviderTreatment(int mSPTID, string providerStatus, MedicalServiceProvider medicalServiceProvidorID)
        {
            this.MSPTID = mSPTID;
            this.ProviderStatus = providerStatus;
            this.MedicalServiceProvidorID = medicalServiceProvidorID;
        }
        public MedicalServiceProviderTreatment()
        {
        }
    }
}
