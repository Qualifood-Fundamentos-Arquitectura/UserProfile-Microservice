using DittoBox.API.UserProfile.Domain.Models.Entities;
using DittoBox.API.UserProfile.Domain.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace DittoBox.API.Shared.Infrastructure
{
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Profile> Profiles { get; set; }

	}

}