using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinNTULearn
{
    class FileInfo
    {
        public string fileName { get; set; }

        public string courseName { get; set; }

        public DateTime syncDate { get; set; }

        public string fileUrl { get; set; }

        public FileInfo(string fileName, string courseName, DateTime syncDate, string fileUrl)
        {

            this.fileName = fileName;

            this.courseName = courseName;

            this.syncDate = syncDate;

            this.fileUrl = fileUrl;

        }
    }
}
