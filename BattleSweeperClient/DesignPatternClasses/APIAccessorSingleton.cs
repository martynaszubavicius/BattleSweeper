using System;
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

namespace BattleSweeperClient.DesignPatternClasses
{
    class APIAccessorSingleton
    {
        private static string apiEndpoint = "https://localhost";
        private static string apiPort = "44337";

        private static APIAccessorSingleton singleton;
        private HttpClient httpClient;


        public static APIAccessorSingleton Instance { get { return APIAccessorSingleton.singleton; } }

        public static readonly string ChatsRoute = "api/Chats";
        public static readonly string GameSettingsRoute = "BattleSweeper/GameSettings";

        // Change this to true if you want to use heroku deployed server
        // Deployed version uses heroku_deploy branch - merge master on heroku_deploy to update deployed version
        // Deploying takes time - be patient. And make sure that the deployed version is actually working...
        private bool use_heroku_server = false;

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
            
            if (use_heroku_server)
                httpClient.BaseAddress = new Uri("https://battle-sweeper.herokuapp.com/");
            else
                httpClient.BaseAddress = new Uri(string.Format("{0}:{1}/", apiEndpoint, apiPort));
        }

        public async Task<string> GetNewGameFromSettings(GameSettings settings, bool debug)
        {
            return await GetObject<string>("BattleSweeper/GetNewGameFromSettings/{0}" + (debug ? "/Debug" : ""), settings.Id.ToString());
        }

        public async Task<Game> GetGameState(string gameKey, int lastState = -1)
        {
            string route = lastState >= 0 ? string.Format("BattleSweeper/Game/{{0}}/State/{0}", lastState) : "BattleSweeper/Game/{0}/State";
            return await GetObject<Game>(route, gameKey);
        }

        public async Task<bool> RegisterPlayerToGame(string gameKey, Player player, bool randomBoard, bool secondaryTestPlayer = false)
        {
            Player registeredPlayer = await PostObject<Player>(string.Format("BattleSweeper/Game/{0}/RegisterPlayer" + (randomBoard ? "WithBoard" : ""), gameKey), player);

            if (secondaryTestPlayer)
                return true;

            if (registeredPlayer != null)
            {
                if (httpClient.DefaultRequestHeaders.Contains("PlayerIdentifier"))
                    httpClient.DefaultRequestHeaders.Remove("PlayerIdentifier");
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
