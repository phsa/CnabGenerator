using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace CnabGenerator.Models
{
    public class CnabGeneratorContext : DbContext
    {
        public CnabGeneratorContext(DbContextOptions<CnabGeneratorContext> options)
            : base(options)
        {
        }

        public DbSet<BankItem> BankItems { get; set; } = null!;
    }
}