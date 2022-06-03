using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication6.Models
{
    public class Ratings
    {
        public int Id { get; set; }

        public string Author { get; set; }

        [Range(1, 5)]
        public int Rate { get; set; }

        public string FeedBack { get; set; }

        public string? Time { get; set; }
        public string? Date { get; set; }
    }
}