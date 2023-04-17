using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DivingDuckServer.Models
{
    [PrimaryKey(nameof(Id))]
    public class Score
    {
        [Required]
        public int Id { get; set; }
        public float TimeElapsed { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
    }

    public class ScoreDTO
    {
        [Required]
        public float TimeElapsed { get; set; }
        public int UserId { get; set; }
    }
}

