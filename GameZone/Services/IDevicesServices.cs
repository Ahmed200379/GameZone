using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services
{
    public interface IDevicesServices
    {
        public IEnumerable<SelectListItem> selectListItems();
    }
}
