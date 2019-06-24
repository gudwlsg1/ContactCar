using ContactCar.Command;
using ContactCar.Model;
using ContactCar.Network.Model;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        private string _comment;
        public string Comment
        {
            get => _comment;
            set => SetProperty(ref _comment, value);
        }

        public ObservableCollection<Sale> Items { get; set; }

        public MultiPartFile[] Files { get; set; }

        private Sale _SelectedItem;
        public Sale SelectedItem
        {
            get => _SelectedItem;
            set
            {
                SetProperty(ref _SelectedItem, value, nameof(SelectedItem));
            }
        }
        #endregion

        #region Command
        public ICommand PostCommand { get; set; }
        public ICommand OpenFileDialogCommand { get; set; }
        public ICommand CloseControlCommand { get; set; }
        public ICommand ShowDetailViewCommand { get; set; }
        public ICommand EditSaleCommand { get; set; }
        public ICommand DeleteSaleCommand { get; set; }
        public ICommand AddCommentCommand { get; set; }

        //public ICommand DeleteCommentCommand { get; set; }
        public DeleteCommentCommand DeleteCommentCommand { get; set; }
        public UpdateCommentCommand UpdateCommentCommand { get; set; }
        #endregion

        #region Event
        public event EventHandler OpenFileDialogEvent;

        public event EventHandler CloseControlEvent;

        public event EventHandler ShowDetailViewEvent;

        public delegate void ResultHandler(bool isSuccess, string msg = null);
        public event ResultHandler ResultEvent;
        #endregion

        public SaleViewModel()
        {
            Items = new ObservableCollection<Sale>();
            OpenFileDialogCommand = new DelegateCommand(OnOpenFileDialog);
            PostCommand = new DelegateCommand(async () => await OnPostAsync());
            CloseControlCommand = new DelegateCommand(OnCloseControl);
            ShowDetailViewCommand = new DelegateCommand(async () => await ShowDetailView());
            EditSaleCommand = new DelegateCommand(async () => await EditSaleAsync());
            DeleteSaleCommand = new DelegateCommand(async () => await DeleteSaleAsync());
            AddCommentCommand = new DelegateCommand(async () => await AddCommentAsync());
            DeleteCommentCommand = new DeleteCommentCommand(DeleteCommentAsync);
            UpdateCommentCommand = new UpdateCommentCommand(UpdateCommentAsync);
        }

        private async void UpdateCommentAsync(int commentId)
        {
            JObject json = new JObject();
            json["content"] = Comment;

            var resp = await App.networkManager.EditComment(commentId, json);

            if (resp == null || resp.Status != (int)HttpStatusCode.OK)
            {
                ResultEvent?.Invoke(false, "댓글 수정에 실패하였습니다.");
                return;
            }

            if (resp != null && resp.Status == (int)HttpStatusCode.OK)
            {
                Comment comment = SelectedItem.lstComment.Where(c => c.Id == commentId).FirstOrDefault();

                if (comment == null)
                {
                    return;
                }
                comment.Content = resp.Data.Content;
            }
        }

        private async void DeleteCommentAsync(int commentId)
        {
            var resp = await App.networkManager.DeleteComment(commentId);

            if (resp == null || resp.Status != (int)HttpStatusCode.OK)
            {
                return;
            }

            if (resp != null && resp.Status == (int)HttpStatusCode.OK)
            {
                Comment comment = SelectedItem.lstComment.Where(c => c.Id == commentId).FirstOrDefault();
                
                if(comment == null)
                {
                    return;
                }
                SelectedItem.lstComment.Remove(comment);
            }
        }

        private async Task AddCommentAsync()
        {
            JObject json = new JObject();
            json["userId"] = App.myInfo.Id;
            json["postId"] = SelectedItem.Id;
            json["content"] = _comment;

            Comment = string.Empty;

            var resp = await App.networkManager.PostComment(json);

            if (resp == null || resp.Status != (int)HttpStatusCode.OK)
            {
                return;
            }

            if (resp != null && resp.Status == (int)HttpStatusCode.OK)
            {
                SelectedItem.lstComment.Add(resp.Data);
            }
        }

        private async Task EditSaleAsync()
        {
            JObject json = new JObject();
            json["title"] = SelectedItem.Title;
            json["content"] = SelectedItem.Content;
            json["price"] = SelectedItem.Price;

            if(SelectedItem == null)
            {
                return;
            }

            var resp = await App.networkManager.EditSale(SelectedItem.Id, json);

            if (resp == null || resp.Status != (int)HttpStatusCode.OK)
            {
                ResultEvent?.Invoke(false, "수정에 실패하였습니다.");
                return;
            }

            if (resp != null && resp.Status == (int)HttpStatusCode.OK)
            {
                Sale respSale = resp.Data;
                SelectedItem.Title = respSale.Title;
                SelectedItem.Content = respSale.Content;
                SelectedItem.Price = respSale.Price;
                ResultEvent?.Invoke(true, "수정 성공하였습니다.");
            }
        }

        private async Task DeleteSaleAsync()
        {
            var resp = await App.networkManager.DeleteSale(SelectedItem.Id);

            if(resp == null || resp.Status != (int)HttpStatusCode.OK)
            {
                ResultEvent?.Invoke(false, "삭제에 실패하였습니다.");
                return;
            }

            if(resp != null && resp.Status == (int)HttpStatusCode.OK)
            {
                this.Items.Remove(SelectedItem);
                SelectedItem = null;
                ResultEvent?.Invoke(true);
            }
        }

        private async Task ShowDetailView()
        {
            if(this.SelectedItem == null)
            {
                return;
            }

            var resp = await App.networkManager.GetCommentData(SelectedItem.Id);

            if (resp == null || resp.Status != (int)HttpStatusCode.OK)
            {
                return;
            }

            if (resp != null && resp.Status == (int)HttpStatusCode.OK)
            {
                SelectedItem.lstComment.Clear();
                resp.Data.ForEach(c => SelectedItem.lstComment.Add(c));
            }
                
            ShowDetailViewEvent?.Invoke(this, null);
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

                    if(resp != null && resp.Status == (int)HttpStatusCode.OK)
                    {
                        data.Data.lstPicture = resp.Data;
                    }
                }
                this.Items.Add(data.Data);
                ClearProperty();
                CloseControlEvent?.Invoke(this, null);
                return;
            }
        }

        private void ClearProperty()
        {
            Title = string.Empty;
            Content = string.Empty;
            Price = 0;
            Files = null;
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
