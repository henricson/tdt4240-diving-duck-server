using System;
using System.ComponentModel.DataAnnotations;

namespace DivingDuckServer.Models
{
	public class Score
	{
        [Key]
        public int ScoreId { get; set; }
        public int ScoreXPos { get; set; }
    }
}

