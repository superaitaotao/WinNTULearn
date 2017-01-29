using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinNTULearn
{
    /// <summary>
    /// Contains info of a file.
    /// </summary>
    class FileInfo
    {
        /// <summary>
        /// Name of a file.
        /// </summary>
        public string fileName { get; set; }

        /// <summary>
        /// Course that the file belongs to.
        /// </summary>
        public string courseName { get; set; }

        /// <summary>
        /// Synchronization date.
        /// </summary>
        public DateTime syncDate { get; set; }

        /// <summary>
        /// Download url of the file.
        /// </summary>
        public string fileUrl { get; set; }

        /// <summary>
        /// Initiation.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="courseName">Name of the course.</param>
        /// <param name="syncDate">Sync Date.</param>
        /// <param name="fileUrl">Url of the file.</param>
        public FileInfo(string fileName, string courseName, DateTime syncDate, string fileUrl)
        {
            // Initiation.
            this.fileName = fileName;
            this.courseName = courseName;
            this.syncDate = syncDate;
            this.fileUrl = fileUrl;
        }
    }
}
