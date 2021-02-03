var vehicleElement = document.getElementById('VehicleName');
var DayElement = document.getElementById("Day");

vehicleElement.addEventListener('change', handler);
DayElement.addEventListener('change', handler);

var price = parseFloat(vehicleElement.options[vehicleElement.selectedIndex].getAttribute('price').replace("/", "."));
var day = parseInt(DayElement.options[DayElement.selectedIndex].value);
document.getElementById("TotalPrice").value = price * day;

function handler(event) {
    var price = parseFloat(vehicleElement.options[vehicleElement.selectedIndex].getAttribute('price').replace("/","."));
    var day = parseInt(DayElement.options[DayElement.selectedIndex].value);
    document.getElementById("TotalPrice").value = price * day;
}