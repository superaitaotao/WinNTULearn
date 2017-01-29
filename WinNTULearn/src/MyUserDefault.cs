using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinNTULearn.Properties;

namespace WinNTULearn
{
    /// <summary>
    /// Saves user information to properties.
    /// </summary>
    class MyUserDefault
    {
        /// <summary>
        /// Singleton instance of this class.
        /// </summary>
        public static MyUserDefault sharedInstance = new MyUserDefault();

        /// <summary>
        /// Default Settings instance.
        /// </summary>
        private Settings userDefault = Properties.Settings.Default;

        /// <summary>
        /// Saves course folders to settings.
        /// </summary>
        /// <param name="courseFolders">List of CourseInfo.</param>
        public void saveCourseFolders(CourseInfo[] courseFolders)
        {
            // Log
            Console.WriteLine("saving course folders...");

            // Declarations.
            List<Dictionary<string, object>> allCourseInfo = new List<Dictionary<string, object>>();
            Dictionary<string, object> oneCourseInfo;
            List<List<object>> oneCourseFolders;

            // Add Course one by one.
            foreach ( CourseInfo course in courseFolders )
            {
                // Declare OneCourseInfo.
                oneCourseInfo = new Dictionary<string, object>();

                // Add course name.
                oneCourseInfo["courseName"] = proccessCourseName(course.name);

                // Add isChecked.
                oneCourseInfo["isChecked"] = course.isChecked;

                // Add course folders.
                oneCourseFolders = new List<List<object>>();
                for ( int i = 0; i < course.folders.Count; i++ )
                {
                    oneCourseFolders.Add(new List<object> { course.folders[i], course.foldersChecked[i], course.foldersUrl[i] });
                }
                oneCourseInfo["folders"] = oneCourseFolders;

                // Add one course info to allCourseInfo.
                allCourseInfo.Add(oneCourseInfo);
            }

            // Save course info to Settings.
            this.userDefault.courseFoldersSetting = allCourseInfo;
        }

        /// <summary>
        /// Gets list of course folders from Settings.
        /// </summary>
        /// <returns>List of course info.</returns>
        public List<CourseInfo> getCourseFolders()
        {
            // Initiations
            List<CourseInfo> courseFolders = new List<CourseInfo>();
            CourseInfo oneCourseInfo;
            List<Boolean> foldersChecked;
            List<string> folders;
            List<string> foldersUrl;

            // Get course folders from setting.
            List<Dictionary<string, object>> courseArray = userDefault.courseFoldersSetting;
            if ( courseArray != null )
            {
                foreach ( Dictionary<string, object> course in courseArray )
                {
                    foldersChecked = new List<Boolean>();
                    folders = new List<string>();
                    foldersUrl = new List<string>();
                    foreach ( List<object> folder in (List<List<object>>)course["folders"] )
                    {
                        folders.Add((string)folder[0]);
                        foldersChecked.Add((bool)folder[1]);
                        foldersUrl.Add((string)folder[2]);
                    }
                    oneCourseInfo = new CourseInfo((string)course["courseName"], folders, (bool)course["isChecked"], foldersChecked, foldersUrl);
                    courseFolders.Add(oneCourseInfo);
                }
            }

            // Return list of course folders.
            return courseFolders;
        }

        /// <summary>
        /// Supposed to modify the course name to be shorter...
        /// </summary>
        /// <param name="name">Course name.</param>
        /// <returns>Shortened course name.</returns>
        public string proccessCourseName(string name)
        {
            // Return shortened name.
            return name;
        }

        /// <summary>
        /// Checks whether CourseFolders is present.
        /// </summary>
        /// <returns>Whether courses were saved.</returns>
        public bool isSavedCourseFoldersPresent()
        {

            // Return whether course folders are present.
            return userDefault.courseFoldersSetting != null;
        }

