using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ClientPoC.Repository;
using ClientPoC.Services.Login;
using ClientPoC.ViewModels;

namespace ClientPoC.Controllers
{
    public class AccountController : BaseController
    {
        private UserRepository _userRepository = new UserRepository();
        private LoginService _loginService = new LoginService();

        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // GET: /Account/
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            var isUserAuthenticated = false;
            if (ModelState.IsValid)
            {
                isUserAuthenticated = await _loginService.CheckUserAuthentication(model.UserName, model.Password);
                if (isUserAuthenticated)
                {
                    var authToken = _loginService.GetAuthenticationToken();
                    if (!string.IsNullOrEmpty(authToken))
                    {
                        Debug.WriteLine(authToken);
                        Session[Constants.Constants.AuthSessionTokenKey] = authToken;
                        Session[Constants.Constants.AuthSessionUsername] = model.UserName;
                        FormsAuthentication.SetAuthCookie(model.UserName, createPersistentCookie: true);
                        
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        isUserAuthenticated = false;
                    }
                }
            }

            if (!isUserAuthenticated)
            {
                ModelState.AddModelError("", "Invalid username or password.");
            }

            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return View("Login");
        }

        [HttpGet]
        public ActionResult LoginPartial()
        {
            return View("_LoginPartial");
        }

    }
}