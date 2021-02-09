using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.IO;
using System.Net.Mail;

namespace InventoryManagement.API.Common
{
    public class Program
    {
        //public static async Task<bool> SendSMS(string UserName, string Password,string SenderId,string SMS,string MobileNo)
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        var values = new Dictionary<string, string>();
        //       // values.Add("token", "abfYkr54678orlAew56129PjuE8426");
        //        values.Add("uname", UserName);
        //        values.Add("pass", Password);
        //        values.Add("send", SenderId);
        //        values.Add("dest", MobileNo);
        //        values.Add("msg", SMS);
        //        var content = new FormUrlEncodedContent(values);
        //        try
        //        {
        //            var httpResponse = await httpClient.PostAsync("http://49.50.77.216/API/SMSHttp.aspx", content);
        //            if (httpResponse != null)
        //            {
        //               // var responseContent = await httpResponse.Content.ReadAsStringAsync();

        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        catch (Exception e)
        //        {
                   
        //        }
        //    }

        //    return false;
        //}
        public static async Task<bool> SendSMSOTP(string UserName, string Password, string SenderId, string SMS, string MobileNo)
        {
            using (var httpClient = new HttpClient())
            {
                //var values = new Dictionary<string, string>();
                //// values.Add("token", "abfYkr54678orlAew56129PjuE8426");
                //values.Add("Username", UserName);
                //values.Add("password", Password);
                //values.Add("Message", SMS);
                //values.Add("to", MobileNo);
                //values.Add("sender", SenderId);
                //values.Add("priority", "1");
                //values.Add("dnd", "1");
                //values.Add("unicode", "0");
                //var content = new FormUrlEncodedContent(values);
                try
                {
                    WebClient client = new WebClient();
                   // var httpResponse = await httpClient.PostAsync( "http://www.kit19.com/ComposeSMS.aspx", content);
                ////  string baseurl = "http://www.kit19.com/ComposeSMS.aspx?username=" + UserName + "&password=" + Password + "&sender=" + SenderId + "&to=" + MobileNo + "&message=" + SMS + "&priority=1&dnd=1&unicode=0";
                ////    Stream data = client.OpenRead(baseurl);
                ////    StreamReader reader =new StreamReader(data);
                ////    String s;
                ////    s = reader.ReadToEnd();
                ////data.Close();
                ////    reader.Close();
                   
                        return true;
                   
                }
                catch (Exception e)
                {

                }
            }

            return false;
        }

        public static async Task<bool> SendEmail(string htmlString,string MailTo)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("spprt.vision@gmail.com");
                foreach (var address in MailTo.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    message.To.Add(address);
                }
                
                message.Subject = "Login OTP";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("spprt.vision@gmail.com", "Vision#@123");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { }
            return true;
        }
    }
}