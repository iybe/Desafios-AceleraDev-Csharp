using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codenation.Challenge.Models
{
    [Table("submission")]
    public class Submission
    {
        [Required]
        [Column("user_id")]
        public int User_Id { get; set; }
        [ForeignKey("user_id")]
        public User User { get; set; }

        [Required]
        [Column("challenge_id")]
        public int Challenge_Id { get; set; }
        [ForeignKey("challenge_id")]
        public Challenge Challenge { get; set; }

        [Required]
        [Column("score")]
        public decimal Score { get; set; }

        [Required]
        [Column("created_at")]
        public DateTime Created_At { get; set; }
    }
}
