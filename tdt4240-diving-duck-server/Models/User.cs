using System;
using DivingDuckServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace DivingDuckServer.Models
{
    [PrimaryKey(nameof(Id))]
    public class User
	{
        [Required]
        public int Id { get; set; }
		public string? UserName { get; set; }
		public List<Score> Scores { get; } = new();
	}

    public class UserDTO
    {
        public string? UserName { get; set; }
        public List<Score> Scores { get; } = new();
    }

}

