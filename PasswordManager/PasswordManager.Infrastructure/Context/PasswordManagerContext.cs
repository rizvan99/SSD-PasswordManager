using Microsoft.EntityFrameworkCore;
using PasswordManager.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Infrastructure.Context
{
    public class PasswordManagerContext : DbContext
    {
        public PasswordManagerContext(DbContextOptions<PasswordManagerContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Manager> Managers { get; set; }


    }
}
