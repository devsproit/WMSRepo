using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary
{


    public class ConstantAndConfiguration
    {
        #region Azure AD 
        public const string AadInstance = "https://login.microsoftonline.com/";
        public const string TenantId = "";
        public const string Authority = AadInstance + TenantId;
        #endregion

        #region Azure SQL SERVER
        public const string SqlResourceUrl = "https://database.windows.net/";
        #endregion
    }
}