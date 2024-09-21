using KMZWDotNetCore.ConsoleApp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMZWDotNetCore.ConsoleApp
{
    public class AppDbContext: DbContext
    {
        protected SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        { 
            DataSource=".",
            InitialCatalog="DotNetTrainingBatch5",
            UserID = "sa",
            Password = "sasa@123",
            TrustServerCertificate = true,
        };    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_sqlConnectionStringBuilder.ConnectionString);
            }
        }

        public DbSet<EFCoreDataModel> Blogs { get; set; }
    }
}
