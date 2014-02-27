using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientPoC.Constants
{
    public static class Constants
    {
        //Authentication Server
        public const string AuthenticationServerBaseUrl = "https://auth.shearwatersystems.com/";
        public const string AuthenticationServerRequestUrl = "issue/adfs";

        //Token
        public const string AuthSessionTokenKey = "SessionToken";
        public const string AuthSessionUsername = "Username";

        //Server
        public const string ServerBaseUrl = "https://dev.shearwatersystems.com/";
        public const string ServerTenantDev = "Dev/";

        public const string GrantType = "password";
        public const string Scope = "https://ssl-demo.shearwatersystems.com/adfs/services/trust";

        //Request URLs - GET
        public const string HubLayoutContainerPatientUrl = "HubData/GetHubLayoutContainer?containerName=PatientPortal&personalOnly=personalOnly";
        public const string HubLayoutContainerClinicalUrl = "HubData/GetHubLayoutContainer?containerName=ClinicalPortal&personalOnly=personalOnly";

        //Request URLs - POST
        public const string PostHubDataCollectionUrl = "HubData/GetHubCollection";
    }
}