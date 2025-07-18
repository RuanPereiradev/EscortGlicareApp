namespace GlicareApp.Domain.Entities;

public class User
{ 
   public string Id { get; set; }
   public string Name { get; private set; }
   public string Email { get; private set; }
   public string PasswordHash { get; private set; }
   public string BirthDate { get; private set; }
   public string TermsAccepted  { get; private set; }
   public string RegisterType {get; private set;}
   public string LoginType {get; private set;}

   public User(string name, string email, string passwordHash, string birthDate, string termsAccepted,
      string registerType, string loginType)
   {
      Name = name;
      Email = email;
      PasswordHash = passwordHash;
      BirthDate = birthDate;
      TermsAccepted = termsAccepted;
      RegisterType = registerType;
      LoginType = loginType;
   }
}