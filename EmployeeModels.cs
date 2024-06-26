using System;

namespace Employee_Management_System.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
    }

    public class EmployeeBasicDetails : BaseEntity
    {
        public string Salutory { get; set; }
        public string FirstName { get; set; }
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
        public string EmploymentStatus { get; set; }
        public DateTime? ProbationEndDate { get; set; }
        public DateTime? ResignationDate { get; set; }
        public string NoticePeriod { get; set; }
        public DateTime? LastWorkingDate { get; set; }
    }

    public class PersonalDetails_
    {
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string BloodGroup { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime? WeddingAnniversary { get; set; }
        public string PersonalEmailId { get; set; }
    }

    public class IdentityInfo_
    {
        public string PANNumber { get; set; }
        public string PassportNumber { get; set; }
        public string AdharNumber { get; set; }
        public string VoterID { get; set; }
    }

    public class Address
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
    }
}
