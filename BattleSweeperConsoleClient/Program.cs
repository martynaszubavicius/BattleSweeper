using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace BattleSweeperConsoleClient
{
    class Program
    {
        private const string endPoint = "https://localhost";
        private const string port = "44337";
        
        static string URL { get { return string.Format("{0}:{1}/", endPoint, port); } }

        private const string apiString = "api/Chats/";


        private static string clientName;

        static void Main(string[] args)
        {
            // Initial setup - save me stackoverflow
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            // Setup the Http client that does all the things
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(URL);

            // Set the client name in console
            Console.WriteLine("Write your name:");
            clientName = Console.ReadLine();
            String message = "Hello!";

            // Send hello to the server with our name
            try
            {
                PostServerMessage(client, message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            // Keep bothering the server for new messages
            while (true)
            {
                Console.Clear();
                Console.WriteLine(String.Format("Client '{0}':\n", clientName));
                try
                {
                    foreach (Chat msg in GetServerMessages(client))
                    {
                        Console.WriteLine(msg);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Thread.Sleep(1000);
            }

            //client.Dispose();
        }

        static void PostServerMessage(HttpClient client, String message)
        {
            Chat msg = new Chat(Program.clientName, message);
            var content = new StringContent(JsonConvert.SerializeObject(msg), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(apiString, content).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(String.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));
            }
        }

        static IEnumerable<Chat> GetServerMessages(HttpClient client)
        {
            HttpResponseMessage response = client.GetAsync(apiString).Result;

            if (response.IsSuccessStatusCode)
            {
                string raw = response.Content.ReadAsStringAsync().Result;
                IEnumerable<Chat> messages = response.Content.ReadAsAsync<IEnumerable<Chat>>().Result;
                return messages;
            }
            else
            {
                throw new Exception(String.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));
            }
        }
    }

    public class Chat
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }

        public Chat(string author, string text)
        {
            Author = author;
            Text = text;
        }

        public override string ToString()
        {
            return string.Format("{0}: '{1}' says: \"{2}\"", this.Id, this.Author, this.Text);
        }
    }

}
