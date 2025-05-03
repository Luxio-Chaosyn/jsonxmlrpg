using System.Text.Json;
using System.Xml.Linq;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string City { get; set; }
}

class Program
{
    static List<Person> people = new();

    static void Main()
    {
        Console.WriteLine("Enter path to JSON file or paste JSON content:");
        string input = Console.ReadLine();

        if (File.Exists(input))
        {
            people = JsonSerializer.Deserialize<List<Person>>(File.ReadAllText(input));
        }
        else
        {
            people = JsonSerializer.Deserialize<List<Person>>(input);
        }

        while (true)
        {
            Console.WriteLine("\nChoose an action:");
            Console.WriteLine("1. Search by name");
            Console.WriteLine("2. Sort by age");
            Console.WriteLine("3. Group by city");
            Console.WriteLine("4. Export to XML");
            Console.WriteLine("5. Exit");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    SearchByName();
                    break;
                case "2":
                    SortByAge();
                    break;
                case "3":
                    GroupByCity();
                    break;
                case "4":
                    ExportToXml();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    static void SearchByName()
    {
        Console.WriteLine("Enter name to search:");
        string name = Console.ReadLine();
        var results = people.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        PrintResults(results);
    }

    static void SortByAge()
    {
        var sorted = people.OrderBy(p => p.Age);
        PrintResults(sorted);
    }

    static void GroupByCity()
    {
        var groups = people.GroupBy(p => p.City);
        foreach (var group in groups)
        {
            Console.WriteLine($"\nCity: {group.Key}");
            PrintResults(group);
        }
    }

    static void ExportToXml()
    {
        XElement xml = new("People",
            from p in people
            select new XElement("Person",
                new XElement("Name", p.Name),
                new XElement("Age", p.Age),
                new XElement("City", p.City)));

        string path = "people_export.xml";
        xml.Save(path);
        Console.WriteLine($"Data exported to {path}");
    }

    static void PrintResults(IEnumerable<Person> results)
    {
        foreach (var p in results)
        {
            Console.WriteLine($"Name: {p.Name}, Age: {p.Age}, City: {p.City}");
        }
    }
}
