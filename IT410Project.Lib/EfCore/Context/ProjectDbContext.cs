using System.Runtime.InteropServices;
using System.Text;
using Microsoft.EntityFrameworkCore;
using EF =  IT410Project.Lib.EfCore.Entities;
using MD = IT410Project.Models;

namespace IT410Project.Lib.EfCore.Context;

public partial class ProjectDbContext : DbContext
{
	public string? ConnectionString { get; set; }

	public ProjectDbContext()
	{
	}

	public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
		: base(options)
	{
	}

	public virtual DbSet<EF.Project> Projects { get; set; }

	public virtual DbSet<EF.Sleep> Sleeps { get; set; }
	public virtual DbSet<MD.LongProject> LongProjects { get; set; }
	public virtual DbSet<MD.LongProjectDay> LongProjectDays { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
		=> optionsBuilder.UseSqlite(ConnectionString ?? $"Data Source={Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName}/Data/EFDatabase.db");
		// => optionsBuilder.UseSqlite($"Data Source=Data/EFDatabase.db");

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Console.WriteLine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName);

		modelBuilder.Entity<EF.Project>(entity =>
		{
			entity.Property(e => e.Id).ValueGeneratedNever();
		});

		modelBuilder.Entity<EF.Sleep>(entity =>
		{
			entity.Property(e => e.Id).ValueGeneratedNever();
		});

		modelBuilder.Entity<MD.LongProject>()
			.HasMany(lp => lp.ProjectDays)
			.WithOne(pd => pd.LongProject)
			.HasForeignKey(lp => lp.LongProjectId);

		LoadSeedData(modelBuilder);

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

