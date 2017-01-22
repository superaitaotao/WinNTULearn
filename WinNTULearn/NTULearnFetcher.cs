using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinNTULearn
{
    class NTULearnFetcher
    {
        private string[] userAgents = new string[]{"Mozilla/5.0 (Windows NT 6.1; WOW64; rv:41.0) Gecko/20100101 Firefox/41.0",
              "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36",
              "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.80 Safari/537.36",
              "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.71 Safari/537.36",
              "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11) AppleWebKit/601.1.56 (KHTML, like Gecko) Version/9.0 Safari/601.1.56",
              "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.80 Safari/537.36",
              "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_1) AppleWebKit/601.2.7 (KHTML, like Gecko) Version/9.0.1 Safari/601.2.7",
              "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko" };

        enum FetchResult
        {
            case logInError(Error?)
            case courseListRetrievalError(Error?)
            case fileDownloadError(Error?)
            case success(Data)
        }

        public static string DownloadFinishedKey = "DownloadFinishedKey";


        private string baseUrl = "https://ntulearn.ntu.edu.sg";
        private string baseFileUrl = NSHomeDirectory() + "/NTULearn";
        private string username;
        private string password;
        private Session URLSession = URLSession.shared;

        private string getUrl(string url) {
            if( url.Length == 0) {
                return "";
            }
        
            if( url[0] == '/' ){
                return baseUrl + url;
            } else {
                return url;
            }
       }
    }
}
