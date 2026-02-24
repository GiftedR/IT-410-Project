dotnet ef dbcontext scaffold "Data Source=Data/Database.db" Microsoft.EntityFrameworkCore.Sqlite --output-dir EfCore/Entities --context-dir EfCore/Context --context ProjectDbContext --data-annotations
dotnet ef migrations add Baseline_ExistingDatabase --output-dir EfCore/Migrations
dotnet ef migrations add Baseline_ExistingDatabase --output-dir EfCore/Migrations --ignore_changes ##This doesnt exist in the cli
