using ContactCar.Model;
using ContactCar.Network;
using ContactCar.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ContactCar
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        public static User myInfo = new User(); 

        public static NetworkManager networkManager = new NetworkManager();
        public static AuthViewModel authViewModel = new AuthViewModel();
        public static SaleViewModel SaleViewModel = new SaleViewModel();
        public static PictureViewModel PictureViewModel = new PictureViewModel();
    }
}
