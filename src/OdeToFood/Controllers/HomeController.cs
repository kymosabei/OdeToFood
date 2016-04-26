using Microsoft.AspNet.Mvc;
using OdeToFood.ViewModels;
using OdeToFood.Services;
using OdeToFood.Entities;
using Microsoft.AspNet.Authorization;

namespace OdeToFood.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IGreeter _greeter;
        private IRestaurantData _restuarantData;

        public HomeController(IRestaurantData restaurantData, IGreeter greeter)
        {
            _restuarantData = restaurantData;
            _greeter = greeter;
        }

        [AllowAnonymous]
        public ViewResult Index() //=> Content("Hola from le controller!");
        {

            var model = new HomePageViewModel();
            model.Restaurants = _restuarantData.GetAll();
            model.CurrentGreeting = _greeter.GetGreeting();

            return View(model);


            //var model = new Restaurant
            //{
            //    Id = 1,
            //    Name = "WF Cody's"
            //};

            //var model = _restuarantData.GetAll();

            //return Content("Hola from le controller!");

            //return new ObjectResult(model);
        }

        public IActionResult Details(int id)
        {
            var model = _restuarantData.Get(id);

            if (model == null)
                return RedirectToAction(nameof(HomeController.Index));
            //return HttpNotFound();

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _restuarantData.Get(id);

            if (model == null)
                return RedirectToAction(nameof(HomeController.Index));

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(int id, RestaurantEditViewModel input)
        {
            var restaurant = _restuarantData.Get(id);
            if (restaurant != null && ModelState.IsValid)
            {
                restaurant.Name = input.Name;
                restaurant.Cuisine = input.Cuisine;

                _restuarantData.Commit();

                return RedirectToAction(nameof(HomeController.Details), new { id = restaurant.Id });
            }

            return View(restaurant);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(RestaurantEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var restaurant = new Restaurant
                {
                    Name = model.Name,
                    Cuisine = model.Cuisine,
                };

                _restuarantData.Add(restaurant);
                _restuarantData.Commit();

                return RedirectToAction(nameof(HomeController.Details), new { id = restaurant.Id }); 
            }

            return View();

        }
    }
}
