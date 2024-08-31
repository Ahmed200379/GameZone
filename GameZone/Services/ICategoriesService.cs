using GameZone.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services
{
 
    public interface ICategoriesService
    {
        public IEnumerable<SelectListItem> GetSelectList();
        public void Create(Category category);
    }
}
