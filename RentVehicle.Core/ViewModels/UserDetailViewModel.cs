using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using RentVehicle.Core.Classes.Convertors;

namespace RentVehicle.Core.ViewModels
{
    public class UserDetailViewModel
    {

        [DisplayName("نام کاربری")]
        [MinLength(3, ErrorMessage = "حداقل 3 نویسه وارد کنید")]
        [MaxLength(40, ErrorMessage = "حداکثر 40 نویسه وارد کنید")]
        [Required(AllowEmptyStrings = false , ErrorMessage = "این فید را پر کنید")]
        public string Name { get; set; }

        [DisplayName("تاریخ ثبت نام")]
        public string RegisterDate { get; set; }

        [DisplayName("وضعیت")]
        public bool IsRemoved { get; set; }

        [DisplayName("تاریخ حذف")]
        public string RemoveDate { get; set; }
    }
}