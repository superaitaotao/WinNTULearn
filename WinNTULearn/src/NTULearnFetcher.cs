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
    /// <summary>
    /// Deals with all Internet related items in the app.
    /// </summary>
    public class NTULearnFetcher
    {
        /// <summary>
        /// List of user agents that can be choose to use (anti-crawling).
        /// </summary>
        private string[] userAgents = new string[]{"Mozilla/5.0 (Windows NT 6.1; WOW64; rv:41.0) Gecko/20100101 Firefox/41.0",
              "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36",
              "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.80 Safari/537.36",
              "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.71 Safari/537.36",
              "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11) AppleWebKit/601.1.56 (KHTML, like Gecko) Version/9.0 Safari/601.1.56",
              "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.80 Safari/537.36",
              "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_1) AppleWebKit/601.2.7 (KHTML, like Gecko) Version/9.0.1 Safari/601.2.7",
              "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko" };

        /// <summary>
        /// Base url.
        /// </summary>
        private readonly string baseUrl = "https://ntulearn.ntu.edu.sg";

        /// <summary>
        /// Base file url.
        /// </summary>
        private string baseFileUrl = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/NTULearn";

        private List<CourseInfo> courseFolders = new List<CourseInfo>();
        private ISet<string> downloadedFileUrls = MyUserDefault.sharedInstance.getDownloadedFileUrls();

        ISet<string> excludedCourses = new HashSet<string>() { "Home Page", "Announcements", "Tools", "Help", "Library Resources", "Information", "Groups" };

        // Cookie container from log in, shared with other network activities
        private CookieContainer myCookieContainer;

        /// <summary>
        /// Logs in async.
        /// </summary>
        async public Task<NTUFectherResult> LogInAsync()
        {
            // Set to accept TLS1.2 and above...
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            // Log
            Console.WriteLine("logging in ...");

            // Get saved username & password
            //string username = MyUserDefault.sharedInstance.getUsername();
            string username = "sxu007";
            //string password = MyUserDefault.sharedInstance.getPassword();
            string password = "Galaxy1234#";

            // Log in uri
            string logInUri = this.baseUrl + "/webapps/login/";

            // Log in
            using ( var clientHandler = new HttpClientHandler { CookieContainer = new CookieContainer() } )
            {
                // Set myCookieContainer
                this.myCookieContainer = clientHandler.CookieContainer;

                // Use HttpClient to log in
                using ( var client = new HttpClient(clientHandler) )
                {
                    // Set a random UserAgent
                    client.DefaultRequestHeaders.Add("User-Agent", this.userAgents[new Random().Next(this.userAgents.Length)]);

                    // Set request message
                    var content = new FormUrlEncodedContent(new Dictionary<string, string>{
                            { "user_id", username},
                            { "password", password }
                        });

                    // Get response
                    HttpResponseMessage response = await client.PostAsync(logInUri, content);

                    //FileStream stream = new FileStream(@"C:\Dev\response.html", FileMode.Create);
                    //await response.Content.CopyToAsync(stream);

                    response.EnsureSuccessStatusCode();
                    Console.WriteLine("status code", response.StatusCode);

                    // Decide whether log in is successful by looking at whether response contains "Course List"
                    if ( response.Content.ToString().Contains("Course List") )
                        return new NTUFectherResult(NTUFetcherResultType.Success, "");
                    else
                        return new NTUFectherResult(NTUFetcherResultType.LogInError, "Log In failed");
                };
            }
        }

        private string getUrl(string url)
        {
            if ( url.Length == 0 )
            {
                return "";
            }

            if ( url[0] == '/' )
            {
                return baseUrl + url;
            }
            else
            {
                return url;
            }
        }
    }

    public class NTUFectherResult
    {
        public NTUFetcherResultType Type { get; set; }
        public string Message { get; set; }

        public NTUFectherResult(NTUFetcherResultType type, string message)
        {
            this.Type = type;
            this.Message = message;
        }
    }

    public enum NTUFetcherResultType
    {
        Success,
        LogInError,
        GetCourseListError,
        DownloadFileError
    }
}
