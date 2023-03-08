using System.ComponentModel.DataAnnotations;

namespace BookWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display (Name ="Display Order")]
        [Range (1,100,ErrorMessage ="please Choice in between 1 to 100 only!!!")]
        public int DisplyOrder { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
