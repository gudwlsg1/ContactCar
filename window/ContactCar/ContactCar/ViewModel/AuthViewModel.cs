using ContactCar.Model;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ContactCar.ViewModel
{
    public class AuthViewModel : BindableBase
    {
        #region Property
        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                SetProperty(ref _id, value);
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                SetProperty(ref _email, value);
            }
        }

        private string _address;
        public string Address
        {
            get => _address;
            set
            {
                SetProperty(ref _address, value);
            }
        }

        private string _desc;
        public string Desc
        {
            get => _desc;
            set
            {
                SetProperty(ref _desc, value);
            }
        }
        #endregion

        #region Command
        public ICommand LoginCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        #endregion

        #region Event
        public delegate void AuthResultHandler(bool success, User myInfo);

        public event AuthResultHandler LoginResult;
        public event AuthResultHandler SignUpResult;
        #endregion

        public AuthViewModel()
        {
            InitCommand();
        }

        private void InitCommand()
        {
            LoginCommand = new DelegateCommand(async () => await OnLoginAsync());
            SignUpCommand = new DelegateCommand(async () => await OnSignUpAsync());
        }

        private async Task OnSignUpAsync()
        {
            JObject json = new JObject();
            json["userId"] = _id;
            json["password"] = _password;
            json["name"] = _name;
            json["email"] = _email;
            json["address"] = _address;

            SignUpResult?.Invoke(true, null);
            ResetProperty();

            return;
            var data = await App.networkManager.SignUp(json);

            if (data == null)
            {
                Desc = "회원가입에 실패했습니다.";
                SignUpResult?.Invoke(false, null);
                return;
            }

            if (data != null || (int)HttpStatusCode.OK == data.Status)
            {// 회원가입 성공
                SignUpResult?.Invoke(true, data.Data);
                ResetProperty();
                return;
            }

            SignUpResult?.Invoke(false, null);
        }

        private void ResetProperty()
        {
            Password = null;
        }

        private async Task OnLoginAsync()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                return;
            }

            JObject json = new JObject();
            json["userId"] = _id;
            json["password"] = _password;

            var data = await App.networkManager.LoginAsync(json);

            if (data.Data != null && (int)HttpStatusCode.OK == data.Status)
            {// 로그인 성공
                LoginResult?.Invoke(true, data.Data);
                return;
            }

            LoginResult?.Invoke(false, null);
        }
    }
}
