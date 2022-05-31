using BulkyBookWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Data
{
    // This file is needed to establish the connection with Entity Framework
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // For every model/entity that needs to be created in the DB, we need to create a DbSet in this file.
        
        // The below creates a Categories DB table based on the Category model.
        public DbSet<Category> Categories { get; set; }
    }
}
