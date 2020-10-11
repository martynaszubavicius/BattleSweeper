using BattleSweeperServer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleSweeperClient
{
    public partial class ChatWindow : Form
    {
        //private const string apiString = "api/Chats/";
        //private static HttpClient client;

        public ChatWindow()
        {
            //client = new HttpClient();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.BaseAddress = new Uri(Program.URL);

            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Chat msg = new Chat(textBox3.Text, textBox2.Text);
            if (await APIAccessorSingleton.Instance.PostObject<Chat>(APIAccessorSingleton.ChatsRoute, msg) == null)
            {
                textBox2.Text = "";
                textBox2.Focus();
            }
        }

        //private async void button1_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        bool success = await PostServerMessage(textBox3.Text, textBox2.Text);
        //        textBox2.Text = "";
        //        textBox2.Focus();
        //    }
        //    catch
        //    {
        //        textBox1.Text = "bad things happened in post";
        //    }
        //}

        private void ChatWindow_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            textBox1.Text = "Innitialising chat...";

            textBox2.Focus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateChat();
        }

        private async void UpdateChat()
        {
            textBox1.Text = "";
            var messages = await APIAccessorSingleton.Instance.GetObjects<Chat>(APIAccessorSingleton.ChatsRoute);
            foreach (Chat msg in messages)
            {
                textBox1.Text += string.Format("{0}{1}", msg, Environment.NewLine);
            }
        }

        //private async void UpdateChat()
        //{
        //    textBox1.Text = "";
        //    try
        //    {
        //        var messages = await GetServerMessagesAsync();
        //        foreach (Chat msg in messages)
        //        {
        //            textBox1.Text += string.Format("{0}{1}", msg, Environment.NewLine);
        //        }
        //    }
        //    catch
        //    {
        //        textBox1.Text = "bad things happened";
        //    }
        //}

        //static async Task<bool> PostServerMessage(string author, string message)
        //{
        //    Chat msg = new Chat(author, message);
        //    var content = new StringContent(JsonConvert.SerializeObject(msg), Encoding.UTF8, "application/json");
        //    HttpResponseMessage response = await client.PostAsync(apiString, content);

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        return false;
        //        // throw new Exception(String.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

        //static async Task<IEnumerable<Chat>> GetServerMessagesAsync()
        //{
        //    HttpResponseMessage response = await client.GetAsync(apiString);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        string raw = response.Content.ReadAsStringAsync().Result;
        //        IEnumerable<Chat> messages = response.Content.ReadAsAsync<IEnumerable<Chat>>().Result;
        //        return messages;
        //    }
        //    else
        //    {
        //        return new List<Chat>();
        //        // throw new Exception(String.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));
        //    }
        //}

    }
}
