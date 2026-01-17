using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Xml;
using System.Threading;

namespace Futech.Socket
{
    public class HttpRequestResponse
    {
        private string URI = "http://localhost:8080/GetEvent(?)";
        private string UserName = "thanh.bk46@gmail.com";
        private string UserPwd = "123456";
        private string ProxyServer = "";
        private int ProxyPort = 0;
        private string RequestMethod = "GET";

        public HttpRequestResponse(string pURI)//Constructor
        {
            URI = pURI;
        }

        public string HTTP_USER_NAME
        {
            get
            {
                return UserName;
            }
            set
            {
                UserName = value;
            }
        }

        public string HTTP_USER_PASSWORD
        {
            get
            {
                return UserPwd;
            }
            set
            {
                UserPwd = value;
            }
        }

        public string PROXY_SERVER
        {
            get
            {
                return ProxyServer;
            }
            set
            {
                ProxyServer = value;
            }
        }

        public int PROXY_PORT
        {
            get
            {
                return ProxyPort;
            }
            set
            {
                ProxyPort = value;
            }
        }

        // This public interface receives the request and send the response of type string.
        public string SendRequest(string Request)
        {
            string FinalResponse = "";

            NameValueCollection collHeader = new NameValueCollection();

            HttpWebResponse webresponse;

            HttpBaseClass BaseHttp = new
              HttpBaseClass(UserName, UserPwd,
              ProxyServer, ProxyPort, Request);

            try
            {
                HttpWebRequest webrequest =
                  BaseHttp.CreateWebRequest(URI,
                  collHeader, RequestMethod, true);
                webresponse =
                 (HttpWebResponse)webrequest.GetResponse();
                ////
                Encoding enc = System.Text.Encoding.GetEncoding(1252);
                StreamReader loResponseStream = new
                  StreamReader(webresponse.GetResponseStream(), enc);

                string Response = loResponseStream.ReadToEnd();

                loResponseStream.Close();
                webresponse.Close();

                FinalResponse = Response;

            }//End of Try Block

            catch (WebException e)
            {
                throw CatchHttpExceptions(FinalResponse = e.Message);
            }
            catch (System.Exception e)
            {
                throw new Exception(FinalResponse = e.Message);
            }
            finally
            {
                BaseHttp = null;
            }

            return FinalResponse;
        } //End of SendRequestTo method


        private WebException CatchHttpExceptions(string ErrMsg)
        {
            ErrMsg = "Error During Web Interface. Error is: " + ErrMsg;
            return new WebException(ErrMsg);
        }

    }//End of RequestResponse Class
}
