# Week 7

EfCores tracked update differes because it is always active while with ADO.NET we have to manually implement a similar feature. SaveChanges() is required as when querying it only saves it to an in memory copy of the database. Calling the method compares the changes stored in memory and sends it out as one remote query in order to reduce the number of times the database is called. One advantage of EFCore is the ability to expand into different types of scheduled items. When I go to add event scheduling (Like convention style events), it would almost copy and paste of a controller to get eveything up and running. The downside to this that I would still be able to do in ADO.NET is when I want to add a more complex schedule item. Something like a cascading event, where you can schedule a certain amount of time each week rather than per day, and it adjusts the other items during that week to make sure you still hit the week time goal. An example of this would be, expecting to work on a project for 5hrs each day for a total of 35hrs a week, missing monday, and the hours are spread across the rest of the items during the week to make sure you hit the weekly goal.

__Console Output:__
> Launching Sqlite Provider
> Using Seed Data
> 
> ADO Net Sample
> Reading Sleep Operations with a limit of 10
> 1: Placeholder Name... @ 2/24/2026 1:42:07 PM - 2/24/2026 9:42:07 PM; Q: 8
> 2: Placeholder Name... @ 2/25/2026 1:42:07 PM - 2/25/2026 9:42:07 PM; Q: 2
> 3: Placeholder Name... @ 2/26/2026 1:42:07 PM - 2/26/2026 9:42:07 PM; Q: 5
> 4: Placeholder Name... @ 2/27/2026 1:42:07 PM - 2/27/2026 9:42:07 PM; Q: 9
> 5: Placeholder Name... @ 2/28/2026 1:42:07 PM - 2/28/2026 9:42:07 PM; Q: 8
> 6: Placeholder Name... @ 3/1/2026 1:42:07 PM - 3/1/2026 9:42:07 PM; Q: 5
> 7: Placeholder Name... @ 3/2/2026 1:42:07 PM - 3/2/2026 9:42:07 PM; Q: 5
> 8: Placeholder Name... @ 3/3/2026 1:42:07 PM - 3/3/2026 9:42:07 PM; Q: 7
> 9: Placeholder Name... @ 3/4/2026 1:42:07 PM - 3/4/2026 9:42:07 PM; Q: 7
> 10: Placeholder Name... @ 3/5/2026 1:42:07 PM - 3/5/2026 9:42:07 PM; Q: 5
> Testing Updating with a new Sleep
> New first sleep: 1: New Shweep @ 2/24/2026 1:42:07 PM - 3/3/2026 1:42:07 PM; Q: 3
> Testing Deleting a Sleep at index 100
> Deleted Sleep: No Sleep Found...
> Testing Transaction with id 69
> Original Sleep: 69: Placeholder Name... @ 5/3/2026 1:42:07 PM - 5/3/2026 9:42:07 PM; Q: 8
> Original Project: 69: Placeholder Name... @ 5/3/2026 1:42:07 PM - 5/3/2026 9:42:07 PM; Q: True
> Deleting a Sleep and a Project at index 69
> Deleted Sleep: No Sleep Found...
> Deleted Project: No Project Found...
> 
> EF Core Sample
> Reading Sleep Operations with a limit of 10
> 1: New Shweep @ 2026-02-24 13:42:07.8450732 - 2026-03-03 13:42:07.8450851; Q: 3
> 2: Placeholder Name... @ 2/25/2026 1:42:07 PM - 2/25/2026 9:42:07 PM; Q: 2
> 3: Placeholder Name... @ 2/26/2026 1:42:07 PM - 2/26/2026 9:42:07 PM; Q: 5
> 4: Placeholder Name... @ 2/27/2026 1:42:07 PM - 2/27/2026 9:42:07 PM; Q: 9
> 5: Placeholder Name... @ 2/28/2026 1:42:07 PM - 2/28/2026 9:42:07 PM; Q: 8
> 6: Placeholder Name... @ 3/1/2026 1:42:07 PM - 3/1/2026 9:42:07 PM; Q: 5
> 7: Placeholder Name... @ 3/2/2026 1:42:07 PM - 3/2/2026 9:42:07 PM; Q: 5
> 8: Placeholder Name... @ 3/3/2026 1:42:07 PM - 3/3/2026 9:42:07 PM; Q: 7
> 9: Placeholder Name... @ 3/4/2026 1:42:07 PM - 3/4/2026 9:42:07 PM; Q: 7
> 10: Placeholder Name... @ 3/5/2026 1:42:07 PM - 3/5/2026 9:42:07 PM; Q: 5
> Testing Updating with a new Sleep
> Sleep To Update: 2: Placeholder Name... @ 2/25/2026 1:42:07 PM - 2/25/2026 9:42:07 PM; Q: 2
> New first sleep: 2: New Shweep @ 2/24/2026 1:42:09 PM - 3/3/2026 1:42:09 PM; Q: 3
> Testing Deleting a Sleep at index 101
> Deleted Sleep: No Sleep Found...
> Testing Transaction with id 70
> Original Sleep: 70: Placeholder Name... @ 5/4/2026 1:42:07 PM - 5/4/2026 9:42:07 PM; Q: 4
> Original Project: 70: Placeholder Name... @ 5/4/2026 1:42:07 PM - 5/4/2026 9:42:07 PM; R: False