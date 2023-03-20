using System;
using DivingDuckServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace DivingDuckServer.Models
{
	public class User
	{
        [Key]
        public int UserId { get; set; }
		public string UserName { get; set; }
		public List<Score> Scores { get; } = new();
	}

}

