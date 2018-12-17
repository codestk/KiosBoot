using KiosBoot.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiosBoot.ViewModels
{
    public class ScrollGameInfoMovel : ObservableObject
    {

        public ObservableCollection<ScrollInfo> ScrollInfo { get; private set; }
 

    }
}
