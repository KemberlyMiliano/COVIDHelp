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
     public class SelectAssistencePsycologyViewModel
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
        public SelectAssistencePsycologyViewModel()
        {
            Diseases = new ObservableCollection<Diseases>()
            {
                new Diseases { Name = "Ansiedad" },
                new Diseases { Name = "Trastornos alimenticios" },
                new Diseases { Name = "Paranoia" },
            };

            AddDataAndNavigateCommand = new DelegateCommand(async () =>
            {
                AddDiseases();
                SaveData();
                await NavigateTo();

            });
        }

        void AddDiseases()
        {
            Diseases.Add(DiseasesAdd);
        }
        void SaveData()
        {
            Barrel.ApplicationId = ConfigApi.MonkeyChadeKey;
            Barrel.Current.Add(key: $"DataUserAssistencePsycology", data: Diseases, expireIn: TimeSpan.FromDays(31));
        }
        async Task NavigateTo()
        {

        }
    }
}
