
using System;

namespace Employee_Management_System.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
    }

    public class EmployeeBasicDetails : BaseEntity
    {
        public int Id { get; set; }
        public string Salutory { get; set; }
        public required string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string EmployeeID { get; set; }
        public string Role { get; set; }
        public string ReportingManagerUId { get; set; }
        public string ReportingManagerName { get; set; }
        public Address Address { get; set; }
        public DateTime DateOfJoining { get; set; }
    }

    public class EmployeeAdditionalDetails : BaseEntity
    {
        public string EmployeeBasicDetailsUId { get; set; }
        public string AlternateEmail { get; set; }
        public string AlternateMobile { get; set; }
        public WorkInfo_ WorkInformation { get; set; }
        public PersonalDetails_ PersonalDetails { get; set; }
        public IdentityInfo_ IdentityInformation { get; set; }
    }

    public class WorkInfo_
    {
        public string DesignationName { get; set; }
        public string DepartmentName { get; set; }
        public string LocationName { get; set; }
        public string EmployeeStatus { get; set; } // Terminated, Active, Resigned etc
        public string SourceOfHire { get; set; }
        public DateTime DateOfJoining { get; set; }
    }

    public class PersonalDetails_
    {
        public DateTime DateOfBirth { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string Caste { get; set; }
        public string MaritalStatus { get; set; }
        public string BloodGroup { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
    }

    public class IdentityInfo_
    {
        public string PAN { get; set; }
        public string Aadhar { get; set; }
        public string Nationality { get; set; }
        public string PassportNumber { get; set; }
        public string PFNumber { get; set; }
    }

    public class Address
    {
        // Address properties
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
