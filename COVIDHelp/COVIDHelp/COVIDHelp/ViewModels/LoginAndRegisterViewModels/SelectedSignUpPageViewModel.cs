using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace COVIDHelp.ViewModels
{
    public class SelectedSignUpPageViewModel : BaseViewModel
    {
        public DelegateCommand ButtonRegisterNeddedCommand { get; set; }
        public DelegateCommand ButtonRegisterHelpersCommand { get; set; }
        public SelectedSignUpPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            ButtonRegisterNeddedCommand = new DelegateCommand(async () => {
                // guardar la data del user y registrarlo, rol = needed(necesita ayuda.)
            });
            ButtonRegisterHelpersCommand = new DelegateCommand(async () => {
                // guardar la data del user y registrarlo, rol = helpers(voluntario.)
            });
        }
    }
}
