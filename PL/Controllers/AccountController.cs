using BLL.Identity;
using DAL.Context;
using Entity.Identity;
using Entity.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AccountController : BaseController
    {
        SatiyorumContext ent = new SatiyorumContext();
        public ActionResult Register()
        {           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]   
        public ActionResult Register(RegisterViewModel model)
        {            
            if (!ModelState.IsValid)
                return View(model);
            var usermanager = IdentityTools.NewUserManager();  //*
            //var kullanici = usermanager.FindByName(model.Username);
            var kullanici = usermanager.FindByEmail(model.Email);
            if (kullanici != null)
            {
                ModelState.AddModelError("", "Bu email sistemde kayıtlı!");
                return View(model);
            }
            ApplicationUser user = new ApplicationUser();
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Email = model.Email;
            user.UserName = model.Username;

            var result = usermanager.Create(user, model.Password);
            
            if (result.Succeeded)
            {
                usermanager.AddToRole(user.Id, "User");
                return RedirectToAction("Login");
            }
                
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }
            return View(model);
        }
        public ActionResult Login(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel() { returnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            
            if (!ModelState.IsValid) 
                return View(model);
            var usermanager = IdentityTools.NewUserManager();

            ApplicationUser kullanici = usermanager.FindByEmail(model.Username);         
            if (kullanici == null)
            {
                ModelState.AddModelError("", "Böyle bir kullanıcı kayıtlı değil!");
                return View(model);
            }
            else if (!usermanager.CheckPassword(kullanici,model.Password))
            {
                ModelState.AddModelError("", "Bilgilerinizi kontrol ediniz!");
                return View(model);
            }
            else
            {
                var role= usermanager.GetRoles(kullanici.Id);
                var authManager = HttpContext.GetOwinContext().Authentication;
                var identity = usermanager.CreateIdentity(kullanici, "ApplicationCookie");
                var authProperty = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe
                };
                authManager.SignIn(authProperty, identity);
                //string Id = HttpContext.User.Identity.GetUserId();
                //ApplicationUser User = new ApplicationUser();
                //User = (from u in ent.Users where u.Id == Id select u).FirstOrDefault();
                Session["User"] = kullanici.Name ;
                if(role[0]=="Admin")
                {

                    return RedirectToAction("Index", "Admin");
                }
                return Redirect(string.IsNullOrEmpty(model.returnUrl) ? "/" : model.returnUrl);
            }
        }
        [Authorize]
        public ActionResult LogOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            var usermanager = IdentityTools.NewUserManager();
            var userstore = IdentityTools.NewUserStore();
            var user = await userstore.FindByEmailAsync(model.Email);
            if(user!=null&&user.UserName==model.UserName&&user.Name==model.Name&&user.Surname==model.Surname)
            {
                
                Random rnd = new Random();
                string str ="qwertyuoplkjhgfdsazxcvbnmQWERTYPOILKJHGFDSAZXCVBNM1234567890!.+*/";
                string NewPassword = "";
                for (int i = 0; i < 7; i++)
                {
                    NewPassword += str[rnd.Next(str.Length)];
                }
                await userstore.SetPasswordHashAsync(user, usermanager.PasswordHasher.HashPassword(NewPassword));
                var result= userstore.Context.SaveChanges();
                
                MailMessage message = new MailMessage();
                message.From = new MailAddress("CRMGiyim@gmail.com");
                message.To.Add(model.Email);
                message.Body = "Sayın " + model.Name + " " + model.Surname + " şifreniz " + NewPassword + " olarak güncellenmiştir. Keyifli alışverişler dileriz.";
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.Credentials = new NetworkCredential("CRMGiyim@gmail.com", "Wissen12345w");
                client.EnableSsl = true;
                client.Send(message);

                return RedirectToAction("Login");
            }
            else
            {
                return View(model);
            }
            
        }

    }
}