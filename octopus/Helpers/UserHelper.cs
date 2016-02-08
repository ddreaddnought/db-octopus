using octopus.DAL;
using System.Linq;
using System.Security.Principal;

namespace octopus.Helpers
{
	public static class UserHelper
	{
		public static int GetUserId(IIdentity identity)
		{
			if (!identity.IsAuthenticated)
				return 0;
			else
				using (var context = new OctopusDbContext())
					return context.Accounts.Where(a => a.Name == identity.Name).First().Id;
		}

		public static bool IsAdmin(IIdentity identity)
		{
			if (!identity.IsAuthenticated)
				return false;
			else
				using (var context = new OctopusDbContext())
					return context.Accounts.Where(a => a.Name == identity.Name).First().IsAdmin;
		}
	}
}
