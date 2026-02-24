# Week 7

EfCores tracked update differes because it is always active while with ADO.NET we have to manually implement a similar feature. SaveChanges() is required as when querying it only saves it to an in memory copy of the database. Calling the method compares the changes stored in memory and sends it out as one remote query in order to reduce the number of times the database is called. One advantage of EFCore is the ability to expand into different types of scheduled items. When I go to add event scheduling (Like convention style events), it would almost copy and paste of a controller to get eveything up and running. The downside to this that I would still be able to do in ADO.NET is when I want to add a more complex schedule item. Something like a cascading event, where you can schedule a certain amount of time each week rather than per day, and it adjusts the other items during that week to make sure you still hit the week time goal. An example of this would be, expecting to work on a project for 5hrs each day for a total of 35hrs a week, missing monday, and the hours are spread across the rest of the items during the week to make sure you hit the weekly goal.

__Console Output:__
> Launching Sqlite Provider
> Using Seed Data
> 
> ADO Net Sample
> Reading Sleep Operations with a limit of 10
> 1: New Shweep @ 2/24/2026 10:28:02 AM - 3/3/2026 10:28:02 AM; Q: 3
> 2: Placeholder Name... @ 2/25/2026 10:01:33 AM - 2/25/2026 6:01:33 PM; Q: 4
> 3: Placeholder Name... @ 2/26/2026 10:01:33 AM - 2/26/2026 6:01:33 PM; Q: 2
> 4: Placeholder Name... @ 2/27/2026 10:01:33 AM - 2/27/2026 6:01:33 PM; Q: 8
> 5: Placeholder Name... @ 2/28/2026 10:01:33 AM - 2/28/2026 6:01:33 PM; Q: 0
> 6: Placeholder Name... @ 3/1/2026 10:01:33 AM - 3/1/2026 6:01:33 PM; Q: 3
> 7: Placeholder Name... @ 3/2/2026 10:01:33 AM - 3/2/2026 6:01:33 PM; Q: 4
> 8: Placeholder Name... @ 3/3/2026 10:01:33 AM - 3/3/2026 6:01:33 PM; Q: 7
> 9: Placeholder Name... @ 3/4/2026 10:01:33 AM - 3/4/2026 6:01:33 PM; Q: 3
> 10: Placeholder Name... @ 3/5/2026 10:01:33 AM - 3/5/2026 6:01:33 PM; Q: 2
> Testing Updating with a new Sleep
> New first sleep: 1: New Shweep @ 2/24/2026 10:28:47 AM - 3/3/2026 10:28:47 AM; Q: 3
> Testing Deleting a Sleep at index 100
> Deleted Sleep: No Sleep Found...
> Testing Transaction with id 69
> Original Sleep: No Sleep Found...
> Original Project: No Project Found...
> Deleting a Sleep and a Project at index 69
> Deleted Sleep: No Sleep Found...
> Deleted Project: No Project Found...
> 
> EF Core Sample
> Reading Sleep Operations with a limit of 10
> 1: New Shweep @ 2026-02-24 10:28:47.0236611 - 2026-03-03 10:28:47.0236669; Q: 3
> 2: Placeholder Name... @ 2/25/2026 10:01:33 AM - 2/25/2026 6:01:33 PM; Q: 4
> 3: Placeholder Name... @ 2/26/2026 10:01:33 AM - 2/26/2026 6:01:33 PM; Q: 2
> 4: Placeholder Name... @ 2/27/2026 10:01:33 AM - 2/27/2026 6:01:33 PM; Q: 8
> 5: Placeholder Name... @ 2/28/2026 10:01:33 AM - 2/28/2026 6:01:33 PM; Q: 0
> 6: Placeholder Name... @ 3/1/2026 10:01:33 AM - 3/1/2026 6:01:33 PM; Q: 3
> 7: Placeholder Name... @ 3/2/2026 10:01:33 AM - 3/2/2026 6:01:33 PM; Q: 4
> 8: Placeholder Name... @ 3/3/2026 10:01:33 AM - 3/3/2026 6:01:33 PM; Q: 7
> 9: Placeholder Name... @ 3/4/2026 10:01:33 AM - 3/4/2026 6:01:33 PM; Q: 3
> 10: Placeholder Name... @ 3/5/2026 10:01:33 AM - 3/5/2026 6:01:33 PM; Q: 2
> Testing Updating with a new Sleep
> New first sleep: 2: Placeholder Name... @ 2/25/2026 10:01:33 AM - 2/25/2026 6:01:33 PM; Q: 4
> Testing Deleting a Sleep at index 101
> Deleted Sleep: No Sleep Found...
> Testing Transaction with id 70
> Original Sleep: 70: Placeholder Name... @ 5/4/2026 10:01:33 AM - 5/4/2026 6:01:33 PM; Q: 5
> Original Project: 70: Placeholder Name... @ 5/4/2026 10:01:33 AM - 5/4/2026 6:01:33 PM; R: False