using System.Data.Entity;

namespace octopus.Data
{
	public class OctopusDb : DbContext
	{
		public DbSet<EntityUser> Users { get; set; }
	}
}
