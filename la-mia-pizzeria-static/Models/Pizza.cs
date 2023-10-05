using la_mia_pizzeria_static.Validation;
using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {
        [Key]
        public int Id {  get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Il nome della pizza è obbligatorio")]
        [StringLength(50 , ErrorMessage = "Nome della pizza troppo lungo, massimo 50 caratterri")]
        public string Name { get; set; }

        [MaxLength(500)]
        [Required(ErrorMessage = "La descrizione della pizza è obbligatoria")]
        [StringLength(500, ErrorMessage = "Descrizione della pizza troppo lungo, massimo 500 caratterri")]
        [DescriptionValidation]
        public string Description { get; set; }

        [MaxLength(500)]
        [Required(ErrorMessage = "L'immagine di copertina è obbligatoria")]
        [StringLength(500, ErrorMessage = "Path dell'immagine della pizza troppo lungo, massimo 500 caratterri")]
        public string Image {  get; set; }

        [Required(ErrorMessage = "Il prezzo della pizza è obbligatorio")]
        [Range(1, 100, ErrorMessage = "Il prezzo della pizza deve essere compreso tra 1 e 100")]
        public double Price { get; set; }

        // Collegamento della tabella categoria in relazione 1 a N
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        // Collegamento della tabella Ingredienti in relazione N a N
        public List<Ingredient>? Ingredients { get; set; }

        public Pizza() { }
        public Pizza(string name, string description, double price,string image)
        {
            this.Name = name;
            this.Description = description;
            this.Image = image;
            this.Price = price;
        }

    }
}
