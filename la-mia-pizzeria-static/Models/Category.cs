using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome della categoria non puo essre nullo!")]
        [StringLength(50, ErrorMessage = "La lunghezza non puo essere superiore ai 50 caratteri")]
        public string Name { get; set; }

        public List<Pizza>? pizzas { get; set; }

        public Category() { }

        public Category(string name)
        {
            Name = name;
        }
        
    }
}
