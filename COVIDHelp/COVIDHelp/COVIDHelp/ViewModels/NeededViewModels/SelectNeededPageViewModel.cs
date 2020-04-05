using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace COVIDHelp.ViewModels.NeededViewModels
{
    public class SelectNeededPageViewModel:BaseViewModel
    {
        public DelegateCommand<string>GoToMaps { get; set; }
        public SelectNeededPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            GoToMaps = new DelegateCommand<string>(async (filter) =>
            {
                var param = new NavigationParameters
                {
                    { nameof(GoToMaps), filter }
                };

                await navigationService.NavigateAsync(new Uri("/MapsPage",UriKind.Relative),param);
            });
        }
    }
}
