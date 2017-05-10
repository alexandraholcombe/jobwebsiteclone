using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JobWebsiteClone.Models
{
    [Table ("Listings")]
    public class Listing
    {
        [Key]
        public int Id { get; set; }
        public string JobTitle { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string Description { get; set; }

        public string Location { get; set; }
        public string Salary { get; set; }
        public string JobType { get; set; }
    }
}
