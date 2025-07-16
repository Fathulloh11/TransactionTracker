using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace MyExpenseTracker.Models
{
    [Table("transactions")]
    public class Transaction
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required]
        [Column("date")]
        [JsonProperty("date")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        [Column("amount")]
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [Required]
        [Column("type")]
        [JsonProperty("type")]
        public TransactionType Type { get; set; }

        [Required]
        [Column("category")]
        [JsonProperty("category")]
        public Category Category { get; set; }

        [Column("note")]
        [JsonProperty("note")]
        public string Note { get; set; }

        
        [Column("user_id")]
        [JsonProperty("userId")]
        public int UserId { get; set; }
        
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
