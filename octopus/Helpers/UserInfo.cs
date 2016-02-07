using octopus.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace octopus.Helpers
{
	static class UserInfo
	{
		public static int UserId(IIdentity identity)
		{
			if (!identity.IsAuthenticated)
				return 0;
			else
				using (var context = new OctopusDbContext())
					return context.Accounts.Where(a => a.Name == identity.Name).First().Id;
		}
	}
}
