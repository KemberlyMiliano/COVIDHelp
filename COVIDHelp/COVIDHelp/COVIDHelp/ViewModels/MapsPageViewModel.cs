using COVIDHelp.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;

namespace COVIDHelp.ViewModels
{
    public class MapsPageViewModel:BaseViewModel,INavigationAware
    {
        public ObservableCollection<Location> FoodsPin { get; set; }
        public ObservableCollection<Location> PharmacyPin { get; set; }
        public ObservableCollection<Location> HelpersPin { get; set; }
        public ObservableCollection<Location> NeedPersonPin { get; set; }

        public DelegateCommand LoadPins { get; set; }
        Geocoder geoCoder = new Geocoder();
        public MapsPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
        }
        async Task<Position> GetPosition(string direccion)
        {
            IEnumerable<Position> approximateLocations = await geoCoder.GetPositionsForAddressAsync(direccion);
            Position position = approximateLocations.FirstOrDefault();
            return new Position(position.Latitude, position.Longitude);
        }
        void LoadFoodsPins()
        {
            FoodsPin = new ObservableCollection<Location>()
            {
            };
        }
        void LoadPharmacyPins()
        {
            FoodsPin = new ObservableCollection<Location>()
            {
            };
        }
        void LoadHelpersPins()
        {
            HelpersPin = new ObservableCollection<Location>()
            {
            };
        }
        void LoadNeedPersonPins()
        {
            NeedPersonPin = new ObservableCollection<Location>()
            {
            };
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            var param = parameters["GoToMaps"] as string;
            switch (param)
            {
                case "Foods":
                    {
                        LoadPins = new DelegateCommand(() => {
                            LoadFoodsPins();
                        });
                        LoadPins.Execute();
                        break;
                    }
                case "Medicines":
                    {
                        LoadPins = new DelegateCommand( () => {
                            LoadPharmacyPins();
                        });
                        LoadPins.Execute();
                        break;
                    }
                case "Self":
                    {
                        LoadPins = new DelegateCommand( () => {
                            LoadHelpersPins();
                        });
                        LoadPins.Execute();
                        break;
                    }
                case "NeedPerson":
                    {
                        LoadPins = new DelegateCommand(() => {
                            LoadNeedPersonPins();
                        });
                        LoadPins.Execute();
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
