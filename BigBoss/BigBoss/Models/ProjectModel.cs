using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BigBoss.Models
{
    public class ProjectModel
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Name of project")]
        public string nameProject { get; set; }

        [Required]
        [Display(Name = "Description of project")]
        public string descProject { get; set; }

        [Display(Name = "Additionl info")]
        public string additionalInfo { get; set; }

        [Display(Name = "Tags")]
        [Required]
        public string tagsProject { get; set; }

        [Display(Name = "Money needed for donation")]
        [Required]
        public decimal money { get; set; }

        [Display(Name = "Money with commision")]
        public decimal moneyWithCommission { get; set; }

        [Display(Name = "Money raised")]
        public decimal moneyRaised { get; set; }

        [Display(Name = "Number of donations")]
        public int numberOfDonations { get; set; }

        [Display(Name = "Category")]
        public CategoryModel categoryMod { get; set; }
    }
}