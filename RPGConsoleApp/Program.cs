using System.Text.Json;
using System.Xml.Linq;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string City { get; set; }
    public int Strength { get; set; }
    public int Agility { get; set; }
    public int Intelligence { get; set; }
}

class Program
{
    static List<Person> people = new();
    static Random random = new();

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
            Console.WriteLine("5. Play guessing game");
            Console.WriteLine("6. Exit");
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
                    PlayGuessingGame();
                    break;
                case "6":
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
                new XElement("City", p.City),
                new XElement("Strength", p.Strength),
                new XElement("Agility", p.Agility),
                new XElement("Intelligence", p.Intelligence)));

        string path = "people_export.xml";
        xml.Save(path);
        Console.WriteLine($"Data exported to {path}");
    }

    static void PlayGuessingGame()
    {
        Console.WriteLine("Choose your character by name:");
        PrintResults(people);
        string playerName = Console.ReadLine();
        var player = people.FirstOrDefault(p => p.Name.Equals(playerName, StringComparison.OrdinalIgnoreCase));

        if (player == null)
        {
            Console.WriteLine("Character not found.");
            return;
        }

        int hp = player.Strength;
        int agility = player.Agility;
        int intelligence = player.Intelligence;

        var target = people[random.Next(people.Count)];
        while (target.Name == player.Name)
        {
            target = people[random.Next(people.Count)];
        }

        Console.WriteLine("Guess who the mystery person is! You have {0} hints.", intelligence);

        List<string> hints = new List<string>
        {
            $"Age: {target.Age}",
            $"City: {target.City}",
            $"Strength is around {target.Strength + random.Next(-2, 3)}",
            $"Agility is around {target.Agility + random.Next(-2, 3)}",
            $"Intelligence is around {target.Intelligence + random.Next(-2, 3)}"
        };

        hints = hints.OrderBy(_ => random.Next()).Take(intelligence).ToList();

        foreach (var hint in hints)
        {
            Console.WriteLine("Hint: " + hint);
        }

        while (hp > 0)
        {
            Console.WriteLine("Who do you think it is?");
            string guess = Console.ReadLine();

            if (guess.Equals(target.Name, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Correct! You win.");
                return;
            }
            else
            {
                int roll = random.Next(1, 21);
                Console.WriteLine($"Wrong! Dice roll: {roll} (needs <= {10+agility-target.Agility} to dodge)");
                if (roll > 10+agility-target.Agility)
                {
                    hp--;
                    Console.WriteLine($"You got hit! HP left: {hp}");
                }
                else
                {
                    Console.WriteLine("You dodged the attack!");
                }
            }
        }

        Console.WriteLine($"You were defeated! The mystery person was {target.Name}.");
    }

    static void PrintResults(IEnumerable<Person> results)
    {
        foreach (var p in results)
        {
            Console.WriteLine($"Name: {p.Name}, Age: {p.Age}, City: {p.City}, Strength: {p.Strength}, Agility: {p.Agility}, Intelligence: {p.Intelligence}");
        }
    }
}
