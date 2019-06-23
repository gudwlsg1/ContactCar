using ContactCar.Network.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
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
    /// Interaction logic for PostControl.xaml
    /// </summary>
    public partial class PostControl : UserControl
    {
        public PostControl()
        {
            InitializeComponent();

            this.Loaded += PostControl_Loaded;
        }

        private void PostControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= PostControl_Loaded;
            this.DataContext = App.SaleViewModel;
            App.SaleViewModel.OpenFileDialogEvent += SaleViewModel_OpenFileDialog;
            App.SaleViewModel.CloseControlEvent += SaleViewModel_CloseControl;
        }

        private void SaleViewModel_CloseControl(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void SaleViewModel_OpenFileDialog(object sender, EventArgs e)
        {
            var fd = new OpenFileDialog();
            fd.Title = "프로필을 선택해주세요";
            fd.Filter = "Image Files|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            fd.Multiselect = true;

            if(fd.ShowDialog() == true)
            {
                var path = fd.FileNames;
                var files = new MultiPartFile[path.Length];

                for(int i = 0; i < path.Length; i++)
                {
                    files[i] = new MultiPartFile(File.ReadAllBytes(path[i]), System.IO.Path.GetFileName(path[i])); 
                }

                App.SaleViewModel.Files = files;
            }
        }
    }
}
