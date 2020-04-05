using COVIDHelp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace COVIDHelp.ViewModels.HelpersViewModels
{
    public class CommitmentsPageViewModel
    {
        public ObservableCollection<Commitment> Commitments { get; set; } = new ObservableCollection<Commitment>();
        public CommitmentsPageViewModel()
        {
            Commitments.Add(new Commitment { Name = "nsdcui", NecesityImage = "home", Necesity = "dushdcuiwdc" });
            Commitments.Add(new Commitment { Name = "nsdcui", NecesityImage = "home", Necesity = "dushdcuiwdc" });
            Commitments.Add(new Commitment { Name = "nsdcui", NecesityImage = "home", Necesity = "dushdcuiwdc" });
            Commitments.Add(new Commitment { Name = "nsdcui", NecesityImage = "home", Necesity = "dushdcuiwdc" });
            Commitments.Add(new Commitment { Name = "nsdcui", NecesityImage = "home", Necesity = "dushdcuiwdc" });
        }
    }
}
