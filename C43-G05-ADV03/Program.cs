using System;
using System.Collections.Generic;

#region Q-01
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
#endregion
#region Q-2
public class LibraryEngine
{
    public static void ProcessBooks<T>(List<Book> bList, Func<Book, T> fPtr)
    {
        foreach (Book b in bList)
        {
            Console.WriteLine(fPtr(b));
        }
    }
}
#endregion



class Program
{
    static void Main()
    {
        List<Book> books = new List<Book>
        {
            new Book("12345", "C# Programming", new string[] { "sayed mohamed ", "mohamed sayed " }, DateTime.Now, 49.99m),
            new Book("67890", "Advanced C#", new string[] { "Alice Brown", "Bob White" }, DateTime.Now.AddYears(-1), 59.99m)
        };

        //  1. (User Defined Delegate) 
        LibraryEngine.ProcessBooks(books, b => BookFunctions.GetProperty(b, x => x.Title));

        //  2.  Delegate     Func<T, TResult> 
        LibraryEngine.ProcessBooks(books, b => b.ISBN);

        //  3.  Anonymous Method 
        LibraryEngine.ProcessBooks(books, delegate (Book b) { return b.Authors.Length > 0 ? b.Authors[0] : "No Author"; });

        //  4.  Lambda Expression لتاريخ النشر 
        LibraryEngine.ProcessBooks(books, b => b.PublicationDate.ToShortDateString());

        //  5. fun  ترجع أي خاصية 
        string firstBookPrice = BookFunctions.GetProperty(books[0], b => b.Price.ToString("C"));
        Console.WriteLine($"Price of first book: {firstBookPrice}");
    }
}
