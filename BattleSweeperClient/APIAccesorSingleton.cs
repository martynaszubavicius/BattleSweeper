﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BattleSweeperClient.Models;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Http.Headers;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using System.Net;
using System.Diagnostics;

namespace BattleSweeperClient
{
    class APIAccesorSingleton
    {
        private static string apiEndpoint = "https://localhost";
        private static string apiPort = "44337";

        private static APIAccesorSingleton singleton;
        private HttpClient httpClient;


        public static APIAccesorSingleton Instance { get { return APIAccesorSingleton.singleton; } }

        public static readonly string ChatsRoute = "api/Chats";


        static APIAccesorSingleton()
        {
            // Save me stack overflow from random configs I need to do to get this stuff to work
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            singleton = new APIAccesorSingleton();
        }

        private APIAccesorSingleton()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.BaseAddress = new Uri(string.Format("{0}:{1}/", apiEndpoint, apiPort));
        }

        public async Task<IEnumerable<T>> GetObjects<T>(string route)
        {
            HttpResponseMessage response;
            try
            {
                response = await this.httpClient.GetAsync(route);
            }
            catch (Exception e)
            {
                Debug.WriteLine(string.Format("Connection to server failed: GET {0}", route));
                Debug.WriteLine(e.Message);
                return new List<T>();
            }

            if (response.IsSuccessStatusCode)
            {
                string raw = response.Content.ReadAsStringAsync().Result;
                IEnumerable<T> messages = await response.Content.ReadAsAsync<IEnumerable<T>>();
                return messages;
            }
            else
            {
                return new List<T>();
            }
        }

        public async Task<bool> PostObject<T>(string route, T obj)
        {
            HttpResponseMessage response;
            var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            try
            {
                response = await this.httpClient.PostAsync(route, content);
            }
            catch (Exception e)
            {
                Debug.WriteLine(string.Format("Connection to server failed: POST {0}", route));
                Debug.WriteLine(e.Message);
                return false;
            }

            return response.IsSuccessStatusCode;
        }
    }
}
