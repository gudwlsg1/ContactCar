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
    public class LoginViewModel : BindableBase
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
        #endregion

        #region Command
        public ICommand LoginCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        public ICommand SignUpCtrlCommand { get; set; }
        #endregion

        #region Event
        public delegate void LoginResultHandler(bool success, User myInfo);
        public event LoginResultHandler LoginResult;

        public event EventHandler ShowSignUpControl;
        #endregion

        public LoginViewModel()
        {
            InitCommand();
        }

        private void InitCommand()
        {
            LoginCommand = new DelegateCommand(OnLogin);
            SignUpCommand = new DelegateCommand(OnSignUp);
            SignUpCtrlCommand = new DelegateCommand(ShowSignUp);
        }

        private void ShowSignUp()
        {
            ShowSignUpControl?.Invoke(this, null);
        }

        private void OnSignUp()
        {
            JObject json = new JObject();
            json["userId"] = _id;
            json["password"] = _password;
            json["name"] = _name;
            json["email"] = _email;
            json["address"] = _address;

            var data = App.networkManager.SignUp(json);

            if (data != null || (int)HttpStatusCode.OK == data.Status)
            {// 로그인 성공
                /*LoginResult?.Invoke(true, data.Data);
                return;*/
            }
        }

        private void InitProperty()
        {
            _id = null;
            _password = null;
        }

        private void OnLogin()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                return;
            }

            var data = App.networkManager.Login(_id, _password);

            if(data != null || (int)HttpStatusCode.OK == data.Status)
            {// 로그인 성공
                LoginResult?.Invoke(true, data.Data);
                return;
            }

            LoginResult?.Invoke(false, null);
        }
    }
}
