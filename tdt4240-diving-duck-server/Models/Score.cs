using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DivingDuckServer.Models
{
    [PrimaryKey(nameof(Id))]
    public class Score
	{
        [Required]
        public int Id { get; set; }
        public int ScoreXPos { get; set; }
    }

    public class ScoreDTO
    {
        [Required]
        public int ScoreXPos { get; set; }
    }
}

