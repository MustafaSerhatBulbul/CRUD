using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Data
{
    public class FakeData : DbContext
    {
        public FakeData(DbContextOptions<FakeData> options)
            : base(options)
        {
        }

        public DbSet<Musteri> Musteriler { get; set; }
    }
}














