using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Core.Http
{
    public static partial class WmsCookieDefaults
    {
        /// <summary>
        /// Gets the cookie name prefix
        /// </summary>
        public static string Prefix => ".Wms";

        /// <summary>
        /// Gets a cookie name of the customer
        /// </summary>
        public static string CustomerCookie => ".Customer";

        public static string BranchId => ".Branch";
    }
}
