// See https://aka.ms/new-console-template for more information
using QuickStart;

LoadFromDb();





async static Task LoadFromDb()
{
    Console.WriteLine("Fetching from DB...");

    var result = await new LocalMethods().TestWithSqlDb("_");
    Console.WriteLine($"Result of query is: {result}");
    Console.WriteLine("Press any key to close...");
    Console.ReadKey();
}
