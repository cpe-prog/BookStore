using Spectre.Console;
using static Spectre.Console.AnsiConsole;
using Console = System.Console;

namespace BookStore;

internal abstract class Program
{
    [Obsolete("Obsolete")]
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            return;
        }

        switch (args[0])
        {
            case "add":
                if (args.Length < 2 || args[1] == "books")
                {
                    AddBooks();
                }
                break;
            default:
                Console.WriteLine("Invalid command.");
                break;
        }
    }

    
    [Obsolete("Obsolete")]
    private static void AddBooks()
    {
        var books = new List<Book>();

        while (true)
        {
            var book = new Book();

            Console.Write("Title: ");
            book.Title = Console.ReadLine();

            Console.Write("Author: ");
            book.Author = Console.ReadLine();

            Console.Write("ID: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("Invalid ID. Please enter a number.");
                continue;
            }
            book.Id = id;

            books.Add(book);

            Console.Write("Add another book? (y/n): ");
            if (Console.ReadLine()!.ToLower() != "y")
            {
                break;
            }
        }
        
        var table = new Table();
        table.AddColumn("[orange]#[/]");
        table.AddColumn("[yellow]ID[/]");
        table.AddColumn("[red]Title[/]");
        table.AddColumn("[green]Author[/]");


        for (var i = 0; i < books.Count; i++)
        {
            table.AddRow((i + 1).ToString(), books[i].Id.ToString(), books[i].Title!, books[i].Author!);
        }
        Render(table);
    }
}