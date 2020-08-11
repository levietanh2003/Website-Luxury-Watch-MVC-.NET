using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDongHo.Models.Entities;

namespace WebBanDongHo.Controllers
{
    public class GioiThieuController : Controller
    {
        WebBanDongHoDbContext db = new WebBanDongHoDbContext();
        // GET: GioiThieu
        public ActionResult GioiThieu()
        {
            ThongTin model = db.ThongTins.ToList().First();
            return View(model);
        }
    }
}