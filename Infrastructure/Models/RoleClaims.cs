using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HordeFlow.HR.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace HordeFlow.HR.Infrastructure.Models
{
    public class RoleClaims : IdentityRoleClaimsBase
    {
        public bool? Active { get; set; }
    }
} 