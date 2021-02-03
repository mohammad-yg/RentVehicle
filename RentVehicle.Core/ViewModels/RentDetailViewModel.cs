using System.ComponentModel;

namespace RentVehicle.Core.ViewModels
{
    public class RentDetailViewModel
    {
        [DisplayName("نام خریدار")]
        public string BuyerName { get; set; }
        [DisplayName("نام وسیله نقلیه")]
        public string VehicleName { get; set; }
        [DisplayName("کد اجراه نامه")]
        public string Number { get; set; }
        [DisplayName("مبلغ پرداختی")]
        public string Price { get; set; }
        [DisplayName("مدت زمان اجاره (روز)")]
        public string Day { get; set; }
        [DisplayName("وضعیت اجاره نامه")]
        public bool IsCanceled { get; set; }
        [DisplayName("تاریخ ثبت")]
        public string RentDate { get; set; }
        [DisplayName("تاریخ لغو")]
        public string CancelDate { get; set; }
    }
}