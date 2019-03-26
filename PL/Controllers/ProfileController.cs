using DAL.Context;
using Entity.Entity;
using Entity.Identity;
using Entity.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ProfileController : BaseController
    {
        SatiyorumContext ent = new SatiyorumContext();
        // GET: Profile
        [Authorize]
        public ActionResult Index()
        {
            string Id = HttpContext.User.Identity.GetUserId();
            ApplicationUser user = (from u in ent.Users where u.Id == Id select u).FirstOrDefault();
            int PostedAdvert = (from a in ent.Adverts where a.SellerId == Id select a).Count();
            int SelledAdvert = (from a in ent.Adverts where a.SellerId==Id && a.IsSold == true select a).Count();
            ProfileViewModel Model = new ProfileViewModel();
            Model.Ad = user.Name;
            Model.Soyad = user.Surname;
            Model.UserName = user.UserName;
            Model.remainingAdvert = user.remainingAdvert;
            Model.PostedAdvert = PostedAdvert;
            Model.SelledAdvert = SelledAdvert;
            Model.Email = user.Email;
            Model.PhoneNumber = user.PhoneNumber;
            Model.RegisterDate = user.RegisterDate;
            return View(Model);
        }
        [Authorize]
        public ActionResult MyAdverts()
        {
            string Id = HttpContext.User.Identity.GetUserId();
            List<Advert> model = (from advert in ent.Adverts where advert.SellerId == Id && advert.IsSold == false && advert.IsDeleted == false select advert).ToList();
            return View(model);
        }
        [Authorize]
        public ActionResult AdvertUpdate(int Id)
        {
            Advert adv = (from a in ent.Adverts where a.Id == Id select a).FirstOrDefault();
            return View(adv);
        }
        [HttpPost, ValidateInput(false)]
        [Authorize]
        public ActionResult AdvertUpdate(Advert model)
        {
            Advert a = (from adv in ent.Adverts where adv.Id == model.Id select adv).FirstOrDefault();
            try
            {
                a.Title = model.Title;
                a.ProductName = model.ProductName;
                a.Description = model.Description;
                a.Color = model.Color;
                a.Warranty = model.Warranty;
                a.Price = model.Price;
                ent.SaveChanges();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }
            return RedirectToAction("MyAdverts");
        }
        [Authorize]
        public ActionResult RemoveAdvert(int Id)
        {
            Advert a = (from adv in ent.Adverts where adv.Id == Id select adv).FirstOrDefault();
            try
            {
                a.IsDeleted = true;
                ent.SaveChanges();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }
            return RedirectToAction("MyAdverts");
        }
        [Authorize]
        public ActionResult SoldAdvert(int Id)
        {
            Advert a = (from adv in ent.Adverts where adv.Id == Id select adv).FirstOrDefault();
            try
            {
                a.IsSold = true;
                a.IsDeleted = true;
                ent.SaveChanges();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }
            return RedirectToAction("MyAdverts");
        }
    }
}