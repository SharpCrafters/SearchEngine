using System.ComponentModel.DataAnnotations;

namespace SearchEngine
{
    public static class PreparedList
    {
        public static List<ScannerDataGrid> List { get; set; }
    }

    public class Creator
    {
        [Key]
        public int id { get; set; }

        public string? Name { get; set; }

        public Creator() { }

        public Creator(int id, string? Name)
        {
            this.id = id;
            this.Name = Name;
        }
    }

    public class Technology
    {
        [Key]
        public int id { get; set; }

        public string? Name { get; set; }

        public Technology() { }

        public Technology(int id, string? Name)
        {
            this.id = id;
            this.Name = Name;
        }
    }

    public class Scanner
    {
        [Key]
        public int id { get; set; }

        public string? Name { get; set; }

        public int Creator { get; set; }

        public int Technology { get; set; }

        public int Accuracy { get; set; }

        public int Speed { get; set; }

        public string? ColorCapture { get; set; }

        public double Price { get; set; }

        public int Release { get; set; }

        public string? Description { get; set; }

        public Scanner() { }

        public Scanner(int id, string? Name, int Creator, int Technology, int Accuracy, 
                       int Speed, string? ColorCapture, double Price, int Release, string? Description)
        {
            this.id = id;
            this.Name = Name;
            this.Creator = Creator;
            this.Technology = Technology;
            this.Accuracy = Accuracy;
            this.Speed = Speed;
            this.ColorCapture = ColorCapture;
            this.Price = Price;
            this.Release = Release;
            this.Description = Description;
        }
    }

    public class ScannerDataGrid
    {
        [Key]
        public int id { get; set; }

        public string? Name { get; set; }

        public string? Creator { get; set; }

        public string? Technology { get; set; }

        public int Accuracy { get; set; }

        public int Speed { get; set; }

        public string? ColorCapture { get; set; }

        public double Price { get; set; }

        public int Release { get; set; }

        public string? Description { get; set; }

        public ScannerDataGrid() { }

        public ScannerDataGrid(int id, string? Name, string? Creator, string? Technology, int Accuracy, 
                               int Speed, string? ColorCapture, double Price, int Release, string? Description)
        {
            this.id = id;
            Name = Name;
            Creator = Creator;
            Technology = Technology;
            Accuracy = Accuracy;
            Speed = Speed;
            ColorCapture = ColorCapture;
            Price = Price;
            Release = Release;
            Description = Description;
        }
    }

    public class User
    {

        [Key]
        public int id { get; set; }

        public string? Name { get; set; }

        public string? PasswordHash { get; set; }

        public User() { }

        public User(int id, string? Name, string? PasswordHash)
        {
            this.id = id;
            this.Name = Name;
            this.PasswordHash = PasswordHash;
        }

        // Метод для установки пароля
        public void SetPassword(string password)
        {
            PasswordHash = PasswordHasher.HashPassword(password);
        }

        // Метод для проверки пароля
        public bool VerifyPassword(string password, string PasswordHash)
        {
            return PasswordHasher.VerifyPassword(password, PasswordHash);
        }

        ~User() { }
    }
}
