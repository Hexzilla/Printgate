using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Printgate.Model
{
    public class Gate
    {
        private GateSettings settings;

        public Gate(GateSettings settings)
        {
            this.settings = settings;
        }

        private string GetServerUrl()
        {
            return string.Format("{0}/index.php?option=com_api&app=vikrestaurants&resource=reservation&format=raw", settings.JoomlaServer);
        }

        private void GetTableDataFromServer()
        {
            var url = GetServerUrl() + "&id=" + maxTableReservationId + "&type=restaurant";
            var data = GetDataFromServer(url);
            if (data != null && data.Count() > 0)
            {
                foreach (var e in data)
                {
                    mTableReservations.Add(new TableReservation(long.Parse(e["id"].ToString())
                                                                , GetDataTimeFromTimeStamp(double.Parse(e["checkin_ts"].ToString()))
                                                                , e["purchaser_nominative"].ToString() + " [" + e["purchaser_phone"].ToString() + "] "
                                                                , e["purchaser_mail"].ToString()
                                                                , e["status"].ToString()
                                                                , "NO"));
                }
                maxTableReservationId = long.Parse(data[0]["id"].ToString());
            }
        }

        private void GetTakeAwayDataFromServer()
        {
            var url = GetServerUrl() + "&id=" + maxTakeAwayReservationId + "&type=takeaway";
            var data = GetDataFromServer(url);
            if (data != null && data.Count() > 0)
            {
                foreach (var e in data)
                {
                    mTakeAwayReservations.Add(new TakeAwayReservation(long.Parse(e["id"].ToString())
                                                                , GetDataTimeFromTimeStamp(double.Parse(e["checkin_ts"].ToString()))
                                                                , e["purchaser_nominative"].ToString() + " [" + e["purchaser_phone"].ToString() + "] "
                                                                , e["purchaser_mail"].ToString()
                                                                , e["status"].ToString()
                                                                , "NO"));
                }
                maxTakeAwayReservationId = long.Parse(data[0]["id"].ToString());
            }
        }
        public async void SetTableReservationToServer(string url, TableReservation item, bool get = false)
        {
            BeforeAsync();
            var result = await Task.Run(() =>
            {
                return GetDataFromServer(url, get);
            });
            Console.WriteLine(result["success"]);
            mTableReservations.Remove(item);
            AfterAsync();
        }

        public async void SetTakeAwayReservationToServer(string url, TakeAwayReservation item, bool get = false)
        {
            BeforeAsync();
            var result = await Task.Run(() =>
            {
                return GetDataFromServer(url, get);
            });
            Console.WriteLine(result["success"].ToString());
            mTakeAwayReservations.Remove(item);
            AfterAsync();
        }

        private JToken GetDataFromServer(string url, bool get = true)
        {
            JToken result;
            // Create a request for the URL.
            WebRequest request = WebRequest.Create(url);
            if (!get)
                request.Method = "PUT";

            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;

            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            // Get the response.
            WebResponse response = request.GetResponse();
            
            // Display the status.
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.
            using (var dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                var reader = new StreamReader(dataStream);

                // Read the content.
                string responseFromServer = reader.ReadToEnd();

                // Display the content.
                Console.WriteLine(responseFromServer);
                // Parse Json
                if (get)
                    result = JObject.Parse(responseFromServer)["data"]["data"];
                else
                    result = JObject.Parse(responseFromServer)["data"];
            }
            // Close the response.
            response.Close();

            return result;
        }
    }
}
