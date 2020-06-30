using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codenation.Challenge.Models
{
    [Table("company")]
    public class Company
    {
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }
        
        [Required]
        [Column("name")]
        [MaxLength(100)]
        public string Name { get; set; }
        
        [Required]
        [Column("slug")]
        [MaxLength(50)]
        public string Slug { get; set; }
        
        [Required]
        [Column("created_at")]
        public DateTime Created_At { get; set; }

        public List<Candidate> Candidates { get; set; }
    }
}