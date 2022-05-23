using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagetonixLibraryAssessment.Data.DTO_s
{
  public class BooksDTO
    {
        public long BookID { get; set; }
        public string BookName { get; set; }
        public long CategoryId { get; set; }
        public bool IsFavorite { get; set; }
        public long CreatedByUserID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastDateUpdated { get; set; }
        public bool isDeleted { get; set; }
        public DateTime DateDeleted { get; set; }
    }

    public class CreateBooksDTO
    {
        public string BookName { get; set; }
        public long CategoryId { get; set; }
        public bool IsFavorite { get; set; }
        public long CreatedByUserID { get; set; }
        public DateTime DateCreated { get; set; }      
    }

    public class UpdateBooksDTO
    {
        public long BookID { get; set; }
        public string BookName { get; set; }
        public long CategoryId { get; set; }
        public bool IsFavorite { get; set; }
        public DateTime LastDateUpdated { get; set; }
        public long LastUpdatedByUserID { get; set; }

    }

    public class DeleteBooksDTO
    {
        public long BookID { get; set; }
    }
}
