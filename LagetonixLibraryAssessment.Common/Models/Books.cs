using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagetonixLibraryAssessment.Models.Models
{
   public class Books
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BookID { get; set; }
        public string BookName { get; set; }
        public long CategoryId { get; set; }
        public bool IsFavorite { get; set; }
        public long CreatedByUserID { get; set; }
        public long LastUpdatedByUserID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastDateUpdated { get; set; }
        public bool isDeleted { get; set; }
        public DateTime DateDeleted { get; set; }

    }
}
