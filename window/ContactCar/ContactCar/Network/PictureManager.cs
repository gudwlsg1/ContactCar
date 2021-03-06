﻿using ContactCar.Model;
using ContactCar.Network.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCar.Network
{
    public partial class NetworkManager
    {
        private const string PICTURE = "picture/";
        public async Task<CResponse<List<Picture>>> UploadFiles(MultiPartFile[] files, int postId)
        {
            var resp = await GetResponseAsync<List<Picture>>(PICTURE + postId, RestSharp.Method.POST, null, files);

            return resp;
        }

        public async Task<CResponse<List<Picture>>> GetPictureData()
        {
            var resp = await GetResponseAsync<List<Picture>>(PICTURE, RestSharp.Method.GET);
            return resp;
        }
    }
}
