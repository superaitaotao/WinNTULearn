using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinNTULearn
{
    /// <summary>
    /// Contains info of a course.
    /// </summary>
    public class CourseInfo
    {

        /// <summary>
        /// Name of the course.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Folders present in a course, e.g Annoucement, Content.
        /// </summary>
        public List<string> folders { get; set; }

        /// <summary>
        /// Whether the folder is selected to be downloaded.
        /// </summary>
        public List<bool> foldersChecked { get; set; }

        /// <summary>
        /// The url of the folder.
        /// </summary>
        public List<string> foldersUrl { get; set; }

        /// <summary>
        /// Whether the course is selected to be downloaded.
        /// </summary>
        public bool isChecked { get; set; }

        /// <summary>
        /// Initiation.
        /// </summary>
        /// <param name="name">Name of the course.</param>
        /// <param name="folders">List of folders for the course.</param>
        /// <param name="foldersUrl">List of folder urls.</param>
        public CourseInfo(string name, List<string> folders, List<string> foldersUrl)
        {
            // Set the fields.
            int i, count = folders.Count;
            this.isChecked = false;
            this.foldersChecked = new List<bool>(count);
            this.name = name;
            this.folders = folders;
            this.foldersUrl = foldersUrl;

            // Initialize folders to be unchecked.
            for ( i = 0; i < count; i++ )
            {
                this.foldersChecked[i] = false;
            }
        }

        /// <summary>
        /// Initiation with all fields.
        /// </summary>
        /// <param name="name">Name of course.</param>
        /// <param name="folders">Folders in the course.</param>
        /// <param name="isChecked">Whether the course is selected.</param>
        /// <param name="foldersChecked">Whether a folder is checked.</param>
        /// <param name="foldersUrl">List of folder urls.</param>
        public CourseInfo(string name, List<string> folders, bool isChecked, List<bool> foldersChecked, List<string> foldersUrl)
        {
            // Set all the fields.
            this.isChecked = isChecked;
            this.foldersChecked = foldersChecked;
            this.name = name;
            this.folders = folders;
            this.foldersUrl = foldersUrl;
        }

        /// <summary>
        /// Displays all the info of the course.
        /// </summary>
        /// <returns>Info string.</returns>
        public override string ToString()
        {
            // Add all fields to the string.
            StringBuilder builder = new StringBuilder();
            builder.Append(this.name + " : " + (this.isChecked ? "True" : "False") + "\n");
            for ( int i = 0; i < this.folders.Count; i++ )
            {
                builder.Append(folders[i] + (foldersChecked[i] ? "True" : "False") + "\n");
            }
            builder.Append("\n");

            // Return the final info string.
            return builder.ToString();
        }

    }
}
