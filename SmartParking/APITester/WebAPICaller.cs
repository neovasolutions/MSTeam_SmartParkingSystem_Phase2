using System;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

namespace APITester
{
    public delegate void DlgtActionOnReadCompleted(object myList);
    public delegate void DlgtActionOnUploadCompleted(string respResult);
    public class WebAPICaller
    {
        private WebClient _MyWebClient;
        private DataContractJsonSerializer _jsonData;
        private const string _uri = "http://192.168.1.234:8080/api/";
        //private const string _uri = "http://localhost:62073/api/";
        private DlgtActionOnReadCompleted _ActionOnReadCompleted;
        private DlgtActionOnUploadCompleted _ActionOnUploadCompleted;
        public WebAPICaller()
        {
            _MyWebClient = new WebClient();
        }

        public DlgtActionOnReadCompleted ActionOnReadCompleted
        { set { _ActionOnReadCompleted = value; } }

        public DlgtActionOnUploadCompleted ActionOnUploadCompleted
        { set { _ActionOnUploadCompleted = value; } } 

        public  DataContractJsonSerializer JsonSerializer
        { set { _jsonData = value; } } 

        public void GET(string Controller)
        {
            _MyWebClient.OpenReadCompleted += new System.Net.OpenReadCompletedEventHandler(WebClient_OpenReadCompleted);   
            
            _MyWebClient.OpenReadAsync(new Uri(_uri  + Controller));
        }
        public void GET(string Controller, string Parameter)
        {
            _MyWebClient.OpenReadCompleted += new System.Net.OpenReadCompletedEventHandler(WebClient_OpenReadCompleted);
            string myUrl = _uri + Controller + "/?" + Parameter;
            _MyWebClient.OpenReadAsync(new Uri(_uri + Controller + "/?" + Parameter ));
        }
        public void POST(string Controller, object obj)
        {
            DataContractJsonSerializer jsonData = new DataContractJsonSerializer(obj.GetType());
            MemoryStream memStream = new MemoryStream();
            jsonData.WriteObject(memStream, obj);
            byte[] jsonDataToPost = memStream.ToArray();
            memStream.Close();
            var data = Encoding.UTF8.GetString(jsonDataToPost, 0, jsonDataToPost.Length);
            _MyWebClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            _MyWebClient.UploadStringCompleted += new  System.Net.UploadStringCompletedEventHandler (WebClient_UploadStringCompleted );
            _MyWebClient.UploadStringAsync(new Uri(_uri + Controller), "POST", data);
        }
        public void PUT(string Controller, object obj)
        {
            DataContractJsonSerializer jsonData = new DataContractJsonSerializer(obj.GetType());
            MemoryStream memStream = new MemoryStream();
            jsonData.WriteObject(memStream, obj);
            byte[] jsonDataToPost = memStream.ToArray();
            memStream.Close();
            var data = Encoding.UTF8.GetString(jsonDataToPost, 0, jsonDataToPost.Length);
            _MyWebClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            _MyWebClient.UploadStringCompleted += new System.Net.UploadStringCompletedEventHandler(WebClient_UploadStringCompleted);
            _MyWebClient.UploadStringAsync(new Uri(_uri + Controller), "PUT", data);
        }
        public void POST(string Controller, string  jsonData)
        {
            _MyWebClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            _MyWebClient.Headers[HttpRequestHeader.Accept] = "application/json";
            _MyWebClient.UploadStringCompleted += new System.Net.UploadStringCompletedEventHandler(WebClient_UploadStringCompleted);
            _MyWebClient.UploadStringAsync(new Uri(_uri + Controller), "POST", jsonData);
        }
        public void POST_urlParam(string Controller, string urlParameter)
        {
            _MyWebClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            //_MyWebClient.Headers[HttpRequestHeader.Accept] = "application/json";
            _MyWebClient.UploadStringCompleted += new System.Net.UploadStringCompletedEventHandler(WebClient_UploadStringCompleted);
            _MyWebClient.UploadStringAsync(new Uri(_uri + Controller + "?" + urlParameter), "POST", string .Empty);
            //_MyWebClient.UploadStringAsync(new Uri(_uri + Controller), "POST");
        }
        void WebClient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    Stream jsonDataStream = e.Result;
                    _ActionOnReadCompleted(_jsonData.ReadObject(jsonDataStream));
                }
                else
                    _ActionOnReadCompleted(null);
            }
            catch (Exception e1) { }
        }
        void WebClient_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    //Stream jsonDataStream = e.Result.;
                    _ActionOnUploadCompleted(e.Result);
                }
                else
                    _ActionOnUploadCompleted(null);
            }
            catch (Exception e1) { }
        }
    }
}
