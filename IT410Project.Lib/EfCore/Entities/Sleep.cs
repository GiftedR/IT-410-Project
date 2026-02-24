using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IT410Project.Lib.EfCore.Entities;

[Table("Sleep")]
public partial class Sleep
{
	[Key]
	public int Id { get; set; }

	public string Name { get; set; } = null!;

	[Column(TypeName = "DATETIME")]
	public string StartTime { get; set; } = null!;

	[Column(TypeName = "DATETIME")]
	public string EndTime { get; set; } = null!;

	public int Quality { get; set; }

	public int RepeatDays { get; set; }
}
