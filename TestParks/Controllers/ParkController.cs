using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestParks.Models;

namespace TestParks.Controllers
{
    public class ParkController : Controller
    {
        
        public ActionResult Index()
        {
            
            List<Park> parks = RetrieveNationalParks();

            return View(parks);
        }

        private List<Park> RetrieveNationalParks()
        {
            string jsonUrl = "https://raw.githubusercontent.com/GiorgioMotorola/ParksJson/main/json";

            using (HttpClient client = new HttpClient())
            {
                string jsonData = client.GetStringAsync(jsonUrl).GetAwaiter().GetResult();
                var parks = JsonConvert.DeserializeObject<List<Park>>(jsonData);

                return parks;
            }
        }

        public ActionResult Details(int id)
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
