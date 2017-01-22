using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        public static string DownloadFinishedKey = "DownloadFinishedKey";

        private string baseUrl = "https://ntulearn.ntu.edu.sg";
        private string baseFileUrl = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/NTULearn";
        private string username;
        private string password;

        Queue logInQueue = Queue.Synchronized(new Queue());
        Queue downloadFileQueue = Queue.Synchronized(new Queue());
        Queue helperQueue = Queue.Synchronized(new Queue());

        List<CourseInfo> courseFolders = new List<CourseInfo>();
        static ISet<string> downloadedFileUrls = MyUserDefault.sharedInstance.getDownloadedFileUrls();
        int noOfDownloadedFiles = 0;

        ISet<string> excludedCourses = new HashSet<string>() { "Home Page", "Announcements", "Tools", "Help", "Library Resources", "Information", "Groups" };
        int numberOfCourses = 0;

        public void logIn()
        {

            Console.WriteLine("logging in ...");

            if (username == null || password == null)
            {
                username = MyUserDefault.sharedInstance.getUsername();
                password = MyUserDefault.sharedInstance.getPassword();
            }

            LoginTask(username, password).Start();

            username = null;
            password = null;
        }

        public async Task<string> LoginTask(string username, string password)
        {
            var clientHandler = new HttpClientHandler();

            using (var client = new HttpClient(clientHandler))
            {
                string postString = "user_id=" + username + "&password=" + password;
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] postData = encoding.GetBytes(postString);
                var content = new ByteArrayContent(postData);
                var response = await client.PostAsync(new Uri("http://example.com/postImage"), content);
                return await response.Content.ReadAsStringAsync();
            }
        }

        private string getUrl(string url)
        {
            if (url.Length == 0)
            {
                return "";
            }

            if (url[0] == '/')
            {
                return baseUrl + url;
            }
            else
            {
                return url;
            }
        }
    }
}
