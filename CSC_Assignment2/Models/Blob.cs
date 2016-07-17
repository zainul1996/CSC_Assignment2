using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CSC_Assignment2.Models
{
    [Table("ImageTable")]
    public class Blob
    {
        [Key]
        public int BlobId { get; set; }
        public string UserId { get; set; }
        public string Url { get; set; }
        public string FileName { get; set; }
    }
}