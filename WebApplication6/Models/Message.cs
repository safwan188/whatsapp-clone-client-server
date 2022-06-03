namespace real.Models
{
    public class Message
    {
        public int Id { get; set;}
        public int contactid { get; set;}
        public string? content { get; set; }
        public string? sender { get; set; }
        public int? time { get; set; }
    }
}
