using Bogus;
using FakerTest.Domain.Entities;

namespace DataGenerationLib;

public enum DataQuality
{
  Good,
  Bad,
  Ugly
}

public class MockDataGenerator
{
  private readonly Faker<User> _userFaker;

  public MockDataGenerator()
  {
    _userFaker = new Faker<User>()
        .CustomInstantiator(f => new User(
            f.Person.FullName,
            f.Internet.Email(),
            f.Phone.PhoneNumber(),
            f.Address.StreetAddress(),
            f.Address.City(),
            f.Address.State(),
            f.Address.ZipCode()))
        .RuleFor(u => u.Id, f => Guid.NewGuid().ToString());
  }

  public List<User> GenerateUsers(int totalQuantity, int goodPercentage, int badPercentage, int uglyPercentage)
  {
    var users = new List<User>();

    int goodCount = totalQuantity * goodPercentage / 100;
    int badCount = totalQuantity * badPercentage / 100;
    int uglyCount = totalQuantity * uglyPercentage / 100;

    users.AddRange(GenerateUsersOfType(goodCount, DataQuality.Good));
    users.AddRange(GenerateUsersOfType(badCount, DataQuality.Bad));
    users.AddRange(GenerateUsersOfType(uglyCount, DataQuality.Ugly));

    return users;
  }

  private List<User> GenerateUsersOfType(int quantity, DataQuality quality)
  {
    var users = new List<User>();

    for (int i = 0; i < quantity; i++)
    {
      users.Add(GenerateUser(quality));
    }

    return users;
  }

  private User GenerateUser(DataQuality quality)
  {
    switch (quality)
    {
      case DataQuality.Good:
        return _userFaker.Generate();
      case DataQuality.Bad:
        var badUser = _userFaker.Generate();
        badUser.EmailAddress = null;
        badUser.PhoneNumber = "123";
        return badUser;
      case DataQuality.Ugly:
        var uglyUser = _userFaker.Generate();
        uglyUser.FullName = "";
        uglyUser.EmailAddress = "not_a_valid_email";
        uglyUser.PhoneNumber = "not#a#phone#number";
        uglyUser.Address = "";
        return uglyUser;
      default:
        throw new ArgumentException("Invalid data quality level.");
    }
  }
}
