using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zikunov.ServiceStation.WebApp.Areas.Identity.Data;
using Zikunov.ServiceStation.WebApp.Data;

namespace Zikunov.ServiceStation.WebApp.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ZikunovServiceStationWebAppContext _db;

        public ServiceController(ZikunovServiceStationWebAppContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var objServiceList = _db.Users.ToList();

            return View(objServiceList);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ZikunovServiceStationWebAppUser obj)
        {
            if (ModelState.IsValid)
            {
                _db.Users.Add(obj);
                 _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var currentUserId = _db.Users.Find(id);

            if (currentUserId == null)
            {
                return NotFound();
            }

            return View(currentUserId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ZikunovServiceStationWebAppUser obj)
        {
            if (ModelState.IsValid)
            {
                _db.Users.Update(obj);
                _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Users.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Users.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Users.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
