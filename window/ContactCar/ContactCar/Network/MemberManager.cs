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
        public async Task<CResponse<List<User>>> GetMemberAsync()
        {
            return await GetResponseAsync<List<User>>("auth", Method.GET);
        }

    }
}
