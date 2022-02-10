namespace WebShopAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }

        public Product(int id, string name, int price, int quant, string category)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quant;
            Category = category;
        }
    }
}
