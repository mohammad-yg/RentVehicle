using System.ComponentModel;

namespace RentVehicle.Core.ViewModels
{
    public class AddRentViewModel
    {
        [DisplayName("نام خودرو")]
        public string VehicleName { get; set; }
        [DisplayName("مدت اجاره (روز)")]
        public int Day { get; set; }
    }
}