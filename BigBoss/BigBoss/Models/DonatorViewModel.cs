using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BigBoss.Models {
    public class DonatorViewModel {
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
    }
}