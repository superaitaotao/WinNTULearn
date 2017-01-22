using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinNTULearn.Properties;

namespace WinNTULearn
{
    class MyUserDefault
    {
        public static MyUserDefault sharedInstance = new MyUserDefault();

        private Settings userDefault = Properties.Settings.Default;

        public void saveCourseFolders(CourseInfo[] courseFolders)
        {
            Console.WriteLine("course folders saved");

            List<Dictionary<string, object>> allCourseInfo = new List<Dictionary<string, object>>();

            Dictionary<string, object> oneCourseInfo;

            List<List<object>> oneCourseFolders;

            foreach (CourseInfo course in courseFolders)
            {

                oneCourseInfo = new Dictionary<string, object>();

                oneCourseInfo["courseName"] = proccessCourseName(course.name);

                oneCourseInfo["isChecked"] = course.isChecked;

                oneCourseFolders = new List<List<object>>();

                for (int i = 0; i < course.folders.Count; i++)
                {

                    oneCourseFolders.Add(new List<object> { course.folders[i], course.foldersChecked[i], course.foldersUrl[i] });

                }

                oneCourseInfo["folders"] = oneCourseFolders;

                allCourseInfo.Add(oneCourseInfo);

            }

            this.userDefault.courseFoldersSetting = allCourseInfo;

        }

        public List<CourseInfo> getCourseFolders()
        {
            List<CourseInfo> courseFolders = new List<CourseInfo>();

            List<Dictionary<string, object>> courseArray = userDefault.courseFoldersSetting;

            CourseInfo oneCourseInfo;

            List<Boolean> foldersChecked;

            List<string> folders;

            List<string> foldersUrl;

            if (courseArray != null)
            {

                foreach (Dictionary<string, object> course in courseArray)
                {

                    foldersChecked = new List<Boolean>();

                    folders = new List<string>();

                    foldersUrl = new List<string>();

                    foreach (List<object> folder in (List<List<object>>)course["folders"])
                    {

                        folders.Add((string)folder[0]);

                        foldersChecked.Add((bool)folder[1]);

                        foldersUrl.Add((string)folder[2]);
                    }

                    oneCourseInfo = new CourseInfo((string)course["courseName"], folders, (bool)course["isChecked"], foldersChecked, foldersUrl);

                    courseFolders.Add(oneCourseInfo);
                }

            }
            return courseFolders;
        }

        //supposed to modify the course name to be shorter...
        public string proccessCourseName(string name)
        {
            return name;
        }

        public bool isSavedCourseFoldersPresent()
        {

            return userDefault.courseFoldersSetting != null;
        }

        public List<FileInfo> getLatestDownloadedFiles()
        {

            List<List<object>> filesDefault = userDefault.latestDownloadedFiles;

            List<FileInfo> files = new List<FileInfo>();

            FileInfo oneFile;

            if (filesDefault != null)
            {

                foreach (List<object> file in filesDefault)
                {

                    oneFile = new FileInfo((string)file[0], (string)file[1], (DateTime)file[2], (string)file[3]);

                    files.Add(oneFile);
                }

            }

            return files;

        }

        public void saveLatestDownloadedFiles(List<FileInfo> files)
        {

            Console.WriteLine("save latest downloaded files");

            List<List<object>> filesDefault = new List<List<object>>();



            foreach (FileInfo file in files)
            {

                filesDefault.Add(new List<object> { file.fileName, file.courseName, file.syncDate, file.fileUrl });

            }

            userDefault.latestDownloadedFiles = filesDefault;
        }

        public void addLatestDownloadedFile(FileInfo file)
        {
            var files = getLatestDownloadedFiles();

            files.Add(file);

            if (files.Count > 10)
            {

                files.RemoveAt(0);

            }

            saveLatestDownloadedFiles(files: files);

        }

        public void sync()
        {
            userDefault.Save();
        }

        public string getUsername()
        {
            return userDefault.username;
        }

        public string getPassword()
        {
            return userDefault.password;
        }

        public void saveCredential(string username, string password)
        {

            Console.WriteLine("saved credentials");


            userDefault.username = username;


            userDefault.password = password;
        }

        public void saveDownloadedFileUrls(ISet<string> fileUrls)
        {

            List<string> urls = new List<string>(fileUrls);

            userDefault.donwloadedFileUrls = urls;
        }

        public ISet<string> getDownloadedFileUrls()
        {

            List<string> urls = userDefault.donwloadedFileUrls;

            ISet<string> set;

            if (urls != null)
            {
                set = new HashSet<string>(urls);
            }
            else
            {
                set = new HashSet<string>();
            }

            return set;
        }

        public void refresh()
        {

            userDefault.donwloadedFileUrls = new List<string>();
            userDefault.Save();
            NTULearnFetcher.downloadedFileUrls = new List<string>();
        }
    }
}
