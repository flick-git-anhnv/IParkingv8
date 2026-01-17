using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Drawing;
using Futech.LPR;
using Newtonsoft.Json;

namespace PlateRecognizer
{
    public class PlateReader
    {
        /// <summary>
        /// Read a plate number from pictures.
        /// </summary>
        /// <param name="postUrl">API Url.</param>
        /// <param name="filePath">File Path.</param>
        /// <param name="regions">Regions</param>
        /// <param name="token">Authentification Token.</param>
        /// <returns></returns>
        public static PlateReaderResult Read(string postUrl, string filePath, string regions, string token)
        {
            try
            {
                PlateReaderResult result = null;
                string formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());
                string contentType = "multipart/form-data; boundary=" + formDataBoundary;
                byte[] formData = GetMultipartFormData(filePath, regions, formDataBoundary);

                HttpWebRequest request = WebRequest.Create(postUrl) as HttpWebRequest;
                request.ReadWriteTimeout = 1000;
                // Set up the request properties.
                request.Method = "POST";
                request.ContentType = contentType;
                request.UserAgent = ".NET Framework CSharp Client";
                request.CookieContainer = new CookieContainer();
                request.ContentLength = formData.Length;
                request.Headers.Add("Authorization", "Token " + token);

                // Send the form data to the request.
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(formData, 0, formData.Length);
                    requestStream.Close();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                WebHeaderCollection header = response.Headers;
                var encoding = ASCIIEncoding.ASCII;
                using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
                {
                    string responseText = reader.ReadToEnd();
                    result = JsonConvert.DeserializeObject< PlateReaderResult>(responseText);
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static PlateReaderResult Read(string postUrl, byte[] data, string token)
        {
            try
            {
                PlateReaderResult result = null;
                string formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());
                string contentType = "multipart/form-data; boundary=" + formDataBoundary;
                byte[] formData = GetMultipartFormData2("--", "", formDataBoundary, data);

                HttpWebRequest request = WebRequest.Create(postUrl) as HttpWebRequest;
                request.ReadWriteTimeout = 2000;
                // Set up the request properties.
                request.Method = "POST";
                request.ContentType = contentType;
                request.UserAgent = ".NET Framework CSharp Client";
                request.CookieContainer = new CookieContainer();
                request.ContentLength = formData.Length;
                request.Headers.Add("Authorization", "Token " + token);

                // Send the form data to the request.
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(formData, 0, formData.Length);
                    requestStream.Close();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if(response.StatusCode == HttpStatusCode.OK)
                {
                    WebHeaderCollection header = response.Headers;
                    var encoding = ASCIIEncoding.ASCII;
                    using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
                    {
                        string responseText = reader.ReadToEnd();
                        try
                        {
                            result = JsonConvert.DeserializeObject<PlateReaderResult>(responseText);
                        }
                        catch
                        {

                        }
                    }
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch 
            {
                return null;
            }
        }

        /// <summary>
        /// Build Multipart Formdata from files and regions.
        /// </summary>
        /// <param name="filePath">File path.</param>
        /// <param name="regions">Regions</param>
        /// <param name="boundary">Boundary.</param>
        /// <returns></returns>
        private static byte[] GetMultipartFormData(string filePath, string regions, string boundary)
        {
            Stream formDataStream = new System.IO.MemoryStream();
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                // Add just the first part of this param, since we will write the file data directly to the Stream
                string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                    boundary,
                    "upload",
                    filePath,
                    "application/octet-stream");

                formDataStream.Write(Encoding.UTF8.GetBytes(header), 0, Encoding.UTF8.GetByteCount(header));
                Byte[] file = File.ReadAllBytes(filePath);
                // Write the file data directly to the Stream, rather than serializing it to a string.
                formDataStream.Write(file, 0, file.Length);
            }

            if (!string.IsNullOrWhiteSpace(regions))
            {
                formDataStream.Write(Encoding.UTF8.GetBytes("\r\n"), 0, Encoding.UTF8.GetByteCount("\r\n"));
                string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                    boundary,
                    "regions",
                    regions);
                formDataStream.Write(Encoding.UTF8.GetBytes(postData), 0, Encoding.UTF8.GetByteCount(postData));
            }

            // Add the end of the request.  Start with a newline
            string footer = "\r\n--" + boundary + "--\r\n";
            formDataStream.Write(Encoding.UTF8.GetBytes(footer), 0, Encoding.UTF8.GetByteCount(footer));

            // Dump the Stream into a byte[]
            formDataStream.Position = 0;
            byte[] formData = new byte[formDataStream.Length];
            formDataStream.Read(formData, 0, formData.Length);
            formDataStream.Close();

            return formData;
        }

        private static byte[] GetMultipartFormData2(string filePath, string regions, string boundary, byte[] data)
        {
            Stream formDataStream = new System.IO.MemoryStream();
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                // Add just the first part of this param, since we will write the file data directly to the Stream
                string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                    boundary,
                    "upload",
                    filePath,
                    "application/octet-stream");

                formDataStream.Write(Encoding.UTF8.GetBytes(header), 0, Encoding.UTF8.GetByteCount(header));
                Byte[] file = data;//File.ReadAllBytes(filePath);
                // Write the file data directly to the Stream, rather than serializing it to a string.
                formDataStream.Write(file, 0, file.Length);
            }

            if (!string.IsNullOrWhiteSpace(regions))
            {
                formDataStream.Write(Encoding.UTF8.GetBytes("\r\n"), 0, Encoding.UTF8.GetByteCount("\r\n"));
                string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                    boundary,
                    "regions",
                    regions);
                formDataStream.Write(Encoding.UTF8.GetBytes(postData), 0, Encoding.UTF8.GetByteCount(postData));
            }

            // Add the end of the request.  Start with a newline
            string footer = "\r\n--" + boundary + "--\r\n";
            formDataStream.Write(Encoding.UTF8.GetBytes(footer), 0, Encoding.UTF8.GetByteCount(footer));

            // Dump the Stream into a byte[]
            formDataStream.Position = 0;
            byte[] formData = new byte[formDataStream.Length];
            formDataStream.Read(formData, 0, formData.Length);
            formDataStream.Close();

            return formData;
        }

        // Get byte array from image
        public static byte[] GetByteArray(Image image)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                image.Save(ms, ImageFormat.Jpeg);
                byte[] bytearray = ms.ToArray();
                return bytearray;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occur while get byte array from image: " + ex.Message);
                return null;
            }
        }


        // Get image from byte array
        public static Image GetImage(byte[] bytearray)
        {
            try
            {
                MemoryStream mem = new MemoryStream();
                mem.Write(bytearray, 0, bytearray.Length);
                Image image = Image.FromStream(mem);
                return image;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occur while get image from byte array: " + ex.Message);
                return null;
            }
        }
    }
}
