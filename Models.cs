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

        public string? name { get; set; }

        public Creator() { }

        public Creator(int id, string? name)
        {
            this.id = id;
            this.name = name;
        }
    }

    public class Technology
    {
        [Key]
        public int id { get; set; }

        public string? name { get; set; }

        public Technology() { }

        public Technology(int id, string? name)
        {
            this.id = id;
            this.name = name;
        }
    }

    public class Scanner
    {
        [Key]
        public int id { get; set; }

        public string? name { get; set; }

        public int creator { get; set; }

        public int technology { get; set; }

        public int accuracy { get; set; }

        public int speed { get; set; }

        public string? colorcapture { get; set; }

        public int price { get; set; }

        public int release { get; set; }

        public string? description { get; set; }

        public Scanner() { }

        public Scanner(int id, string? name, int creator, int technology, int accuracy, int speed, string? colorcapture, int price, int release, string? description)
        {
            this.id = id;
            this.name = name;
            this.creator = creator;
            this.technology = technology;
            this.accuracy = accuracy;
            this.speed = speed;
            this.colorcapture = colorcapture;
            this.price = price;
            this.release = release;
            this.description = description;
        }

        ~Scanner() { }
    }

    public class ScannerDataGrid
    {
        [Key]
        public int id { get; set; }

        public string? name { get; set; }

        public string? creator { get; set; }

        public string? technology { get; set; }

        public int accuracy { get; set; }

        public int speed { get; set; }

        public string? colorcapture { get; set; }

        public int price { get; set; }

        public int release { get; set; }

        public string? description { get; set; }

        public ScannerDataGrid() { }

        public ScannerDataGrid(int id, string? name, string? creator, string? technology, int accuracy, int speed, string? colorcapture, int price, int release, string? description)
        {
            this.id = id;
            this.name = name;
            this.creator = creator;
            this.technology = technology;
            this.accuracy = accuracy;
            this.speed = speed;
            this.colorcapture = colorcapture;
            this.price = price;
            this.release = release;
            this.description = description;
        }

        ~ScannerDataGrid() { }
    }

    public class User
    {
        [Key]
        public int id { get; set; }

        public string? name { get; set; }

        public string? password { get; set; }

        public User() { }

        public User(int id, string? name, string? password)
        {
            this.id = id;
            this.name = name;
            this.password = password;
        }

        ~User() { }
    }
}
