using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BattleSweeperServer.Models;
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
    class APIAccessorSingleton
    {
        private static string apiEndpoint = "https://localhost";
        private static string apiPort = "44337";

        private static APIAccessorSingleton singleton;
        private HttpClient httpClient;


        public static APIAccessorSingleton Instance { get { return APIAccessorSingleton.singleton; } }

        public static readonly string ChatsRoute = "api/Chats";
        


        static APIAccessorSingleton()
        {
            // Save me stack overflow from random configs I need to do to get this stuff to work
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            singleton = new APIAccessorSingleton();
        }

        private APIAccessorSingleton()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.BaseAddress = new Uri(string.Format("{0}:{1}/", apiEndpoint, apiPort));
        }

        public async Task<Game> CreateGame(Game game)
        {
            return await PostObject<Game>("BattleSweeper/CreateGame", game);
        }

        public async Task<Game> GetGameState(string gameKey)
        {
            return await GetObject<Game>("BattleSweeper/Game/{0}/State", gameKey);
        }

        public async Task<bool> RegisterPlayerToGame(string gameKey, Player player)
        {
            Player registeredPlayer = await PostObject<Player>(string.Format("BattleSweeper/Game/{0}/RegisterPlayer", gameKey), player);

            if (registeredPlayer != null)
            {
                // TODO: remove header after game is finished
                httpClient.DefaultRequestHeaders.Add("PlayerIdentifier", registeredPlayer.Identifier);
                return true;
            }
            else
            {
                return false;
            }
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
                IEnumerable<T> messages = await response.Content.ReadAsAsync<IEnumerable<T>>();
                return messages;
            }
            else
            {
                return new List<T>();
            }
        }

        public async Task<T> GetObject<T>(string route, string id)
        {
            HttpResponseMessage response;
            try
            {
                response = await this.httpClient.GetAsync(string.Format(route, id));
            }
            catch (Exception e)
            {
                Debug.WriteLine(string.Format("Connection to server failed: GET {0}", route));
                Debug.WriteLine(e.Message);
                return default;
            }

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                T obj = JsonConvert.DeserializeObject<T>(json);
                //T obj = await response.Content.ReadAsAsync<T>();
                return obj;
            }
            else
            {
                return default;
            }
        }

        public async Task<T> PostObject<T>(string route, T obj)
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
                return default;
            }

            return response.IsSuccessStatusCode ? await response.Content.ReadAsAsync<T>() : default; 
        }
    }
}
