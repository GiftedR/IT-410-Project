USE master;

GO

IF EXISTS (SELECT database_id
FROM sys.databases
WHERE name = 'IT410ProjectMilestone')
BEGIN

alter database IT410ProjectMilestone set single_user with rollback immediate;

DROP DATABASE IT410ProjectMilestone;


END

GO

CREATE DATABASE IT410ProjectMilestone;

GO

-- Still building sample data
