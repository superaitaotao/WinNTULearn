using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinNTULearn
{
    public class CourseInfo
    {

        public string name { get; set; }

        public List<string> folders { get; set; }

        public List<Boolean> foldersChecked { get; set; }

        public List<string> foldersUrl { get; set; }

        public bool isChecked { get; set; }

        public CourseInfo(string name, List<string> folders, List<string> foldersUrl)
        {
            int i, count = folders.Count;
            this.isChecked = false;
            this.foldersChecked = new List<Boolean>(count);
            this.name = name;
            this.folders = folders;
            this.foldersUrl = foldersUrl;

            for(i=0;i<count;i++)
            {
                this.foldersChecked[i] = false;
            }
        }

        public CourseInfo(string name, List<string> folders, bool isChecked, List<Boolean> foldersChecked, List<string> foldersUrl)
        {
            this.isChecked = isChecked;
            this.foldersChecked = foldersChecked;
            this.name = name;
            this.folders = folders;
            this.foldersUrl = foldersUrl;
        }

        public override string ToString() {

            StringBuilder builder = new StringBuilder();

            builder.Append(this.name + " : " + (this.isChecked ? "True" : "False") + "\n");

            for(int i = 0; i < this.folders.Count; i++) {

                builder.Append(folders[i] + (foldersChecked[i] ? "True" : "False") + "\n");

            }

            builder.Append("\n");

            return builder.ToString();
        }

    }
}
