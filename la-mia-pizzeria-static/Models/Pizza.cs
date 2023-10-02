using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {
        [Key]
        public int Id {  get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [MaxLength(500)]
        public string Image {  get; set; }
        public double Price { get; set; }

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
