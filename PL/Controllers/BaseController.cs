using BLL.Repository;
using DAL.Context;
using Entity.Entity;
using Entity.Identity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class BaseController : Controller
    {
        SatiyorumContext ent = new SatiyorumContext();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Repository<Category> repoC = new Repository<Category>(ent);
            ViewBag.Category = repoC.GetAll();

            Repository<Model> repoM = new Repository<Model>(ent);
            ViewBag.Model = repoM.GetAll();

            Repository<Trademark> repoT = new Repository<Trademark>(ent);
            ViewBag.Trademark = repoT.GetAll();

            

            base.OnActionExecuting(filterContext);
        }
    }
}