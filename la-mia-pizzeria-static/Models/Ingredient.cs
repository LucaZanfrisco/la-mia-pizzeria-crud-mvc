using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nom dell'ingrediente è obbligatorio"), StringLength(50, ErrorMessage ="Il nome dell'ingrediente non puo essere superiore a 50 caratteri")]
        public string Name { get; set; }

        public List<Pizza> Pizzas { get; set; }

        public Ingredient() { }

    }
}
