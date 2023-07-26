using DataGenerationLib;
using System.Diagnostics;

var mockDataGenerator = new MockDataGenerator();

int totalQuantity = 100;
int goodPercentage = 70;
int badPercentage = 20;
int uglyPercentage = 10;

var users = mockDataGenerator.GenerateUsers(totalQuantity, goodPercentage, badPercentage, uglyPercentage);

const string format = "{0,-36} | {1,-30} | {2,-35} | {3,-25} | {4,-30} | {5,-25} | {6,-20} | {7,-20}";

string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.txt");

using (StreamWriter file = new(filePath))
{
  file.WriteLine(format, "ID", "Name", "Email", "Phone", "Address", "City", "State", "Zip Code");
  file.WriteLine(new string('-', 233));

  foreach (var user in users)
  {
    file.WriteLine(format,
        user.Id,
        user.FullName,
        user.EmailAddress ?? "NULL",
        user.PhoneNumber ?? "NULL",
        user.Address ?? "NULL",
        user.City ?? "NULL",
        user.State ?? "NULL",
        user.ZipCode ?? "NULL");
  }
}

Console.WriteLine($"Data successfully written to file at {filePath}");
Console.WriteLine("Do you want to open the file? (yes/no)");

string? response = Console.ReadLine()?.ToLower();

if (response == "yes")
{
  Process.Start(new ProcessStartInfo
  {
    FileName = "notepad.exe",
    Arguments = filePath,
    UseShellExecute = false
  });
}
