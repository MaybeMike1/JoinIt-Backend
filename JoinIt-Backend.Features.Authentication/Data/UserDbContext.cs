using JoinIt_Backend.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Features.Authentication.Data
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
