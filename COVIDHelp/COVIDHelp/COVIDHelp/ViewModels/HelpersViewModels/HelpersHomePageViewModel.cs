using COVIDHelp.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace COVIDHelp.ViewModels.HelpersViewModels
{
    public class HelpersHomePageViewModel : BaseViewModel
    {
        public DelegateCommand GoToHelpPage { get; set; }
        public ObservableCollection<Necesity> Necesities { get; set; } = new ObservableCollection<Necesity>();
        public HelpersHomePageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Necesities.Add(new Necesity { Status = "Mal", Image = "commitment", NeededPerson = "Rosa Perez" });
            Necesities.Add(new Necesity { Status = "Mal", Image = "home", NeededPerson = "Rosa Perez" });
            Necesities.Add(new Necesity { Status = "Mal", Image = "profile", NeededPerson = "Rosa Perez" });
            Necesities.Add(new Necesity { Status = "Mal", Image = "commitment", NeededPerson = "Rosa Perez" });
            GoToHelpPage = new DelegateCommand(async () =>
            {
                await navigationService.NavigateAsync("HelpPage");
            });


        }
    }
}
