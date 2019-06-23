using ContactCar.Model;
using ContactCar.Network.Model;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ContactCar.ViewModel
{
    public class SaleViewModel : BindableBase
    {
        #region Property
        private object _lock = new object();

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _content;
        public string Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        private int _price;
        public int Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        public ObservableCollection<Sale> Items { get; set; }

        public MultiPartFile[] Files { get; set; }

        private Sale _SelectionItem;
        public Sale SelectionItem
        {
            get => _SelectionItem;
            set
            {
                SetProperty(ref _SelectionItem, value);
            }
        }
        #endregion

        #region Command
        public ICommand PostCommand { get; set; }
        public ICommand OpenFileDialogCommand { get; set; }
        public ICommand CloseControlCommand { get; set; }
        public ICommand ShowDetailViewCommand { get; set; }
        #endregion

        #region Event
        public event EventHandler OpenFileDialogEvent;

        public event EventHandler CloseControlEvent;

        public delegate void DetailViewHandler(Sale selectionSale);
        public event DetailViewHandler ShowDetailViewEvent;
        #endregion

        public SaleViewModel()
        {
            Items = new ObservableCollection<Sale>();
            OpenFileDialogCommand = new DelegateCommand(OnOpenFileDialog);
            PostCommand = new DelegateCommand(async () => await OnPostAsync());
            CloseControlCommand = new DelegateCommand(OnCloseControl);
            ShowDetailViewCommand = new DelegateCommand(ShowDetailView);
        }

        private void ShowDetailView()
        {
            if(this.SelectionItem == null)
            {
                return;
            }
            ShowDetailViewEvent?.Invoke(this.SelectionItem);
        }

        private void OnCloseControl()
        {
            CloseControlEvent?.Invoke(this, null);
        }

        private void OnOpenFileDialog()
        {
            OpenFileDialogEvent?.Invoke(this, null);
        }

        private async Task OnPostAsync()
        {
            JObject json = new JObject();
            json["userId"] = App.myInfo.Id;
            json["title"] = this.Title;
            json["content"] = this.Content;
            json["price"] = this.Price;

            var data = await App.networkManager.PostSale(json);

            if (data == null || data.Status != (int)HttpStatusCode.OK)
            {// 추가 실패
                Files = null;
                return;
            }

            if (data != null || (int)HttpStatusCode.OK == data.Status)
            {// 추가 성공
                //this.Items.Add(data.Data);
                if (Files != null)
                {
                    int postId = data.Data.Id;
                    var resp = await App.networkManager.UploadFiles(Files, postId);
                        foreach(MultiPartFile file in Files)
                        {
                            Picture picture = new Picture() { Path = string.Format("http://localhost:8000/pictures/{0}", file.FileName), PostId = postId };
                            data.Data.lstPicture.Add(picture);
                        }
                        this.Items.Add(data.Data);
                        Files = null;
                }
                CloseControlEvent?.Invoke(this, null);
                return;
            }
        }

        public async Task GetDataAsync()
        {
            var resp = await App.networkManager.GetSaleData();

            if(resp == null || resp.Status != 200)
            {
                return;
            }

            lock (_lock)
            {
                AddSales(resp.Data);
            }
        }

        private void AddSales(List<Sale> data)
        {
            foreach(Sale sale in data)
            {
                this.Items.Add(sale);
            }
        }
    }
}
