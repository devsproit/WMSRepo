using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Report
{
    public interface IReportService
    {
        int PendingPickUp();
        int PendingInvoice();
        int PendingDispatch();
    }
}