	public void LoadSeedData(ModelBuilder modelBuilder)
	{
		List<EF.Sleep> sleepDataList = [];
		List<EF.Project> projectDataList = [];
		List<MD.LongProjectDay> longProjectDayList = [];
		List<MD.LongProject> longProjectList = [];
		DateTime usedDate = DateTime.Parse("2026-03-01");

		for(int idx = 0; idx < 2048; idx++)
		{
			sleepDataList.Add(new EF.Sleep {
				Id = idx + 1,
				Name = "Placeholder Name...",
				StartTime = usedDate.AddDays(idx).ToString(),
				EndTime = usedDate.AddDays(idx).AddHours(8).ToString(),
				Quality = idx % 10,
				RepeatDays = (idx * 100) % 0b0010000
			});
			projectDataList.Add(new EF.Project
			{
				Id = idx + 1,
				Name = "Placeholder Name...",
				StartTime = usedDate.AddDays(idx).ToString(),
				EndTime = usedDate.AddDays(idx).AddHours(8).ToString(),
				Desc = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec malesuada arcu lacus, quis hendrerit nisi condimentum quis. Suspendisse pretium luctus metus eu feugiat. Mauris odio est, mattis id lectus vitae, vestibulum pulvinar augue. In non finibus sapien. Quisque facilisis augue sed libero fermentum consequat. Cras eros velit, tincidunt vitae magna eget, interdum aliquam tortor. Praesent vestibulum sem nec facilisis condimentum. Duis volutpat metus quis arcu ultricies, ornare ultricies elit gravida. Vivamus semper fringilla neque, eu mollis orci rutrum a. Aliquam erat volutpat.
Nam varius semper dui eget vehicula. Sed in neque ut nisi scelerisque vehicula sit amet non enim. Nullam bibendum ante est. In hac habitasse platea dictumst. Quisque ultricies lacinia urna at rhoncus. In facilisis libero a ligula pretium, quis mattis enim pretium. In id ipsum quis enim gravida efficitur. Vivamus sed ligula purus.

Cras dignissim a nulla et rutrum. Fusce consequat sit amet nunc et pretium. Mauris urna diam, commodo ut tincidunt vitae, condimentum sit amet est. Phasellus vestibulum, dui at tempor condimentum, augue erat sodales quam, sit amet dictum quam nisi vitae tortor. Mauris et egestas felis. Maecenas suscipit tortor massa, id dapibus tortor tempor nec. Mauris faucibus iaculis eros in pellentesque. Fusce malesuada molestie ipsum sit amet posuere. Aenean posuere, elit vel sollicitudin ornare, nibh eros sodales urna, quis ornare mi nibh quis eros. Maecenas et pharetra ipsum. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Cras enim tellus, aliquam sit amet turpis in, faucibus ultricies ligula.

Sed sollicitudin non lacus eget lobortis. Duis ut feugiat velit. Morbi varius est eu dui laoreet convallis. Donec lectus magna, mattis at nisi venenatis, condimentum commodo massa. Phasellus ut ipsum cursus, elementum lacus nec, pellentesque urna. Vivamus erat odio, dignissim non purus ut, facilisis vulputate sapien. Nulla aliquet magna arcu, ut fermentum sapien auctor eget. Aliquam ornare mi aliquam, dictum libero sollicitudin, varius odio. Nulla vitae quam at magna sagittis imperdiet. Aliquam vitae porttitor leo. Sed lacinia sed dolor et varius. Phasellus eleifend mattis nisi, eu gravida libero mattis vitae. Nunc blandit turpis tempor felis facilisis, ac mattis dui tincidunt. Maecenas finibus condimentum sagittis. Duis molestie arcu eget tortor dapibus, at accumsan purus consectetur. Suspendisse eu ex et lacus tincidunt efficitur sed ac diam.

Vivamus eget tincidunt nunc. Ut sagittis nec mi a faucibus. In sagittis risus sit amet enim faucibus placerat. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Ut posuere rhoncus odio, eu vehicula velit mattis quis. Etiam hendrerit mauris odio, vitae elementum ipsum scelerisque quis. Donec convallis erat eget risus luctus, et mollis eros aliquam. Quisque venenatis bibendum velit vel facilisis. Maecenas non tincidunt quam.",
				IsRepeating = (idx % 2) == 0 ? false : true
				});

			MD.LongProjectDay Weekday1 = new MD.LongProjectDay
			{
				Id = (idx * 7) + 1,
				StartTime = usedDate.AddDays((idx * 7) + 1).AddHours(8),
				EndTime = usedDate.AddDays((idx * 7) + 1).AddHours(14),
				Notes = "Cool Notes (:",
				LongProjectId = idx + 1
			};

			MD.LongProjectDay Weekday2 = new MD.LongProjectDay
			{
				Id = (idx * 7) + 2,
				StartTime = usedDate.AddDays((idx * 7) + 2).AddHours(8),
				EndTime = usedDate.AddDays((idx * 7) + 2).AddHours(14),
				Notes = "Cool Notes (:",
				LongProjectId = idx + 1
			};

			MD.LongProjectDay Weekday3 = new MD.LongProjectDay
			{
				Id = (idx * 7) + 3,
				StartTime = usedDate.AddDays((idx * 7) + 3).AddHours(8),
				EndTime = usedDate.AddDays((idx * 7) + 3).AddHours(14),
				Notes = "Cool Notes (:",
				LongProjectId = idx + 1
			};

			MD.LongProjectDay Weekday4 = new MD.LongProjectDay
			{
				Id = (idx * 7) + 4,
				StartTime = usedDate.AddDays((idx * 7) + 4).AddHours(8),
				EndTime = usedDate.AddDays((idx * 7) + 4).AddHours(14),
				Notes = "Cool Notes (:",
				LongProjectId = idx + 1
			};

			MD.LongProjectDay Weekday5 = new MD.LongProjectDay
			{
				Id = (idx * 7) + 5,
				StartTime = usedDate.AddDays((idx * 7) + 5).AddHours(8),
				EndTime = usedDate.AddDays((idx * 7) + 5).AddHours(14),
				Notes = "Cool Notes (:",
				LongProjectId = idx + 1
			};

			MD.LongProjectDay Weekday6 = new MD.LongProjectDay
			{
				Id = (idx * 7) + 6,
				StartTime = usedDate.AddDays((idx * 7) + 6).AddHours(8),
				EndTime = usedDate.AddDays((idx * 7) + 6).AddHours(14),
				Notes = "Cool Notes (:",
				LongProjectId = idx + 1
			};

			MD.LongProjectDay Weekday7 = new MD.LongProjectDay
			{
				Id = (idx * 7) + 7,
				StartTime = usedDate.AddDays((idx * 7) + 7).AddHours(8),
				EndTime = usedDate.AddDays((idx * 7) + 7).AddHours(14),
				Notes = "Cool Notes (:",
				LongProjectId = idx + 1
				
			};
			longProjectDayList.AddRange([
					Weekday1,
					Weekday2,
					Weekday3,
					Weekday4,
					Weekday5,
					Weekday6,
					Weekday7
				]);


			longProjectList.Add(new MD.LongProject
			{
				Id = idx + 1,
				Name = "Take A Week...",
				Desc = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec malesuada arcu lacus, quis hendrerit nisi condimentum quis. Suspendisse pretium luctus metus eu feugiat. Mauris odio est, mattis id lectus vitae, vestibulum pulvinar augue. In non finibus sapien. Quisque facilisis augue sed libero fermentum consequat. Cras eros velit, tincidunt vitae magna eget, interdum aliquam tortor. Praesent vestibulum sem nec facilisis condimentum. Duis volutpat metus quis arcu ultricies, ornare ultricies elit gravida. Vivamus semper fringilla neque, eu mollis orci rutrum a. Aliquam erat volutpat.
Nam varius semper dui eget vehicula. Sed in neque ut nisi scelerisque vehicula sit amet non enim. Nullam bibendum ante est. In hac habitasse platea dictumst. Quisque ultricies lacinia urna at rhoncus. In facilisis libero a ligula pretium, quis mattis enim pretium. In id ipsum quis enim gravida efficitur. Vivamus sed ligula purus.

Cras dignissim a nulla et rutrum. Fusce consequat sit amet nunc et pretium. Mauris urna diam, commodo ut tincidunt vitae, condimentum sit amet est. Phasellus vestibulum, dui at tempor condimentum, augue erat sodales quam, sit amet dictum quam nisi vitae tortor. Mauris et egestas felis. Maecenas suscipit tortor massa, id dapibus tortor tempor nec. Mauris faucibus iaculis eros in pellentesque. Fusce malesuada molestie ipsum sit amet posuere. Aenean posuere, elit vel sollicitudin ornare, nibh eros sodales urna, quis ornare mi nibh quis eros. Maecenas et pharetra ipsum. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Cras enim tellus, aliquam sit amet turpis in, faucibus ultricies ligula.

Sed sollicitudin non lacus eget lobortis. Duis ut feugiat velit. Morbi varius est eu dui laoreet convallis. Donec lectus magna, mattis at nisi venenatis, condimentum commodo massa. Phasellus ut ipsum cursus, elementum lacus nec, pellentesque urna. Vivamus erat odio, dignissim non purus ut, facilisis vulputate sapien. Nulla aliquet magna arcu, ut fermentum sapien auctor eget. Aliquam ornare mi aliquam, dictum libero sollicitudin, varius odio. Nulla vitae quam at magna sagittis imperdiet. Aliquam vitae porttitor leo. Sed lacinia sed dolor et varius. Phasellus eleifend mattis nisi, eu gravida libero mattis vitae. Nunc blandit turpis tempor felis facilisis, ac mattis dui tincidunt. Maecenas finibus condimentum sagittis. Duis molestie arcu eget tortor dapibus, at accumsan purus consectetur. Suspendisse eu ex et lacus tincidunt efficitur sed ac diam.

Vivamus eget tincidunt nunc. Ut sagittis nec mi a faucibus. In sagittis risus sit amet enim faucibus placerat. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Ut posuere rhoncus odio, eu vehicula velit mattis quis. Etiam hendrerit mauris odio, vitae elementum ipsum scelerisque quis. Donec convallis erat eget risus luctus, et mollis eros aliquam. Quisque venenatis bibendum velit vel facilisis. Maecenas non tincidunt quam.",
				Deadline = usedDate.AddDays((idx * 7) + 7),
				TotalWorkHours = 40
			});
		}

		modelBuilder.Entity<EF.Sleep>()
			.HasData(
				sleepDataList
			);

		modelBuilder.Entity<EF.Project>()
			.HasData(
				projectDataList
			);
		
		modelBuilder.Entity<MD.LongProject>()
			.HasData(
				longProjectList
			);
		
		modelBuilder.Entity<MD.LongProjectDay>()
			.HasData(
				longProjectDayList
			);
	}

}
