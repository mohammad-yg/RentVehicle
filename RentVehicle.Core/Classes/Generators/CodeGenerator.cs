using System;

namespace RentVehicle.Core.Classes.Generators
{
    public static class CodeGenerator
    {
        public static string GetNewRentNumber()
        {
            var rand = new Random();
            return rand.Next(1000000).ToString("000000");
        }
    }
}