using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        private Printers printers;

        public Gate()
        {
        }

        public Gate(GateSettings settings)
        {
            this.settings = settings;
            printers = new Printers();
        }

        public string GetDataTimeFromTimeStamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp).ToString();
        }

        public async Task<List<TableReservation>> GetTableDataFromServer(string url)
        {
            var result = new List<TableReservation>();
            
            var data = await GetDataFromServer(url);
            if (data != null && data.Count() > 0)
            {
                foreach (var e in data)
                {
                    var temp = JsonConvert.DeserializeObject<TableReservation>(e.ToString());
                    var customf = JsonConvert.DeserializeObject<TableReservationCustomF>(temp.custom_f);
                    temp.CustomF = customf;
                    result.Add(temp);
                }
            }

            return result;
        }

        public async Task<List<TakeAwayReservation>> GetTakeAwayDataFromServer(string url)
        {
            var result = new List<TakeAwayReservation>();

            var data = await GetDataFromServer(url);
            if (data != null && data.Count() > 0)
            {
                foreach (var e in data)
                {
                    var temp = JsonConvert.DeserializeObject<TakeAwayReservation>(e.ToString());
                    var customf = JsonConvert.DeserializeObject<TakeAwayReservationCustomF>(temp.custom_f);
                    temp.CustomF = customf;
                    result.Add(temp);
                }
            }

            return result;
        }

        public async Task<List<RoomReservation>> GetRoomDataFromServer(string url)
        {
            var result = new List<RoomReservation>();
            
            var data = await GetDataFromServer(url);
            if (data != null && data.Count() > 0)
            {
                foreach (var e in data)
                {
                    var temp = JsonConvert.DeserializeObject<RoomReservation>(e.ToString());
                    result.Add(temp);
                }
            }

            return result;
        }

        public async Task<bool> SetTableReservationToServer(string url, bool get = false)
        {
            var result = await GetDataFromServer(url, get);
            if (result != null)
            {
                try
                {
                    var temp = JsonConvert.DeserializeObject<TableReservationPrintData>(result.ToString());
                    var customf = JsonConvert.DeserializeObject<TableReservationCustomF>(temp.custom_f);
                    temp.CustomF = customf;

                    return await printers.PrintTableReservationData(temp);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }

            }
            return false;
        }

        public async Task<bool> ChangePrintStatus(string url, bool get = false)
        {
            var result = await GetDataFromServer(url, get);
            return result != null ? true : false;
        }
        
        public async Task<bool> SetTakeAwayReservationToServer(string url, bool get = false)
        {
            var result = await GetDataFromServer(url, get);
            if (result != null)
            {
                try
                {
                    var temp = JsonConvert.DeserializeObject<TakeAwayReservationPrintData>(result.ToString());
                    var customf = JsonConvert.DeserializeObject<TakeAwayReservationCustomF>(temp.custom_f);
                    temp.CustomF = customf;

                    
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }

            }
            return false;
        }

        public async Task<bool> SetRoomReservationToServer(string url, bool get = false)
        {
            var result = await GetDataFromServer(url, get);
            if (result != null)
            {
                return true;
            }
            return false;
        }


        private async Task<JToken> GetDataFromServer(string url, bool get = true)
        {
            JToken result;
            WebRequest request = WebRequest.Create(url);
            if (!get)
                request.Method = "PUT";

            request.Credentials = CredentialCache.DefaultCredentials;
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            WebResponse response = await request.GetResponseAsync();
            using (var dataStream = response.GetResponseStream())
            {
                var reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                Console.WriteLine(responseFromServer);
                try
                {
                    if (get)
                    { 
                        result = JObject.Parse(responseFromServer)["data"]["data"];
                    }
                    else
                    { 
                        result = JObject.Parse(responseFromServer)["data"]["data"];
                        Debug.WriteLine(responseFromServer);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            response.Close();
            return result;
        }
    }
}
