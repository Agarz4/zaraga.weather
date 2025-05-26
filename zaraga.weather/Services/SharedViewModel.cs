using System.Threading.Tasks;
using zaraga.devicefeatures;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;
using zaraga.weather.Services;

namespace zaraga.weather
{
    internal class SharedViewModel : PropertyChangedViewModel
    {
        private readonly DeviceSensors _devicefeatures;

        internal SharedViewModel()
        {
            _devicefeatures = new DeviceSensors();
        }

        /// <summary>
        /// Obtiene todos los permisos necesarios
        /// </summary>
        /// <returns></returns>
        internal async Task GetAppPermissions()
        {
            App.Log("Getting location in use permission");
            var locationPermission = await _devicefeatures.CheckAndRequestPermissionAsync(new Permissions.LocationWhenInUse());
            App.Log($"Location permission status: {locationPermission}");
        }

        /// <summary>
        /// Obtiene la ubicacion actual del dispositivo si se cuenta con el permiso.
        /// </summary>
        /// <returns></returns>
        internal async Task<Location?> GetDeviceLocation()
        {
            App.Log("Checking location in use permission");
            var locationPermission = await _devicefeatures.CheckPermissionStatus(new Permissions.LocationWhenInUse());
            App.Log($"Location permission status: {locationPermission}");
            if (locationPermission == PermissionStatus.Granted)
            {
                App.Log("Getting Current location");
                var deviceLocation = await _devicefeatures.GetCurrentLocation();
                App.Log($"Current location: Latitude:{deviceLocation?.Latitude}, Longitude:{deviceLocation?.Longitude}");
                return deviceLocation;
            }
            else
            {
                // Permission denied, handle accordingly
                await Shell.Current.DisplayAlert("Permiso denegado", "No se cuenta con el permiso para acceder a la ubicación del dispositivo", "OK");
                return null;
            }
        }
    }
}
