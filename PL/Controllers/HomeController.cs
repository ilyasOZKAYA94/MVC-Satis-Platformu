using BLL.Repository;
using DAL.Context;
using Entity.Entity;
using Entity.Identity;
using Entity.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class HomeController : BaseController
    {
        SatiyorumContext ent = new SatiyorumContext();
        Repository<Advert> repoA = new Repository<Advert>(new SatiyorumContext());
        public ActionResult Index(int? Id)
        {

            if (Id != null)
            {
                List<Advert> liste = new List<Advert>();              
                Trademark t = (from trd in ent.Trademarks where trd.Id == Id select trd).FirstOrDefault();
                if(t!=null)
                {
                    foreach (Model item in t.Models)
                    {
                        foreach (Advert advert in item.Adverts)
                        {
                            if(advert.IsDeleted==false)
                            {
                                liste.Add(advert);
                                return View(liste);
                            }
                            
                        }
                    }
                }                
            }
            return View(repoA.GetAll(x => x.IsConfirmed == true && x.IsSold == false && x.IsDeleted == false));
        }

        public ActionResult Single(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Advert adv = (from a in ent.Adverts where a.Id == Id select a).FirstOrDefault();
            ApplicationUser Seller = (from u in ent.Users where u.Id == adv.SellerId select u).FirstOrDefault();
            SingleViewModel model = new SingleViewModel();
            model.Color = adv.Color;
            model.CreateDate = adv.CreateDate;
            model.Description = adv.Description;
            model.Image1 = adv.Image1;
            model.Image2 = adv.Image2;
            model.Image3 = adv.Image3;
            model.Price = adv.Price;
            model.ProductName = adv.ProductName;
            model.SellerId = Seller.Id;
            model.SellerUserName = Seller.UserName;
            model.Views = adv.Views;
            model.Warranty = adv.Warranty;
            if (Id == 0)
                return RedirectToAction("Index");
            else
            {
                try
                {
                    adv.Views += 1;
                    ent.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }

                return View(model);
            }
        }   
        
        

        public ActionResult Login()
        {
            return View();
        }


        public ActionResult Contact()
        {
            return View();
        }
        [Authorize]
        public ActionResult AddAdvert()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        [Authorize]
        public ActionResult AddAdvert(AddAdvert model)
        {
           // Repository<Advert> repoA = new Repository<Advert>(new SatiyorumContext());            
            if (model.PictureUpload[1] != null)
            {
                string Id = HttpContext.User.Identity.GetUserId();
                ApplicationUser user = (from u in ent.Users where u.Id == Id select u).FirstOrDefault();               
                if(user.remainingAdvert==0)
                {
                    return RedirectToAction("Packets");
                }
                int i = 0;
                foreach (var resim in model.PictureUpload)
                {
                    string filename = model.PictureUpload[i].FileName;
                    string imagePath = Server.MapPath("/images/" + filename);
                    model.PictureUpload[i].SaveAs(imagePath);
                    i++;
                }               
                
                Advert yeni = new Advert();
                yeni.Title = model.Title;
                yeni.ProductName = model.ProductName;
                yeni.Color = model.Color;
                yeni.Warranty = model.Warranty;
                yeni.Description = model.Description;
                yeni.Price = model.Price;
                yeni.Image1 = model.PictureUpload[0].FileName;
                yeni.Image2 = model.PictureUpload[1].FileName;
                yeni.Image3 = model.PictureUpload[2].FileName;
                yeni.SellerId = user.Id;
                yeni.ModelId = model.ModelId;
                if (repoA.Add(yeni))
                {
                    user.remainingAdvert -= 1;
                    ent.SaveChanges();
                    return RedirectToAction("Index");
                }                  
                return View(model);
            }
            return View();
        }

        public ActionResult loadModel(int trademarkID)
        {
            return Json(ent.Models.Where(x => x.TrademarkId == trademarkID).Select(x => new
            {
                id = x.Id,
                Name = x.ModelName
            }).ToList(), JsonRequestBehavior.AllowGet);

        }


    }
}