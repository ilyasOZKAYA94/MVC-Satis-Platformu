using BLL.Identity;
using BLL.Repository;
using DAL.Context;
using Entity.Entity;
using Entity.Identity;
using Entity.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AdminController : BaseController
    {
        SatiyorumContext ent = new SatiyorumContext();

        // GET: Admin   
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            TempData["Sold"] = (from a in ent.Adverts where a.IsSold == true select a).Count();
            TempData["UnSold"] = (from a in ent.Adverts where a.IsSold == false && a.IsDeleted == true select a).Count();
            TempData["Active"] = (from a in ent.Adverts where a.IsSold == false && a.IsDeleted == false && a.IsConfirmed == true select a).Count();
            TempData["User"] = ent.Users.Count();
            Advert adv = (from a in ent.Adverts where a.IsConfirmed == false && a.IsDeleted == false select a).Take(1).FirstOrDefault();
            return View(adv);
            
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Confirm(int Id)
        {
            Advert a = (from x in ent.Adverts where x.Id == Id select x).FirstOrDefault();
            
            
                try
                {
                    a.IsConfirmed = true;
                    ent.SaveChanges();
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    throw;
                }

            Advert adv = new Advert();
            adv= (from advert in ent.Adverts where advert.IsConfirmed == false && advert.IsDeleted==false select advert).Take(1).FirstOrDefault();
            return View("Index",adv);
        }
        // [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Decline(int Id)
        {
            Advert a = (from x in ent.Adverts where x.Id == Id select x).FirstOrDefault();
            ApplicationUser user = (from u in ent.Users where u.Id == a.SellerId select u).FirstOrDefault();


            try
            {
                a.IsDeleted = true;
                string imagePath1 = Server.MapPath("/images/" + a.Image1);
                string imagePath2 = Server.MapPath("/images/" + a.Image2);
                string imagePath3 = Server.MapPath("/images/" + a.Image3);

                System.IO.File.Delete(imagePath1);
                System.IO.File.Delete(imagePath2);
                System.IO.File.Delete(imagePath3);
                user.remainingAdvert += 1;
                ent.SaveChanges();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }

            Advert adv = new Advert();
            adv = (from advert in ent.Adverts where advert.IsConfirmed == false && advert.IsDeleted == false select advert).Take(1).FirstOrDefault();
            return View("Index", adv);
        }
        [Authorize(Roles = "Admin")]

        public ActionResult AddCTM()
        {
            return View();
        }

        public ActionResult AddCategory()
        {
            ViewBag.Category = ent.Categories.ToList();
            return View(); 
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory(AddCategoryViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            
            Category c = (from ctg in ent.Categories where ctg.CategoryName == model.CategoryName select ctg).FirstOrDefault();
            if(c!=null)
            {
                ModelState.AddModelError("", "Bu Kategori sistemde kayıtlı!");
                return View(model);
            }
            c = new Category();
            c.CategoryName = model.CategoryName;
            c.Description = model.Description;
            try
            {
                ent.Categories.Add(c);
                ent.SaveChanges();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                RedirectToAction("AddCategory", model);
            }
            return View("AddCTM");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AddTrademark()
        {
            ViewBag.Trademark = ent.Trademarks.ToList();
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult AddTrademark(AddTrademarkViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            Trademark t = (from trd in ent.Trademarks where trd.TrademarkName == model.TrademarkName select trd).FirstOrDefault();
            if (t!=null)
            {
                ModelState.AddModelError("", "Bu marka sistemde kayıtlı!");
                return View(model);
            }
            t = new Trademark();
            t.CategoryId = model.CategoryId;
            t.TrademarkName = model.TrademarkName;
            try
            {
                ent.Trademarks.Add(t);
                ent.SaveChanges();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                RedirectToAction("AddTrademark", model);
            }
            return View("AddCTM");
        }
        public ActionResult AddModel()
        {
            ViewBag.Model = ent.Models.ToList();
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddModel(AddModelViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            Model m = (from mdl in ent.Models where mdl.ModelName == model.ModelName select mdl).FirstOrDefault();
            if(m!=null)
            {
                return View(model);
            }
            m = new Model();
            m.ModelName = model.ModelName;
            m.Description = model.Description;
            m.TrademarkId = model.TrademarkId;                   
            try
            {
                ent.Models.Add(m);
                ent.SaveChanges();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                RedirectToAction("AddModel", model);
            }
            return View("AddCTM");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult UserOperations()
        {
            return View(ent.Users.ToList());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string Id)
        {
            var usermanager = IdentityTools.NewUserManager();
            ViewBag.Roleliste = ent.Roles.ToList();
            ApplicationUser user = (from u in ent.Users where u.Id == Id select u).FirstOrDefault();
            UserListViewModel model = new UserListViewModel();
            model.Email = user.Email;
            model.PhoneNumber = user.PhoneNumber;
            model.UserName = user.UserName;
            model.Name = user.Name;
            model.Surname = user.Surname;
            model.Role= usermanager.GetRoles(Id)[0];
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(UserListViewModel model)
        {
            var usermanager = IdentityTools.NewUserManager();
            ApplicationUser Degisecek = (from u in ent.Users where u.Id == model.Id select u).FirstOrDefault();
            
            if (model.Role!= usermanager.GetRoles(model.Id)[0])
            {
                usermanager.RemoveFromRole(Degisecek.Id, usermanager.GetRoles(model.Id)[0]);
                usermanager.AddToRole(model.Id, model.Role);
            }
            try
            {
                Degisecek.Name = model.Name;
                Degisecek.Surname = model.Surname;
                Degisecek.UserName = model.UserName;
                Degisecek.PhoneNumber = model.PhoneNumber;
                Degisecek.Email = model.Email;
                ent.SaveChanges();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
               return RedirectToAction("Edit", model);
            }
            return RedirectToAction("UserOperations");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string Id)
        {
            ApplicationUser Silinecek = (from u in ent.Users where u.Id == Id select u).FirstOrDefault();
            try
            {
                ent.Users.Remove(Silinecek);
                ent.SaveChanges();
            }
            catch (Exception ex) 
            {
                string message = ex.Message;
                throw new Exception("Silme İşlemi Başarısız Oldu");
            }
            return RedirectToAction("UserOperations");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Packets()
        {
            return View(ent.Packets.Where(x => x.IsDeleted == false).ToList()) ;
        }
        [Authorize(Roles = "Admin")]
        public ActionResult pDelete(int Id)
        {
            Packets packets = (from p in ent.Packets where p.Id == Id select p).FirstOrDefault();
            try
            {
                packets.IsDeleted = true;
                ent.SaveChanges();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw new Exception("Silme İşlemi Başarısız Oldu");
            }
            ent.Packets.Remove(packets);
            ent.SaveChanges();
            return RedirectToAction("Packets");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult pEdit(int Id)
        {
            Packets packets = (from p in ent.Packets where p.Id == Id select p).FirstOrDefault();
            return View(packets);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult pEdit(Packets model)
        {
            Packets packets = (from p in ent.Packets where p.Id == model.Id select p).FirstOrDefault();
            packets.AdRights = model.AdRights;
            packets.Price = model.Price;
            try
            {
                ent.SaveChanges();
            }
            catch (Exception ex)
            {
                string Message = ex.Message;
                //throw new Exception("Güncelleme işlemi başarısız oldu!");
                return RedirectToAction("pEdit",model);
            }
            return RedirectToAction("Packets");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult pCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult pCreate(Packets model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            Packets p = new Packets();
            p.AdRights = model.AdRights;
            p.Price = model.Price;
            try
            {
                ent.Packets.Add(p);
                ent.SaveChanges();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return RedirectToAction("pCreate",model);
            }
            return RedirectToAction("Packets");            
        }
        [Authorize(Roles = "Admin")]
        public ActionResult loadTrademark(int categoryID)
        {
            return Json(ent.Trademarks.Where(x => x.CategoryId == categoryID).Select(x => new
            {
                id = x.Id,
                Name = x.TrademarkName
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
    }    
}