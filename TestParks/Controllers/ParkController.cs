using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using TestParks.Models;

namespace TestParks.Controllers
{
    public class ParkController : Controller
    {

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {

            List<Park> parks = RetrieveNationalParks();

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["StateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "state_desc" : "";



            if (!String.IsNullOrEmpty(searchString))
            {
                parks = parks.Where(p => p.State.ToLower().Contains(searchString.ToLower())).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    parks = parks.OrderByDescending(i => i.Name).ToList();
                    break;
                case "state_desc":
                    parks = parks.OrderByDescending(i => i.State).ToList();
                    break;
                case "state_asc":
                    parks = parks.OrderBy(i => i.State).ToList();
                    break;
                case "state":
                    parks = parks.OrderBy(i => i.State).ToList();
                    break;
                default:
                    parks = parks.OrderBy(i => i.Name).ToList();
                    break;
            }


            return View(parks);
        }

        public List<Park> RetrieveNationalParks()
        {
            string jsonUrl = "https://raw.githubusercontent.com/GiorgioMotorola/ParksJson/main/json";

            using (HttpClient client = new HttpClient())
            {
                string jsonData = client.GetStringAsync(jsonUrl).GetAwaiter().GetResult();
                var parks = JsonConvert.DeserializeObject<List<Park>>(jsonData);

                return parks;
            }
        }

        public async Task<IActionResult> Details(int id)
        {

            List<Park> parks = RetrieveNationalParks();


            Park park = parks.FirstOrDefault(p => p.Id == id);

            if (park == null)
            {
                return NotFound();
            }

            return View(park);
        }

        public ActionResult Create()
        {
            return View();
        }


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

        public ActionResult Edit(int id)
        {
            return View();
        }


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


        public ActionResult Delete(int id)
        {
            return View();
        }


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