        /// <summary>
        /// Gets list of latest downloaded files.
        /// </summary>
        /// <returns>list of latest file infos.</returns>
        public List<FileInfo> getLatestDownloadedFiles()
        {
            // Declarations.
            List<FileInfo> files = new List<FileInfo>();
            FileInfo oneFile;

            // Get latest files from Settings.
            List<List<object>> filesDefault = userDefault.latestDownloadedFiles;
            if ( filesDefault != null )
            {
                foreach ( List<object> file in filesDefault )
                {
                    oneFile = new FileInfo((string)file[0], (string)file[1], (DateTime)file[2], (string)file[3]);
                    files.Add(oneFile);
                }
            }

            // Return list of latest downloaded FileInfos.
            return files;
        }

        /// <summary>
        /// Saves list of latest downloaded files.
        /// </summary>
        /// <param name="files">List of Files' info.</param>
        public void saveLatestDownloadedFiles(List<FileInfo> files)
        {
            // Log
            Console.WriteLine("save latest downloaded files");

            // Add file info to filesDefault.
            List<List<object>> filesDefault = new List<List<object>>();
            foreach ( FileInfo file in files )
            {
                filesDefault.Add(new List<object> { file.fileName, file.courseName, file.syncDate, file.fileUrl });
            }

            // Save to Settings.
            userDefault.latestDownloadedFiles = filesDefault;
        }

        /// <summary>
        /// Adds latest downloaded file to existing list.
        /// </summary>
        /// <param name="file">File info of the new file.</param>
        public void addLatestDownloadedFile(FileInfo file)
        {
            // Add new file to existing list.
            var files = getLatestDownloadedFiles();
            files.Insert(0, file);

            // Remove last item if more than 10 files are present
            if ( files.Count > 10 )
                files.RemoveAt(files.Count - 1);

            // Save the new list of latest files
            saveLatestDownloadedFiles(files: files);
        }

        /// <summary>
        /// Save latest settings. (This method may not be used.. removed later)
        /// </summary>
        public void sync()
        {
            userDefault.Save();
        }

        /// <summary>
        /// Gets saved username from Settings.
        /// </summary>
        /// <returns>Username.</returns>
        public string getUsername()
        {
            // Return username.
            return userDefault.username;
        }

        /// <summary>
        /// Gets saved password from Settings.
        /// </summary>
        /// <returns>Password.</returns>
        public string getPassword()
        {
            // Return password.
            return userDefault.password;
        }

        /// <summary>
        /// Saves both username and password. 
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        public void saveCredential(string username, string password)
        {
            // Log
            Console.WriteLine("saved credentials");

            // Save username, password to Settings. 
            userDefault.username = username;
            userDefault.password = password;
        }

        /// <summary>
        /// Saves downloaded file urls to Settings. This is used to track which files are downloaded so that they wont be downloaded again.
        /// </summary>
        /// <param name="fileUrls">List of file urls.</param>
        public void saveDownloadedFileUrls(ISet<string> fileUrls)
        {

            // Save list of downloaded file urls.
            List<string> urls = new List<string>(fileUrls);
            userDefault.donwloadedFileUrls = urls;
        }

        /// <summary>
        /// Returns a set of downloaded file urls.
        /// </summary>
        /// <returns>Set of downloaded file urls.</returns>
        public ISet<string> getDownloadedFileUrls()
        {

            // Declaration
            ISet<string> set;

            // Get list of downloaded urls from Settings.
            List<string> urls = userDefault.donwloadedFileUrls;
            if ( urls != null )
                set = new HashSet<string>(urls);
            else
                set = new HashSet<string>();

            // Return url list.
            return set;
        }

        /// <summary>
        /// Clears downloaded file urls in Settings. This method may not be needed, remove later.
        /// </summary>
        public void refresh()
        {
            userDefault.donwloadedFileUrls = new List<string>();
            userDefault.Save();
            NTULearnFetcher.downloadedFileUrls = new List<string>();
        }
    }
}
