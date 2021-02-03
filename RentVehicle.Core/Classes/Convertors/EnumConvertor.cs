using RentVehicle.DataLayer.Entities;

namespace RentVehicle.Core.Classes.Convertors
{
    public static class EnumConvertor
    {
        public static string ToPersianText(this VehicleType type)
        {
            return type switch
            {
                VehicleType.Car => "خودرو",
                VehicleType.MotorSiclet => "موتور",
                _ => ""
            };
        }
    }
}