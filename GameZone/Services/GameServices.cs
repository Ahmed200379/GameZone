using GameZone.Data;
using GameZone.Models;
using GameZone.settings;
using GameZone.viewmodel;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class GameServices : IGameServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;
        public GameServices(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{ConstantsNeeded.ImagesPath}";
        }
        public IEnumerable<Game> GetAllGames()
        {
            return _context.Games.Include(g=>g.Category).Include(i=>i.Device).ThenInclude(i => i.Device).AsNoTracking().ToList();
        }
        public Game? GetGameById(int id)
        {
            return _context.Games.Include(g => g.Category).Include(i => i.Device).ThenInclude(i => i.Device).FirstOrDefault(i=>i.Id==id);
        }
        public async Task Create(CreateGameViewModel model)
        {
            var covername = await SaveCover(model.Cover);
            Game game = new()
            {
                Cover = covername,
                CategoryId = model.CategoryId,
                Description = model.Description,
                Device = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList(),
                Name = model.Name,
            };
            _context.Games.Add(game);
            _context.SaveChanges();
        }
        public async Task<String> SaveCover(IFormFile Cover)
        {
            var covername = $"{Guid.NewGuid()}{Path.GetExtension(Cover.FileName)}";
            var path = Path.Combine(_imagesPath, covername);
            using var stream = File.Create(path);
            await Cover.CopyToAsync(stream);
            return (covername);
        }
        public async Task<Game?> Update(EditGameViewModel model)
        {
            var game=_context.Games.Include(d=>d.Device).SingleOrDefault(s=>s.Id==model.id);
            var oldcover = game.Cover;
            if (game == null)
                return null;
            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Device=model.SelectedDevices.Select(d => new GameDevice {DeviceId=d }).ToList();
            if(model.Cover!=null)
            {
                game.Cover = await SaveCover(model.Cover);
            }
            var affectedrows=_context.SaveChanges();
            if(affectedrows>0)
            {
                if (model.Cover != null){
                    var cover = Path.Combine(_imagesPath, oldcover);
                    File.Delete(cover);
                }
                return game;
            }
            else
            {
                var cover = Path.Combine(_imagesPath, game.Cover);
                File.Delete(cover);
                return null;
            }
        }

        public bool Delete(int id)
        {
            var isDeleted = false;
            var game=_context.Games.SingleOrDefault(d=>d.Id==id);
            if (game==null) return isDeleted;
            _context.Games.Remove(game);
            var effectedrows=_context.SaveChanges();
            if (effectedrows > 0)
            {
                isDeleted= true;
                var cover = Path.Combine(_imagesPath, game.Cover);
                File.Delete(cover);
            }
            return isDeleted;
        }
    }
}
