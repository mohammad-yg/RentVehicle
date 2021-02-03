using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentVehicle.Core.ViewModels
{
    public class RegisterUserViewModel
    {
        [DisplayName("نام کاربری")]
        [MinLength(3, ErrorMessage = "حداقل 3 نویسه وارد کنید")]
        [MaxLength(40, ErrorMessage = "حداکثر 40 نویسه وارد کنید")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "این فید را پر کنید")]
        public string Name { get; set; }
    }
}