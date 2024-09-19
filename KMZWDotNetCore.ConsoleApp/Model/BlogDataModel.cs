using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMZWDotNetCore.ConsoleApp.Model
{
    internal class BlogDataModel
    {
        public int BlogId { get; set; }
        public string BlogAuthor { get; set; }
        public string BlogTitle { get; set; }

        public string BlogContent { get; set; }
        public int DeleteFlag { get; set; }

    }
}
