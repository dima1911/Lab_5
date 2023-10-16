using System;
using System.Collections.Generic;

// Клас "Товар"
public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public int Rating
    {
        get; set;
    }

    public Product(string name, decimal price, string description, string category, int rating)
    {
        Name = name;
        Price = price;
        Description = description;
        Category = category;
        Rating = rating;
    }
}

// Клас "Користувач"
public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public List<Order> PurchaseHistory { get; set; }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
        PurchaseHistory = new List<Order>();
    }
}

// Клас "Замовлення"
public class Order
{
    public List<Product> Products { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; }

    public Order(List<Product> products, int quantity)
    {
        Products = products;
        Quantity = quantity;
        TotalPrice = CalculateTotalPrice();
        Status = "Pending";
    }

    private decimal CalculateTotalPrice()
    {
        decimal totalPrice = 0;
        foreach (var product in Products)
        {
            totalPrice += product.Price;
        }
        return totalPrice * Quantity;
    }
}

// Інтерфейс для пошуку товарів
public interface ISearchable
{
    List<Product> SearchByPrice(decimal minPrice, decimal maxPrice);
    List<Product> SearchByCategory(string category);
    List<Product> SearchByRating(int minRating);
}

// Клас "Магазин"
public class Store : ISearchable
{
    private List<User> users = new List<User>();
    private List<Product> products = new List<Product>();
    private List<Order> orders = new List<Order>();

    public void AddUser(User user)
    {
        users.Add(user);
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public void PlaceOrder(User user, List<Product> selectedProducts, int quantity)
    {
        var order = new Order(selectedProducts, quantity);
        user.PurchaseHistory.Add(order);
        orders.Add(order);
    }

    public List<Product> SearchByPrice(decimal minPrice, decimal maxPrice)
    {
        return products.FindAll(product => product.Price >= minPrice && product.Price <= maxPrice);
    }

    public List<Product> SearchByCategory(string category)
    {
        return products.FindAll(product => product.Category == category);
    }

    public List<Product> SearchByRating(int minRating)
    {
        return products.FindAll(product => product.Rating >= minRating);
    }

    // other methods

    public void DisplayUsers()
    {
        foreach (var user in users)
        {
            Console.WriteLine($"Username: {user.Username}");
        }
    }

    public void DisplayProducts()
    {
        foreach (var product in products)
        {
            Console.WriteLine($"Name: {product.Name}, Price: {product.Price}");
        }
    }

    public void DisplayOrders()
    {
        foreach (var order in orders)
        {
            Console.WriteLine($"Total Price: {order.TotalPrice}, Status: {order.Status}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Store store = new Store();

        User user1 = new User("user1", "password1");
        User user2 = new User("user2", "password2");

        Product product1 = new Product("Product 1", 10.0m, "Description 1", "Category A", 4);
        Product product2 = new Product("Product 2", 15.0m, "Description 2", "Category B", 5);

        store.AddUser(user1);
        store.AddUser(user2);

        store.AddProduct(product1);
        store.AddProduct(product2);

        List<Product> selectedProducts = new List<Product> { product1, product2 };
        store.PlaceOrder(user1, selectedProducts, 2);

        store.DisplayUsers();
        store.DisplayProducts();
        store.DisplayOrders();
    }
}
