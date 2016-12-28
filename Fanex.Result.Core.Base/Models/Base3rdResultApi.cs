namespace Fanex.Result.Core.Base.Models
{
    using System;
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization.Json;
    using System.Text;

    public abstract class Base3rdResultApi
    {
        public Base3rdResultApi()
        {
            TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        protected string TimeStamp { get; set; }

        protected string SiteId { get; set; }

        protected string Seq { get; set; }

        public abstract string GetResultUrl<T>(T data);

        protected string GenerateSeqCode(string custId, string gameId, string siteId)
        {
            var random = new Random();

            var timestampWithRandom = $"{DateTime.Now.Ticks}-{random.Next(0, int.MaxValue)}";

            var hashedPreInfo = $"{siteId}-{custId}-{gameId}-{random.Next(0, int.MaxValue)}";

            return $"{timestampWithRandom}-{hashedPreInfo}";
        }

        protected string SerializeJsonObject<T>(T request)
        {
            var ser = new DataContractJsonSerializer(typeof(T));
            var stream = new MemoryStream();
            var streamReader = new StreamReader(stream);

            ser.WriteObject(stream, request);
            stream.Position = 0;

            return streamReader.ReadToEnd();
        }

        protected T DeserializeJsonObject<T>(string jsonData)
        {
            var ser = new DataContractJsonSerializer(typeof(T));
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonData));

            return (T)ser.ReadObject(stream);
        }

        protected string SendPostRequest(string url, string jsonData)
        {
            var result = string.Empty;

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                result = client.UploadString(url, jsonData);
            }

            return result;
        }
    }
}