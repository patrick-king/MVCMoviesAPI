using System;
using System.Collections.Generic;

namespace MVCMoviesAPI.Models
{
    public partial class Seller
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public string? Address1 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        public string? Phone { get; set; }
    }
}
