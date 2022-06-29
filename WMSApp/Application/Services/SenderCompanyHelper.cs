using Application.Common;
using DatabaseLibrary;
using DatabaseLibrary.SQL;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core.Data;

namespace Application.Services
{
    public class SenderCompanyHelper:ISenderCompany
    {
        private readonly IAdoConnection _adoConnection;
        private readonly IRepository<SenderCompanyNameDb> _senderCompanyRepository;

        public SenderCompanyHelper(IAdoConnection adoConnection, IRepository<SenderCompanyNameDb> senderCompanyRepository)
        {
            _adoConnection = adoConnection;
            _senderCompanyRepository = senderCompanyRepository;
        }

        public List<SenderCompanyNameDb> GetAllSenderCompanies()
        {
            List<SenderCompanyNameDb> data = new List<SenderCompanyNameDb>();
            DataTable dbDT = _adoConnection.GetDatatableFromSqlWithSP(Constants.GetAllSenderCompanySP);
            if (dbDT != null)
                data = dbDT.ToList<SenderCompanyNameDb>();
            return data;
        }

     
    }
}
