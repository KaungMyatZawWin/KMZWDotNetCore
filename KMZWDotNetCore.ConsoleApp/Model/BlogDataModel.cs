using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMZWDotNetCore.ConsoleApp.Model
{
    public class BlogDataModel
    {
        public int BlogId { get; set; }
        public string BlogAuthor { get; set; }
        public string BlogTitle { get; set; }

        public string BlogContent { get; set; }
        public int DeleteFlag { get; set; }

    }

    [Table("Tbl_Blog")]
    public class EFCoreDataModel
    {
        [Key]
        [Column("BlogId")]
        public int BlogId { get; set; }

        [Column("BlogAuthor")]
        public string BlogAuthor { get; set; }

        [Column("BlogTitle")]
        public string BlogTitle { get; set; }

        [Column("BlogContent")]
        public string BlogContent { get; set; }

        [Column("DeleteFlag")]
        public int DeleteFlag { get; set; }
    }
}
