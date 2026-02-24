using System;
using System.Collections.Generic;
using IT410Project.Lib.EfCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace IT410Project.Lib.EfCore.Context;

public partial class ProjectDbContext : DbContext
{
	public ProjectDbContext()
	{
	}

	public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
		: base(options)
	{
	}

	public virtual DbSet<Project> Projects { get; set; }

	public virtual DbSet<Sleep> Sleeps { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
		=> optionsBuilder.UseSqlite("Data Source=Data/Database.db");

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Project>(entity =>
		{
			entity.Property(e => e.Id).ValueGeneratedNever();
		});

		modelBuilder.Entity<Sleep>(entity =>
		{
			entity.Property(e => e.Id).ValueGeneratedNever();
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
