using COVIDHelp.Helpers;
using COVIDHelp.Models;
using COVIDHelp.Services;
using MonkeyCache.FileStore;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace COVIDHelp.ViewModels
{
    public class SelectAssistenceDoctorViewModel
    {
        public ObservableCollection<Diseases> Diseases { get; set; }
        public Diseases DiseasesAdd { get; set; }
        public DelegateCommand AddDataAndNavigateCommand { get; set; }
        private bool isEnable;

        public bool IsEnable
        {
            get
            {

                return isEnable;


            }
            set
            {
                isEnable = value;

            }
        }
        public SelectAssistenceDoctorViewModel()
        {
            Diseases = new ObservableCollection<Diseases>()
            {
                new Diseases { Name = "Fiebre" },
                new Diseases { Name = "Dolor de cabeza" },
                new Diseases { Name = "Dificultad para respirar" },
            };

            AddDataAndNavigateCommand = new DelegateCommand(async () =>
            {
                AddDiseases();
                SaveData();
               await  NavigateTo();

            });
        }

        void AddDiseases()
        {
            Diseases.Add(DiseasesAdd);
        }
        void SaveData()
        {
            Barrel.ApplicationId = ConfigApi.MonkeyChadeKey;
            Barrel.Current.Add(key: $"DataUserAssistenceDoctor", data: Diseases, expireIn: TimeSpan.FromDays(31));
        }
        async Task NavigateTo()
        {
            
        }
    }
}
