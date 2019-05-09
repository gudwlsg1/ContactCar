using ContactCar.Model;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCar.ViewModel
{
    public class UserViewModel : BindableBase
    {
        #region Property
        private User _userInfo;
        public User UserInfo
        {
            get => _userInfo;
            set => SetProperty(ref _userInfo, value);
        }
        #endregion
    }
}
