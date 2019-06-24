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

namespace ContactCar.Controls
{
    /// <summary>
    /// Interaction logic for HomeControl.xaml
    /// </summary>
    public partial class HomeControl : UserControl
    {
        public HomeControl()
        {
            InitializeComponent();

            this.Loaded += HomeControl_Loaded;
        }

        private void HomeControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = App.SaleViewModel;
            App.SaleViewModel.ShowDetailViewEvent += SaleViewModel_ShowDetailViewEvent; ;
        }

        private void SaleViewModel_ShowDetailViewEvent(object sender, EventArgs e)
        {
            ctrlDetailView.Visibility = Visibility.Visible;
        }

        private void ShowPostControl(object sender, RoutedEventArgs e)
        {
            ctrlPost.Visibility = Visibility.Visible;
        }
    }
}
