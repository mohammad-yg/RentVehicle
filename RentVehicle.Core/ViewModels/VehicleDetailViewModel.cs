using System.ComponentModel;

namespace RentVehicle.Core.ViewModels
{
    public class VehicleDetailViewModel
    {
        [DisplayName(" نام وسیله نقلیه")]
        public string Name { get; set; }
        [DisplayName("قیمت اجاره روزانه")]
        public string Price { get; set; }
        [DisplayName("نوع وسیله نقلیه")]
        public string VehicleType { get; set; }
        [DisplayName("وضعیت اجاره")]
        public bool IsRented { get; set; }
        [DisplayName("وضعیت")]
        public bool IsRemoved { get; set; }
        [DisplayName("تاریخ ایجاد")]
        public string CreateDate { get; set; }
        [DisplayName("تاریخ حذف")]
        public string RemoveDate { get; set; }
        [DisplayName("نام ایجاد کننده")]
        public string CreatorName { get; set; }
    }
}