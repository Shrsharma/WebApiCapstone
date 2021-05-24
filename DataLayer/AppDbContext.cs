
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class AppDbContext : IdentityDbContext
    {
        

        public AppDbContext(DbContextOptions option) : base(option)
        {

        }

        public DbSet<UserModel> UserModel { get; set; }
        public DbSet<MedicineModel> MedicineModel { get; set; }
        public DbSet<AuthModel> AuthModel { get; set; }
        public DbSet<CategoryModel> CategoryModel { get; set; }
    }
}
