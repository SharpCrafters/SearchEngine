using System.ComponentModel.DataAnnotations;

namespace SearchEngine
{
    public class Scanner
    {
        [Key]
        public int id {  get; set; }

        public string? Name { get; set; }

        public string? Creator { get; set; }

        public string? Technology { get; set; }

        public int Accuracy { get; set; }

        public int Speed { get; set; }

        public string? Formats { get; set; }

        public int Price { get; set; }

        public int ReleaseYear { get; set; }

        public string? Descriprion { get; set; }

        public Scanner() { }

        public Scanner(int id, string? name, string? creator, string? technology, int accuracy, int speed, string? formats, int price, int releaseYear, string? descriprion)
        {
            this.id = id;
            Name = name;
            Creator = creator;
            Technology = technology;
            Accuracy = accuracy;
            Speed = speed;
            Formats = formats;
            Price = price;
            ReleaseYear = releaseYear;
            Descriprion = descriprion;
        }

        ~Scanner() { }
    }
}
