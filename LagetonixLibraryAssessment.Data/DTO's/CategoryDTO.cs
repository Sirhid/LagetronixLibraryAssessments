using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagetonixLibraryAssessment.Data.DTO_s
{
   public class CategoryDTO
    {
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public long CreatedByUserID { get; set; }
        public long DeletedByUserID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastDateUpdated { get; set; }
        public bool isDeleted { get; set; }
        public DateTime DateDeleted { get; set; }
    }

    public class CreateCategoryDTO
    {
        public string CategoryName { get; set; }
        public long CreatedByUserID { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public class UpdateCategoryDTO
    {
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime LastDateUpdated { get; set; }
        public long LastUpdatedByUserID { get; set; }
    }

    public class DeleteCategoryDTO
    {
        public long CategoryId { get; set; }
        public long DeletedByUserID { get; set; }


    }

}
