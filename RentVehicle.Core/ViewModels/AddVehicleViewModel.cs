using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentVehicle.Core.ViewModels
{
    public class AddVehicleViewModel
    {
        [DisplayName(" نام وسیله نقلیه")]
        [MinLength(3, ErrorMessage = "حداقل 3 نویسه وارد کنید")]
        [MaxLength(40, ErrorMessage = "حداکثر 40 نویسه وارد کنید")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "این فید را پر کنید")]
        public string Name { get; set; }

        [DisplayName("نوع وسیله نقلیه")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "این فید را پر کنید")]
        public string VehicleType { get; set; }

        [DisplayName("قیمت اجاره روزانه")]
        [RegularExpression(@"^\d{1,5}/\d{1,3}$|^\d{1,5}$", ErrorMessage = "فرم وارد شده صحیح نیست )مثال : 3.45)")]
        public string Price { get; set; }
    }
}