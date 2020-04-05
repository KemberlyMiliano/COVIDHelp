using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace COVIDHelp.ViewModels.NeededViewModels
{
    public class NeededHomePageViewModel:BaseViewModel
    {
        public DelegateCommand GoToNeedHelp { get; set; }
        public NeededHomePageViewModel(INavigationService navigationService, IPageDialogService dialogService):base(navigationService, dialogService)
        {
            this.navigationService = navigationService;
            GoToNeedHelp = new DelegateCommand(async () =>
            {
                await NavigateToNeed();
            });
        }
        async Task NavigateToNeed()
        {
            await navigationService.NavigateAsync(new Uri($"/SelectNeededPage",UriKind.Relative));
        }
    }
}
