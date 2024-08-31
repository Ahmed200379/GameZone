using GameZone.Models;
using GameZone.viewmodel;

namespace GameZone.Services
{
    public interface IGameServices
    {
        public Task Create(CreateGameViewModel model);
        public IEnumerable<Game> GetAllGames();
        public Game? GetGameById(int id);
        public Task<Game?> Update(EditGameViewModel model);
        public bool Delete(int id);
    }
}
