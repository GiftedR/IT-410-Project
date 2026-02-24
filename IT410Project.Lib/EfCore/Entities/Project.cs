using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IT410Project.Lib.EfCore.Entities;

public partial class Project
{
	[Key]
	public int Id { get; set; }

	public string Name { get; set; } = null!;

	[Column(TypeName = "DATETIME")]
	public string StartTime { get; set; } = null!;

	[Column(TypeName = "DATETIME")]
	public string EndTime { get; set; } = null!;

	public string Desc { get; set; } = null!;

	[Column(TypeName = "BIT")]
	public bool IsRepeating { get; set; }
}
