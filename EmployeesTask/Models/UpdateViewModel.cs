using System.ComponentModel.DataAnnotations;

namespace EmployeesTask.Models
{
    public class UpdateViewModel
    {
        public int EmployeeId { get; set; }
         public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Qualification { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
    }
}
