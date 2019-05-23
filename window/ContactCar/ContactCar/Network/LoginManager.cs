using ContactCar.Model;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCar.Network
{
    public partial class NetworkManager
    {
        public async Task<CResponse<User>> LoginAsync(string id, string pw)
        {
            JObject json = new JObject();
            json["userId"] = id;
            json["password"] = pw;

            return await GetResponse<User>("auth/login", Method.POST, json);
        } 

        public async Task<CResponse<User>> SignUp(JObject json)
        {
            return await GetResponse<User>("auth", Method.POST, json);
        }
    }
}
