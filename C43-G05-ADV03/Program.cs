using System;

public class Book
{
    public string ISBN { get; set; }
    public string Title { get; set; }
    public string[] Authors { get; set; }
    public DateTime PublicationDate { get; set; }
    public decimal Price { get; set; }

    public Book(string _ISBN, string _Title, string[] _Authors, DateTime _PublicationDate, decimal _Price)
    {
        ISBN = _ISBN;
        Title = _Title;
        Authors = _Authors;
        PublicationDate = _PublicationDate;
        Price = _Price;
    }

    public override string ToString()
    {
        return $"Title: {Title}, ISBN: {ISBN}, Authors: {string.Join(", ", Authors)}, " +
               $"Publication Date: {PublicationDate.ToShortDateString()}, Price: {Price:C}";
    }
}

public class BookFunctions
{
    public delegate T BookProperty<T>(Book book);

    public static T GetProperty<T>(Book book, BookProperty<T> propertyDelegate)
    {
        return propertyDelegate(book);
    }
}

class Program
{
    static void Main()
    {
        Book myBook = new Book("12345", "C# Programming", new string[] { "sayed mohamed ", "mohamed sayed " }, DateTime.Now, 49.99m);

        string title = BookFunctions.GetProperty(myBook, b => b.Title);
        string authors = BookFunctions.GetProperty(myBook, b => string.Join(", ", b.Authors));
        decimal price = BookFunctions.GetProperty(myBook, b => b.Price);

        Console.WriteLine($"Title: {title}");
        Console.WriteLine($"Authors: {authors}");
        Console.WriteLine($"Price: {price:C}");
    }
}
