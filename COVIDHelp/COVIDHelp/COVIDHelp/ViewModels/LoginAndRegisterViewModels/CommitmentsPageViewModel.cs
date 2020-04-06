using COVIDHelp.Models;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace COVIDHelp.ViewModels.LoginAndRegisterViewModels
{
    public class CommitmentsPageViewModel
    {
        public DelegateCommand ContactCommand { get; set; }
        public DelegateCommand DoneCommand { get; set; }
        public ObservableCollection<Necesity> Historial { get; set; } = new ObservableCollection<Necesity>();
        public CommitmentsPageViewModel()
        {
            Historial.Add(new Necesity { Image = "home", NeededPerson = "Rosa", Status = "Mal" });
            Historial.Add(new Necesity { Image = "profile", NeededPerson = "Rosa", Status = "Mal" });
            Historial.Add(new Necesity { Image = "home", NeededPerson = "Rosa", Status = "Mal" });
            Historial.Add(new Necesity { Image = "profile", NeededPerson = "Rosa", Status = "Mal" });
            Historial.Add(new Necesity { Image = "home", NeededPerson = "Rosa", Status = "Mal" });
            Historial.Add(new Necesity { Image = "profile", NeededPerson = "Rosa", Status = "Mal" });
            Historial.Add(new Necesity { Image = "home", NeededPerson = "Rosa", Status = "Mal" });
            Historial.Add(new Necesity { Image = "profile", NeededPerson = "Rosa", Status = "Mal" });

            ContactCommand = new DelegateCommand(() =>
            {


            });

            DoneCommand = new DelegateCommand(() =>
            {


            });
        }
    }
}
