using GameZone.settings;
using GameZone.Validation;

namespace GameZone.viewmodel
{
    public class EditGameViewModel:GameViewModel
    {
        public int id {  get; set; }
        public string? namecover { get; set; }
        [AllowedExtention(ConstantsNeeded.AllowExtentions),
           MaxSize(ConstantsNeeded.maxSizeByByets)]
        public IFormFile? Cover { get; set; }
    }
}
