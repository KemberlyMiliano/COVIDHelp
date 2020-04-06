using COVIDHelp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace COVIDHelp.ViewModels.LoginAndRegisterViewModels
{
    public class CommitmentsPageViewModel
    {
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
        }
    }
}
