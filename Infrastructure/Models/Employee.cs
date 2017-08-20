using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HordeFlow.HR.Infrastructure.Models
{
    public class Employee : CompanyEntity
    {
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Birthdate { get; set; }
        public Gender Gender { get; set; }
        public string Title { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public bool? Active { get; set; }
        public string Religion { get; set; }
        public string Citizenship { get; set; }
        public string TIN { get; set; }
        public string PHIC { get; set; }
        public string SSS { get; set; }
        public string GSIS { get; set; }
        /* Addresses */
        public ICollection<EmployeeAddress> EmployeeAddresses { get; set; }
        /* Groupings */
        public int? DesignationId { get; set; }
        public Designation Designation { get; set; }
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
        public int? TeamId { get; set; }
        public Team Team { get; set; }
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }

    public enum MaritalStatus
    {
        Single,
        Married,
        Divorced,
        Widowed
    }
}