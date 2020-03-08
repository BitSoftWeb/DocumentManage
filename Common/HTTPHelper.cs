using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Collections;
using System.Net;


using ServiceStack;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Common
{
    public class HTTPHelper
    {
        //private Cookie sessionidCookie = null;
        //public static string url = "https://114.115.220.70:8011/";

        
            /// <summary>
            /// Sets the cert policy.
            /// </summary>
            public static void SetCertificatePolicy()
            {
                ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
            }

            /// <summary>
            /// Remotes the certificate validate.
            /// </summary>
            private static bool RemoteCertificateValidate( object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
            {
                // trust any certificate!!!
                System.Console.WriteLine("Warning, trust any certificate");
                return true;
            }
        

        #region
        //public String setFilesToHttpWebServer(String url, Hashtable data, CookieCollection cookies, Hashtable filesSaveAddress)

        //{

        //    //用于缓存服务器端传输回来的结果字符串

        //    string result = null;

        //    if (string.IsNullOrEmpty(url))

        //    {

        //        return null;//传入参数异常

        //    }

        //    //用于分割信息部分的分隔符(不能与消息原文冲突)

        //    String boundary = "HttpWebTool" + DateTime.Now.Ticks;

        //    //结束分隔符数据流

        //    byte[] andBoundary = Encoding.UTF8.GetBytes("--" + boundary + "--");

        //    //新行字符串数据流

        //    byte[] newline = Encoding.UTF8.GetBytes("\r\n");

        //    try

        //    {

        //        HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);

        //        //请求方式

        //        req.Method = "POST";

        //        //声明客户端只接收txt类型的内容

        //        req.Accept = "text/plain";

        //        //以消息的形式向服务器传递参数和数据流

        //        req.ContentType = "multipart/form-data; boundary=" + boundary;

        //        //设置cookie盒子(客户端请求的cookie和服务器端返回的cookie就放在此盒子中)

        //        CookieContainer cookieContainer = new CookieContainer();

        //        if (sessionidCookie != null && !string.IsNullOrEmpty(sessionidCookie.Domain))

        //        {

        //            cookieContainer.Add(sessionidCookie);

        //        }

        //        if (cookies != null)

        //        {

        //            cookieContainer.Add(cookies);//添加调用者传入的cookie集合

        //        }

        //        req.CookieContainer = cookieContainer;

        //        //用于累计数据流长度，初始化为0

        //        long dataCount = 0;

        //        byte[] parameterBytes = getParameterBytes(data, boundary);

        //        if (parameterBytes != null && parameterBytes.Length > 0)

        //        {

        //            //累计请求参数字符串数据流大小

        //            dataCount += parameterBytes.Length;

        //        }

        //        //<控件名，上传文件的消息头部分字符流byte[]>

        //        Hashtable uploadFileDeclareBytesSet = new Hashtable();

        //        //如果有要上传的文件

        //        if (filesSaveAddress != null && filesSaveAddress.Count > 0)

        //        {

        //            foreach (DictionaryEntry de in filesSaveAddress)

        //            {

        //                //如果将要上传的文件存在

        //                if (File.Exists(de.Value.ToString()))

        //                {

        //                    byte[] uploadFileDeclareBytes = getUploadFileDeclareBytes(de, boundary);

        //                    if (uploadFileDeclareBytes != null)

        //                    {

        //                        //累计上传文件消息头部描述字符串数据流大小

        //                        dataCount += uploadFileDeclareBytes.Length;

        //                        //累计上传文件正文数据流大小

        //                        dataCount += new FileInfo(de.Value.ToString()).Length;

        //                        //累计新行字符串数据流数据流大小

        //                        dataCount += newline.Length;

        //                        uploadFileDeclareBytesSet.Add(de.Key.ToString(), uploadFileDeclareBytes);

        //                    }

        //                }

        //            }

        //        }

        //        //如果有数据流

        //        if (dataCount > 0)

        //        {

        //            //累计结束分隔符数据流大小

        //            dataCount += andBoundary.Length;

        //            //请求数据流的长度

        //            req.ContentLength = dataCount;

        //            using (Stream requestStream = req.GetRequestStream())

        //            {

        //                if (parameterBytes != null && parameterBytes.Length > 0)

        //                {

        //                    requestStream.Write(parameterBytes, 0, parameterBytes.Length);

        //                }

        //                if (filesSaveAddress != null && filesSaveAddress.Count > 0)

        //                {

        //                    foreach (DictionaryEntry de in filesSaveAddress)

        //                    {

        //                        if (File.Exists(de.Value.ToString()))

        //                        {

        //                            byte[] uploadFileDeclareBytes = (byte[])uploadFileDeclareBytesSet[de.Key.ToString()];

        //                            requestStream.Write(uploadFileDeclareBytes, 0, uploadFileDeclareBytes.Length);

        //                            using (FileStream fileStream = new FileStream(de.Value.ToString(), FileMode.Open, FileAccess.Read))

        //                            {

        //                                //建立字节组，并设置它的大小是多少字节

        //                                byte[] bytes = new byte[10240];

        //                                int n = -1;

        //                                while ((n = fileStream.Read(bytes, 0, bytes.Length)) > 0)

        //                                {

        //                                    requestStream.Write(bytes, 0, n); //将指定字节的流信息写入文件流中

        //                                }

        //                            }

        //                            requestStream.Write(newline, 0, newline.Length);

        //                        }

        //                    }

        //                }

        //                requestStream.Write(andBoundary, 0, andBoundary.Length);

        //            }

        //        }

        //        //接收返回值

        //        using (HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse())
        //        {

        //            using (StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8))
        //            {

        //                result = reader.ReadToEnd().Trim();

        //            }

        //            if (myResponse.Cookies["SESSIONID"] != null)

        //            {

        //                sessionidCookie = myResponse.Cookies["SESSIONID"];

        //            }

        //            else

        //            {

        //                if (myResponse.Cookies["JSESSIONID"] != null)

        //                {

        //                    sessionidCookie = myResponse.Cookies["JSESSIONID"];

        //                }

        //            }

        //        }

        //    }

        //    catch (Exception)

        //    {

        //        Console.WriteLine("请查看传入参数是否正确或者服务器是否关闭");

        //    }

        //    return result;

        //}

        //private byte[] getParameterBytes(Hashtable data, String boundary)

        //{

        //    byte[] parameterBytes = null;

        //    //如果有请求参数

        //    if (data != null && data.Count > 0)

        //    {

        //        string parameterStr = "";

        //        foreach (DictionaryEntry de in data)

        //        {

        //            parameterStr += "--" + boundary;

        //            parameterStr += "\r\n" + "Content-Disposition: form-data;name=\"" + de.Key.ToString() + "\"";

        //            parameterStr += "\r\n" + "Content-Type: text/plain; charset=UTF-8";

        //            parameterStr += "\r\n\r\n" + de.Value.ToString();

        //            parameterStr += "\r\n";

        //        }

        //        if (!string.IsNullOrEmpty(parameterStr))

        //        {

        //            parameterBytes = Encoding.UTF8.GetBytes(parameterStr);//将上传字符串数据打包成数据流

        //        }

        //    }

        //    return parameterBytes;

        //}

        /////

        ///// 获得上传文件的消息头部分字符流，以"\r\n\r\n"结尾

        /////

        ///// 上传文件《控件名,上传文件的保存位置(包括"文件名"."扩展名")》

        ///// 消息分隔符

        ///// 返回上传文件的消息头部分字符流，返回会为null代表获得失败

        //private byte[] getUploadFileDeclareBytes(DictionaryEntry de, String boundary)

        //{

        //    byte[] uploadFileDeclareBytes = null;

        //    //上传文件的消息头描述部分

        //    string uploadFileDeclareStr = "";

        //    uploadFileDeclareStr += "--" + boundary;

        //    uploadFileDeclareStr += "\r\n" + "Content-Disposition: form-data;name=\"" + de.Key.ToString() + "\"; filename=\"" + de.Value.ToString() + "\"";

        //    uploadFileDeclareStr += "\r\n" + "Content-Type: application/octet-stream";

        //    uploadFileDeclareStr += "\r\n\r\n";

        //    if (!string.IsNullOrEmpty(uploadFileDeclareStr))

        //    {

        //        uploadFileDeclareBytes = Encoding.UTF8.GetBytes(uploadFileDeclareStr);//将上传字符串数据打包成数据流

        //    }

        //    return uploadFileDeclareBytes;

        //}
        #endregion

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        ///// <summary>
        /////  将数据缓冲区(一般是指文件流或内存流对应的字节数组)上载到由 URI 标识的资源。(包含body数据)
        ///// </summary>
        ///// <param name="url">请求目标URL</param>
        ///// <param name="data">主体数据(字节数据)</param>
        ///// <param name="method">请求的方法。请使用 WebRequestMethods.Http 的枚举值</param>
        ///// <param name="contentType"><see langword="Content-type" /> HTTP 标头的值。请使用 ContentType 类的常量来获取。默认为 application/octet-stream</param>
        ///// <returns>HTTP-POST的响应结果</returns>
        //public HttpResult UploadData(string url, byte[] data, string method = WebRequestMethods.Http.Post, string contentType = HttpContentType.APPLICATION_OCTET_STREAM)
        //{
        //    HttpResult httpResult = new HttpResult();
        //    HttpWebRequest httpWebRequest = null;

        //    try
        //    {
        //        httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
        //        httpWebRequest.Method = method;
        //        httpWebRequest.Headers = HeaderCollection;
        //        httpWebRequest.CookieContainer = CookieContainer;
        //        httpWebRequest.ContentLength = data.Length;
        //        httpWebRequest.ContentType = contentType;
        //        httpWebRequest.UserAgent = _userAgent;
        //        httpWebRequest.AllowAutoRedirect = _allowAutoRedirect;
        //        httpWebRequest.ServicePoint.Expect100Continue = false;

        //        if (data != null)
        //        {
        //            httpWebRequest.AllowWriteStreamBuffering = true;
        //            using (Stream requestStream = httpWebRequest.GetRequestStream())
        //            {
        //                requestStream.Write(data, 0, data.Length);
        //                requestStream.Flush();
        //            }
        //        }

        //        HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
        //        if (httpWebResponse != null)
        //        {
        //            GetResponse(ref httpResult, httpWebResponse);
        //            httpWebResponse.Close();
        //        }
        //    }
        //    catch (WebException webException)
        //    {
        //        GetWebExceptionResponse(ref httpResult, webException);
        //    }
        //    catch (Exception ex)
        //    {
        //        GetExceptionResponse(ref httpResult, ex, method, contentType);
        //    }
        //    finally
        //    {
        //        if (httpWebRequest != null)
        //        {
        //            httpWebRequest.Abort();
        //        }
        //    }

        //    return httpResult;
        //}

        ///// <summary>
        ///// 获取HTTP的响应信息
        ///// </summary>
        ///// <param name="httpResult">即将被HTTP请求封装函数返回的HttpResult变量</param>
        ///// <param name="httpWebResponse">正在被读取的HTTP响应</param>
        //private void GetResponse(ref HttpResult httpResult, HttpWebResponse httpWebResponse)
        //{
        //    httpResult.HttpWebResponse = httpWebResponse;
        //    httpResult.Status = HttpResult.STATUS_SUCCESS;
        //    httpResult.StatusDescription = httpWebResponse.StatusDescription;
        //    httpResult.StatusCode = (int)httpWebResponse.StatusCode;

        //    if (ReadMode == ResponseReadMode.Binary)
        //    {
        //        int len = (int)httpWebResponse.ContentLength;
        //        httpResult.Data = new byte[len];
        //        int bytesLeft = len;
        //        int bytesRead = 0;

        //        using (BinaryReader br = new BinaryReader(httpWebResponse.GetResponseStream()))
        //        {
        //            while (bytesLeft > 0)
        //            {
        //                bytesRead = br.Read(httpResult.Data, len - bytesLeft, bytesLeft);
        //                bytesLeft -= bytesRead;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        using (StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream()))
        //        {
        //            httpResult.Text = sr.ReadToEnd();
        //        }
        //    }
        //}


        ///// <summary>
        ///// 获取HTTP访问网络期间发生错误时引发的异常响应信息
        ///// </summary>
        ///// <param name="httpResult">即将被HTTP请求封装函数返回的HttpResult变量</param>
        ///// <param name="webException">访问网络期间发生错误时引发的异常对象</param>
        //private void GetWebExceptionResponse(ref HttpResult httpResult, WebException webException)
        //{
        //    HttpWebResponse exResponse = webException.Response as HttpWebResponse;
        //    if (exResponse != null)
        //    {
        //        httpResult.HttpWebResponse = exResponse;
        //        httpResult.Status = HttpResult.STATUS_FAIL;
        //        httpResult.StatusDescription = exResponse.StatusDescription;
        //        httpResult.StatusCode = (int)exResponse.StatusCode;

        //        httpResult.RefCode = httpResult.StatusCode;
        //        using (StreamReader sr = new StreamReader(exResponse.GetResponseStream(), EncodingType))
        //        {
        //            httpResult.Text = sr.ReadToEnd();
        //            httpResult.RefText = httpResult.Text;
        //        }

        //        exResponse.Close();
        //    }
        //}


        ///// <summary>
        ///// 获取HTTP的异常响应信息
        ///// </summary>
        ///// <param name="httpResult">即将被HTTP请求封装函数返回的HttpResult变量</param>
        ///// <param name="ex">异常对象</param>
        ///// <param name="method">HTTP请求的方式</param>
        ///// <param name="contentType">HTTP的标头类型</param>
        //private void GetExceptionResponse(ref HttpResult httpResult, Exception ex, string method, string contentType = "")
        //{
        //    contentType = string.IsNullOrWhiteSpace(contentType) ? string.Empty : "-" + contentType;
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendFormat("[{0}] [{1}] [HTTP-" + method + contentType + "] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), _userAgent);
        //    Exception exception = ex;
        //    while (exception != null)
        //    {
        //        sb.AppendLine(exception.Message + " ");
        //        exception = exception.InnerException;
        //    }
        //    sb.AppendLine();

        //    httpResult.HttpWebResponse = null;
        //    httpResult.Status = HttpResult.STATUS_FAIL;

        //    httpResult.RefCode = (int)HttpStatusCode2.USER_UNDEF;
        //    httpResult.RefText += sb.ToString();
        //}


    }
}
