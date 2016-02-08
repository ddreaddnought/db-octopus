using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using octopus.Models;
using System.Web.Security;
using octopus.DAL;
using System.Security.Cryptography;
using System.Text;

namespace octopus.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		OctopusDbContext _dbContext;

		public AccountController()
		{
			_dbContext = new OctopusDbContext();
		}

		//
		// GET: /Account/Login
		[AllowAnonymous]
		public ActionResult Login(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		//
		// POST: /Account/Login
		[HttpPost]
		[AllowAnonymous]
		public ActionResult Login(LoginViewModel model, string returnUrl)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			string passwordHash = GetEncodedHash(model.Password);
            Account result = _dbContext
				.Accounts
				.Where(a => a.Email == model.Email && a.Password == passwordHash)
				.FirstOrDefault();

			if (result != null)
			{
				FormsAuthentication.SetAuthCookie(result.Name, true);
				return RedirectToAction("Index", "Home");
			}
			else
			{
				ModelState.AddModelError("", "Invalid login attempt.");
				return View(model);
			}
		}

		//
		// GET: /Account/Register
		public ActionResult Register()
		{
			return View();
		}

		//
		// POST: /Account/Register
		[HttpPost]
		public ActionResult Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var newAccount = new Account
				{
					Name = model.Name,
					Email = model.Email,
					Password = GetEncodedHash(model.Password)
				};

				if (_dbContext.Accounts.Where(a => a.Email == newAccount.Email).Any())
				{
					ModelState.AddModelError(string.Empty, "User with this email already exitst!");
				}
				else
				{
					FormsAuthentication.SetAuthCookie(newAccount.Name, true);
					_dbContext.Accounts.Add(newAccount);
					_dbContext.SaveChanges();
					return RedirectToAction("Index", "Home");
				}

			}

			return View(model);
		}


		//
		// POST: /Account/LogOff
		public ActionResult LogOff()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Index", "Home");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
			}

			base.Dispose(disposing);
		}

		#region Helpers

		string GetEncodedHash(string password)
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] digest = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
			string base64digest = Convert.ToBase64String(digest, 0, digest.Length);
			return base64digest.Substring(0, base64digest.Length - 2);
		}

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}