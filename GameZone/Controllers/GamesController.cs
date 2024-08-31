using GameZone.Data;
using GameZone.Services;
using GameZone.viewmodel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
    
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _Context;
        private readonly ICategoriesService _CategoriesService;
        private readonly IDevicesServices _DevicesService;
        private readonly IGameServices _gameServices;
        public GamesController(ApplicationDbContext context, ICategoriesService categoriesService, IDevicesServices devicesService, IGameServices gameServices)
        {
            _Context = context;
            _CategoriesService = categoriesService;
            _DevicesService = devicesService;
            _gameServices = gameServices;
        }
        [Authorize(Roles ="admin,user")]
        public IActionResult Index()
        {
            return View(_gameServices.GetAllGames());
        }
        [Authorize(Roles = "admin,user")]
        public IActionResult Details(int id)
        {
            if(_gameServices.GetGameById(id)!=null)
                return View(_gameServices.GetGameById(id));
            else
                return NotFound();
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            CreateGameViewModel model = new()
            {
                Categories = _CategoriesService.GetSelectList(),
                Devices = _DevicesService.selectListItems()
            };
            return View(model);
        }
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(CreateGameViewModel model)
        {
            if(!ModelState.IsValid) 
                {
                model.Categories= _CategoriesService.GetSelectList();
                model.Devices= _DevicesService.selectListItems();
                return View(model);
                }
          await _gameServices.Create(model);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            var game = _gameServices.GetGameById(id);
            if (game == null)
                return NotFound();
            else
            {
                EditGameViewModel model = new()
                {
                    id = id,
                    namecover = game.Cover,
                    CategoryId = game.CategoryId,
                    Name = game.Name,
                    Description = game.Description,
                    Devices = _DevicesService.selectListItems(),
                    Categories = _CategoriesService.GetSelectList(),
                    SelectedDevices = game.Device.Select(d => d.DeviceId).ToList(),

                };

                return View(model);
            }

        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditGameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _CategoriesService.GetSelectList();
                model.Devices = _DevicesService.selectListItems();
                return View(model);
            }
            var game=await _gameServices.Update(model);
            if (game == null)
                return BadRequest();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
         var isdeleted=   _gameServices.Delete(id);
            return isdeleted ? Ok() :BadRequest();
        }
    }
}
