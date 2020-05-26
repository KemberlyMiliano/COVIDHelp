using COVIDHelp.Helpers;
using COVIDHelp.Models;
using COVIDHelp.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using System;
using System.Threading.Tasks;

namespace COVIDHelp.ViewModels
{
    public class EditProfilePageViewModel : BaseViewModel,IInitialize
    {
        public User User { get; set; }
        public DelegateCommand EditCommand { get=> new DelegateCommand(async () =>
        {
            await PostPhoto(User);
            User = await userServices.UpdateUser(User, Setting.Token);
            await navigationService.GoBackAsync();
           
        }); }

        public DelegateCommand PictureCommand { get=> new DelegateCommand(async () =>
        {
            await TakeImage();
        }); }
        public EditProfilePageViewModel(INavigationService navigationService, IPageDialogService dialogService, ICovidUserServices userServices,IHelpServices helpServices, IDialogService dialog) : base(navigationService, dialogService,userServices, helpServices)
        {
        }

        async Task TakeImage()
        {
            const string takePhoto = "Take photo";
            const string ChossePhoto = "Chosse photo";
            MediaFile file = null;
            var dialog = await dialogService.DisplayActionSheetAsync("What do you want?", null, "Cancel", takePhoto, ChossePhoto);
            switch (dialog)
            {
                case takePhoto:
                    {
                        file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions { PhotoSize = PhotoSize.Medium });
                        User.ProfilePhoto = file.Path;
                        break;
                    }
                    
                case ChossePhoto:
                    {
                        file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { PhotoSize = PhotoSize.Medium });
                        User.ProfilePhoto = file.Path;
                        break;
                    }
                    

                default:
                    break;
            }
        }
        async Task PostPhoto(User user)
        {
            if (User.ProfilePhoto!=null)
            {
                await userServices.PostPhoto(user.Phone, user, Setting.Token);
            }
        } 
        public void Initialize(INavigationParameters parameters)
        {
            var user = parameters[Constants.PersonKey] as User;
            User = user;
        }
    }

}
