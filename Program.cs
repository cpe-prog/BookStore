using System.Text.Json;
using BookStore.CommandBase;
using Spectre.Console;
using static Spectre.Console.AnsiConsole;
using Console = System.Console;

namespace BookStore;

internal abstract class Program
{
    public Dictionary<string, object> Books = new()
    {
        { "add", new AddCommand() },
        { "edit", new EditCommand() },
        { "delete", new DeleteCommand() },
        { "list", new ListCommand()},
    };
    
    private static void Main(string[] args)
    {
        var db = JsonSerializer.Serialize("Library.json");
        
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

    
    private static void AddBooks()
    {
        var books = new List<Books>();

        while (true)
        {
            var book = new Books();

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
        table.AddColumns(new TableColumn("[navy]#[/]"), new TableColumn("[yellow]ID[/]"), new TableColumn("[red]Title[/]"), new TableColumn("[aqua]Author[/]"));
        
        for (var i = 0; i < books.Count; i++)
        {
            table.AddRow((i + 1).ToString(), books[i].Id.ToString(), books[i].Title!, books[i].Author!);
        }
        Write(table);
    }

   
}