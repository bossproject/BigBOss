using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BigBoss.Models
{
    public class OrganizationModel
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string OrganizationName { get; set; }

        [Required]
        public string MaticniBroj { get; set; }

        [Required]
        public string PIB { get; set; }

        public ApplicationUser usersAplication { get; set; }
    }
}