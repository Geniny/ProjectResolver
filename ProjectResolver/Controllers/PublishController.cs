using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectResolver.Controllers
{
    public class PublishController : Controller
    {
        // GET: PublishController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PublishController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PublishController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PublishController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PublishController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PublishController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PublishController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PublishController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
