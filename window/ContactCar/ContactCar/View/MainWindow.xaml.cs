using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ContactCar.Model;

namespace ContactCar
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LoginControl.ShowSignUpCtrl += LoginControl_ShowSignUpCtrl;
            App.authViewModel.LoginResult += AuthViewModel_LoginResultAsync;
        }

        private void LoginControl_ShowSignUpCtrl(object sender, EventArgs e)
        {
            App.authViewModel.Desc = string.Empty;
            SignUpControl.Visibility = Visibility.Visible;
        }

        private async Task AuthViewModel_LoginResultAsync(bool success, User myInfo)
        {
            if (success)
            {
                App.myInfo = myInfo;

                LoginControl.Visibility = Visibility.Collapsed;
                HomeControl.Visibility = Visibility.Visible;

                await LoadDataAsync();
            }
        }

        private async Task LoadDataAsync()
        {
            await GetMemberData();
            await App.SaleViewModel.GetDataAsync();
            await App.PictureViewModel.GetDataAsync();
        }

        private async Task GetMemberData()
        {
            await App.authViewModel.GetDataAsync();
            App.myInfo = App.authViewModel.Items.Where(u => u.UserId.Equals(App.authViewModel.Id)).FirstOrDefault();
        }
    }
}
