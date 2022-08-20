using System;
using System.ComponentModel.DataAnnotations;

namespace KuittiApp.Models
{
    public class Kuitti
    {
        [Key]
        public string Id { get; set; }
        public DateTime PVM { get; set; }
        public string Kuvaus { get; set; }
        public byte[] Kuva { get; set; }
        public string ContentType { get; set; }
        public string Kayttaja { get; set; }
    }
}

