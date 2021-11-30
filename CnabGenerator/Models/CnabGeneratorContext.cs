using Microsoft.EntityFrameworkCore;

namespace CnabGenerator.Models
{
    public class CnabGeneratorContext : DbContext
    {
        public CnabGeneratorContext(DbContextOptions<CnabGeneratorContext> options)
            : base(options)
        {
        }

        public DbSet<IssuingInstitution> IssuingInstitutions { get; set; } = null!;
    }
}