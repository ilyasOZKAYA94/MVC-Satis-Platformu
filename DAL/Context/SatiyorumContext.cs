using DAL.Migrations;
using Entity.Entity;
using Entity.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class SatiyorumContext :IdentityDbContext<ApplicationUser>
    {
        public SatiyorumContext() : base("SatiyorumContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SatiyorumContext, Configuration>("SatiyorumContext"));
        }

        public virtual DbSet<Advert> Adverts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<Packets> Packets { get; set; }
        public virtual DbSet<Trademark> Trademarks { get; set; }
    }
}
