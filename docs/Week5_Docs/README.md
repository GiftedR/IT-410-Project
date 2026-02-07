# Week 5

For the refactor of this week, it was mentioned to me that the connection string was hard coded. To fix this I moved the connection strings for the providers to the file [Connection-String.json](../../IT410Project.Launch/Connection-String.json). To make sure that the connection strings are still valid, I added checks to the program.cs. Unfortunately, C# doesn't allow for static constructors with parameters. This improves maintainability because it allows software like Docker and Github Actions to define their own custom connection strings. This allows them to hide certain details that should be inaccesible to the majority, but still runs with them.


__Output Before__
```
Sql Server: False
Use Sqlite True
Launching Sqlite Provider
Using Seed Data
Reading Sleep Operations with a limit of 10
1: Placeholder Name... @ 2/6/2026 6:28:13 PM - 2/7/2026 2:28:13 AM; Q: 7
2: Placeholder Name... @ 2/7/2026 6:28:13 PM - 2/8/2026 2:28:13 AM; Q: 3
3: Placeholder Name... @ 2/8/2026 6:28:13 PM - 2/9/2026 2:28:13 AM; Q: 9
4: Placeholder Name... @ 2/9/2026 6:28:13 PM - 2/10/2026 2:28:13 AM; Q: 0
5: Placeholder Name... @ 2/10/2026 6:28:13 PM - 2/11/2026 2:28:13 AM; Q: 9
6: Placeholder Name... @ 2/11/2026 6:28:13 PM - 2/12/2026 2:28:13 AM; Q: 3
7: Placeholder Name... @ 2/12/2026 6:28:13 PM - 2/13/2026 2:28:13 AM; Q: 3
8: Placeholder Name... @ 2/13/2026 6:28:13 PM - 2/14/2026 2:28:13 AM; Q: 6
9: Placeholder Name... @ 2/14/2026 6:28:13 PM - 2/15/2026 2:28:13 AM; Q: 0
10: Placeholder Name... @ 2/15/2026 6:28:13 PM - 2/16/2026 2:28:13 AM; Q: 6
Testing Updating with a new Sleep
New first sleep: 1: New Shweep @ 2/6/2026 6:28:13 PM - 2/13/2026 6:28:13 PM; Q: 3
Testing Deleting a Sleep at index 100
Deleted Sleep: No Sleep Found...
Testing Transaction with id 69
Original Sleep: 69: Placeholder Name... @ 4/15/2026 6:28:13 PM - 4/16/2026 2:28:13 AM; Q: 2
Original Project: 69: Placeholder Name... @ 4/15/2026 6:28:13 PM - 4/16/2026 2:28:13 AM; Q: True
Deleting a Sleep and a Project at index 69
Deleted Sleep: No Sleep Found...
Deleted Project: No Project Found...
```

__Output After__
```
Sql Server: False
Use Sqlite True
Launching Sqlite Provider
Using Seed Data
Reading Sleep Operations with a limit of 10
1: Placeholder Name... @ 2/6/2026 8:27:40 PM - 2/7/2026 4:27:40 AM; Q: 6
2: Placeholder Name... @ 2/7/2026 8:27:40 PM - 2/8/2026 4:27:40 AM; Q: 5
3: Placeholder Name... @ 2/8/2026 8:27:40 PM - 2/9/2026 4:27:40 AM; Q: 8
4: Placeholder Name... @ 2/9/2026 8:27:40 PM - 2/10/2026 4:27:40 AM; Q: 3
5: Placeholder Name... @ 2/10/2026 8:27:40 PM - 2/11/2026 4:27:40 AM; Q: 9
6: Placeholder Name... @ 2/11/2026 8:27:40 PM - 2/12/2026 4:27:40 AM; Q: 6
7: Placeholder Name... @ 2/12/2026 8:27:40 PM - 2/13/2026 4:27:40 AM; Q: 8
8: Placeholder Name... @ 2/13/2026 8:27:40 PM - 2/14/2026 4:27:40 AM; Q: 3
9: Placeholder Name... @ 2/14/2026 8:27:40 PM - 2/15/2026 4:27:40 AM; Q: 8
10: Placeholder Name... @ 2/15/2026 8:27:40 PM - 2/16/2026 4:27:40 AM; Q: 1
Testing Updating with a new Sleep
New first sleep: 1: New Shweep @ 2/6/2026 6:27:40 PM - 2/13/2026 6:27:40 PM; Q: 3
Testing Deleting a Sleep at index 100
Deleted Sleep: No Sleep Found...
Testing Transaction with id 69
Original Sleep: 69: Placeholder Name... @ 4/15/2026 8:27:40 PM - 4/16/2026 4:27:40 AM; Q: 8
Original Project: 69: Placeholder Name... @ 4/15/2026 8:27:40 PM - 4/16/2026 4:27:40 AM; Q: True
Deleting a Sleep and a Project at index 69
Deleted Sleep: No Sleep Found...
Deleted Project: No Project Found...
```