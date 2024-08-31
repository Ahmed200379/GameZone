using GameZone.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using GameZone.settings;
namespace GameZone.viewmodel
{
    public class CreateGameViewModel:GameViewModel
    {
        [AllowedExtention(ConstantsNeeded.AllowExtentions),
            MaxSize(ConstantsNeeded.maxSizeByByets)]
        public IFormFile Cover { get; set; } = default!;
    }
}
