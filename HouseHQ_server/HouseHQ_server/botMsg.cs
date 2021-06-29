using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;


namespace HouseHQ_server
{
    /*
     * This file is running a bot that sends alerts to the administrator directly to WhatsApp currently the bot only works for my phone number
     */
    public class botMsg
    {
        public void sentMsgForWhatsApp(string apiKey, string phone, string text)
        {
            text = text.Replace(" ", "+");
            HttpClient client = new HttpClient();
            var responseString = client.GetStringAsync("https://api.callmebot.com/whatsapp.php?phone=" + phone + "&text=" + text + "&apikey=" + apiKey);
        }
    }
}
