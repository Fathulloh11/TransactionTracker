﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace MyExpenseTracker.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required]
        [Column("username")]
        [JsonProperty("username")]
        public string Username { get; set; }

        [Required]
        [Column("password")]
        [JsonProperty("password")]
        public string Password { get; set; }

    }
}
