namespace real.Models
{
    public class Contact
    {
        public int Id { get; set; } 
        public int userid { get; set; }
        public string Name { get; set; }
        public List<Message>? Messages { get; set; }

    }
}
