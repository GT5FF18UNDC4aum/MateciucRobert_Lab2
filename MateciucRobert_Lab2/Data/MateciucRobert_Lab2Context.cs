using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MateciucRobert_Lab2.Models;

namespace MateciucRobert_Lab2.Data
{
    public class MateciucRobert_Lab2Context : DbContext
    {
        public MateciucRobert_Lab2Context (DbContextOptions<MateciucRobert_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<MateciucRobert_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<MateciucRobert_Lab2.Models.Publisher> Publisher { get; set; } = default!;
    }
}
