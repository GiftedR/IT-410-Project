# Week 6

__Scaffolding__ - The process of scaffolding this project began by making sure the required directories were there, and making sure that the naming convention wouldnt get confusing. Attempting to run the commands provided already provided some issues. Since im using the dotnet cli as opposed to the PM Console, commands that look like this:

> Scaffold-DbContext "YOUR_CONNECTION_STRING" Microsoft.EntityFrameworkCore.SqlServer -OutputDir EfCore/Entities -ContextDir EfCore/Context -Context ProjectDbContext -DataAnnotations

Turned more into something like this:

> dotnet ef dbcontext scaffold "YOUR_CONNECTION_STRING" Microsoft.EntityFrameworkCore.SqlServer --output-dir EfCore/Entities --context-dir EfCore/Context --context ProjectDbContext --data-annotations

After getting this to work, a dbcontext file was created. Thought this came with its own issues. Trying to create a migration after this resulted in an empty migration file. This is because the DbContext didnt contain any reference to the models. To fix this it was as simple as adding two DbSet properties with the Sleep and Project types. Then the migration worked properly.

__Migration__ - When creating the migration, I had to begin by translating the command to the cli version.

> dotnet ef migration add Baseline_ExistingDatabase --output-dir EfCore/Migrations

But it was missing something, the -IngoreChanges property. As it turns out, it doesn't exist (at least in the documentation) for the cli. Ommiting it worked as we expected it to.

The main purpose of the first migration after the scaffolding process is to define a starting point for all future databases to go off of. These migrations act like a changelog and can be created to almost any point in the migration history. Much like git of database schema changes.

__Verification__ - Verification was the simplest for this step. Since there was no need to run tests against the new controllers, all that needed to be checked was the migrations. Which looked fine and matched what I expected the schema to be. When adding seed data later, the migration files will become massive. And might take a minute to check for validation. Also because I'm using DbLite, things might change wildly from feature to feature.