using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codenation.Challenge.Models
{
    [Table("candidate")]
    public class Candidate
    {
        [Required]
        [Column("user_id")]
        public int User_Id { get; set; }
        [ForeignKey("user_id")]
        public User User { get; set; }

        [Required]
        [Column("acceleration_id")]
        public int Acceleration_Id { get; set; }
        [ForeignKey("acceleration_id")]
        public Acceleration Acceleration { get; set; }

        [Required]
        [Column("company_id")]
        public int Company_Id { get; set; }
        [ForeignKey("company_id")]
        public Company Company { get; set; }

        [Required]
        [Column("status")]
        public int Status { get; set; }

        [Required]
        [Column("created_at")]
        public DateTime Created_At { get; set; }
    }
}
