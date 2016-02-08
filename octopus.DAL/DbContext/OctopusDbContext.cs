using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace octopus.DAL
{
    public class OctopusDbContext : DbContext
    {
		private static OctopusDbContext _instance = new OctopusDbContext();

		public static OctopusDbContext Instance { get { return _instance; } }

        public OctopusDbContext() : base() { }

        public OctopusDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Database.SetInitializer<OctopusDbContext>(new OctopusDbContextInitializer());
        }

        public DbSet<SqlQuery> Queries { get; set; }
		public DbSet<Account> Accounts { get; set; }
		public DbSet<PreparedScript> PreparedScripts { get; set; }
		public DbSet<DataSource> DataSources { get; set; }
		public DbSet<SqlQueryResult> QueryResults { get; set; }
	}
}
