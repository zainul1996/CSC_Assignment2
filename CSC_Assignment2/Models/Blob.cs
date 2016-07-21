using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Bio { get; set; }
        public string Reknown { get; set; }
        public string TalentName { get; set; }
    }
}