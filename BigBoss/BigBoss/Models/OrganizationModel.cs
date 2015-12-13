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
        [Display(Name = "Organization Name")]
        public string OrganizationName { get; set; }

        [Required]
        [Display(Name = "Maticni broj")]
        public string MaticniBroj { get; set; }

        [Required]
        [Display(Name = "PIB")]
        public string PIB { get; set; }

        public virtual ApplicationUser usersAplication { get; set; }
    }

    public class DonatorModel
    { 
        [Key]
        public string Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string OrganizationName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string MaticniBroj { get; set; }

        [Required]
        [Display(Name = "Street")]
        public string street { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Total donations")]
        public decimal TotalDonations { get; set; }

        [Required]
        [Display(Name = "Number of donations")]
        public int NumberOfDonations { get; set; }

        public virtual ApplicationUser usersAplication { get; set; }
    }

    public class CompanyModel
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string OrganizationName { get; set; }

        [Required]
        [Display(Name = "Maticni broj")]
        public string MaticniBroj { get; set; }

        [Required]
        [Display(Name = "Delatnost")]
        public string Delatnost { get; set; }

        [Required]
        [Display(Name = "PIB")]
        public string PIB { get; set; }

        public virtual ApplicationUser usersAplication { get; set; }
    }

}