namespace FakerTest.Domain.Entities
{
  public class User : BaseEntity
  {
    public User(
      string fullName,
      string? emailAddress,
      string? phoneNumber,
      string? address,
      string? city,
      string? state,
      string? zipCode
    )
    {
      FullName = fullName;
      EmailAddress = emailAddress;
      PhoneNumber = phoneNumber;
      Address = address;
      City = city;
      State = state;
      ZipCode = zipCode;
    }
    
    public string FullName { get; set; }
    public string? EmailAddress { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
  }
}
