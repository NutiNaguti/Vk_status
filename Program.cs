using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using SimpleJson;
using Newtonsoft.Json;
using System.Threading;
using System.Runtime.InteropServices;

namespace ConsoleApp5
{
    class Program
    {
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        public static void Main()
        {
            ShowWindow(GetConsoleWindow(), 0);
            while (true)
            {
                Metod();
            }
        }
        public static void Metod()
        {
            string token = "dd82658dc77e3eeac8bcd38bc9d1149e938d313c28a6609a9eb27209549e2963332802e68486c5c383906";

            var responseString = "";
            using (var client = new WebClient())
            {
                responseString = client.DownloadString("https://api.coinmarketcap.com/v1/ticker/bitcoin/");
            }
            dynamic decoded = JsonConvert.DeserializeObject(responseString);

            var stoimost = decoded[0].price_usd;
            stoimost = Math.Round(Convert.ToDouble(stoimost));
            var percent = decoded[0].percent_change_24h;
            var cap = decoded[0].market_cap_usd;
            string time = DateTime.Now.ToShortTimeString();
            string status = "🔥 Текущая стоимость BTC: " + stoimost + "$ ✨ Изменение за 24 часа: " + percent + "% ✨ Капитализация: " + cap + "$ 🕐 Время: " + time + "🔥";
            using (var client_1 = new WebClient())
            {
                string response = client_1.DownloadString("https://api.vk.com/method/status.set?v=1&text=" + status + "&access_token=" + token);
            }
            Thread.Sleep(60000);
        }
    }
}
