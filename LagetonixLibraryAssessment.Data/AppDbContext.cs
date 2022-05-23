using LagetonixLibraryAssessment.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagetonixLibraryAssessment.Data
{
   public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public  DbSet<Books> Books { get; set; }
        public  DbSet<Catergory> Catergories { get; set; }

    }
}
