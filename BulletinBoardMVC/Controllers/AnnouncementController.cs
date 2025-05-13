using BulletinBoardMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace BulletinBoardMVC.Controllers
{
    public class AnnouncementController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7071/api");
        private readonly HttpClient _client;
        private readonly Dictionary<string, List<string>> _categorySubcategories = new Dictionary<string, List<string>>
        {
            {"Побутова техніка", new List<string>{"Холодильники", "Пральні машини", "Бойлери", "Печі", "Витяжки", "Мікрохвильові печі"}},
            {"Комп'ютерна техніка", new List<string>{"ПК", "Ноутбуки", "Монітори", "Принтери", "Сканери"}},
            {"Смартфони", new List<string>{"Android смартфони", "iOS/Apple смартфони"}},
            {"Інше", new List<string>{"Одяг", "Взуття", "Аксесуари", "Спортивне обладнання", "Іграшки"}}
        };

        public AnnouncementController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        public IActionResult Index(string category = null, string subcategory = null)
        {
            List<AnnouncementViewModel> announcementsList = GetAllAnnouncementsFromApi();

            announcementsList = announcementsList.Where(a =>
                (string.IsNullOrEmpty(category) || a.Category == category) &&
                (string.IsNullOrEmpty(subcategory) || a.SubCategory == subcategory)
            ).ToList();

            ViewBag.Categories = _categorySubcategories.Keys.ToList();
            ViewBag.CategorySubcategories = JsonConvert.SerializeObject(_categorySubcategories);
            ViewBag.SelectedCategory = category;
            ViewBag.SelectedSubcategory = subcategory;

            return View(announcementsList);
        }

        private List<AnnouncementViewModel> GetAllAnnouncementsFromApi()
        {
            List<AnnouncementViewModel> announcementsList = new List<AnnouncementViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/announcement/Get").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                announcementsList = JsonConvert.DeserializeObject<List<AnnouncementViewModel>>(data);
            }
            return announcementsList;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AnnouncementViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress +
                    "/Announcement/Post", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Announcement Created";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                AnnouncementViewModel announcement = new AnnouncementViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress +
                    "announcement/Get/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    announcement = JsonConvert.DeserializeObject<AnnouncementViewModel>(data);
                }
                return View(announcement);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(AnnouncementViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress +
                    "/Announcement/Put", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Announcement details updated";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                AnnouncementViewModel announcement = new AnnouncementViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress +
                    "/announcement/Get/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    announcement = JsonConvert.DeserializeObject<AnnouncementViewModel>(data);
                }
                return View(announcement);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress +
                        "/announcement/Delete/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Announcement details deleted successfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

    }
}

    
