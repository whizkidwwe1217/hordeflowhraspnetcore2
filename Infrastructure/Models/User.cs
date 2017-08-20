using System.ComponentModel.DataAnnotations;

namespace HordeFlow.HR.Infrastructure.Models
{
    public class User : CompanyEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool? Active { get; set; }
    }
}