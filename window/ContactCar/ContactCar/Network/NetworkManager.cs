﻿using ContactCar.Model;
using ContactCar.Network.Model;
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
        private RestClient createClient()
        {
            return new RestClient("http://localhost:8000/") { Timeout = 15000 };
        }

        public async Task<CResponse<T>> GetResponseAsync<T>(string url, Method method, JObject data = null, MultiPartFile[] files = null)
        {
            var client = createClient();
            var request = new RestRequest(url, method);

            if(data != null)
            {
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", data.ToString(), ParameterType.RequestBody);
            }

            if(files != null)
            {
                request.AddHeader("Content-Type", "multipart/form-data");
                foreach(MultiPartFile file in files)
                {
                    request.AddFile("images[]", file.File, file.FileName);
                }
            }

            var response = await client.ExecuteTaskAsync<CResponse<T>>(request);

            return response.Data;
        }
    }
}
