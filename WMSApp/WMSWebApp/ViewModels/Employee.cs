using System.ComponentModel.DataAnnotations;

namespace WMSWebApp.ViewModels
{
    public class Employee
    {
        public int Id { get; set; }
        public string ScreenCode { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string CompanySelection { get; set; }
        public string BranchCode { get; set; }
        public string Role { get; set; }
       

    }
}
