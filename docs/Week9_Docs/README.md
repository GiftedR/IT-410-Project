# Week 9

I chose the Raw SQL over the stored procedure as I have the database delete after the completion of the program. EFCore doesn't clearly allow the creation of a stored procedure at runtime without using the Raw Sql items, so it was the only choice to make. Linq was less appropriate because the method originally used the id to define a limit of items, however, using the Raw Sql allows you to use the built in Limit keyword allowing for 10 items anywhere. This approach doesn't provide much additional in the way of archetectural needs. I would use the pattern selectively in production as EFCore has a wide variety of built in safegaurds that could be bypassed by using raw sql. Or those items could be alot more bloated and inneffiecent to recieve than using the native items. Preserving the IDataAccess allows us to change the items with requiring a redesign of the UI or any other depencancies of other items.

__Console Output:__
> Launching Sqlite Provider
> Using Seed Data
> 
> ADO Net Sample
> Reading Sleep Operations with a limit of 10
> 1: New Shweep @ 3/17/2026 6:35:58 PM - 3/24/2026 6:35:58 PM; Q: 3
> 2: Placeholder Name... @ 3/18/2026 6:34:52 PM - 3/19/2026 2:34:52 AM; Q: 4
> 3: Placeholder Name... @ 3/19/2026 6:34:52 PM - 3/20/2026 2:34:52 AM; Q: 8
> 4: Placeholder Name... @ 3/20/2026 6:34:52 PM - 3/21/2026 2:34:52 AM; Q: 5
> 5: Placeholder Name... @ 3/21/2026 6:34:52 PM - 3/22/2026 2:34:52 AM; Q: 3
> 6: Placeholder Name... @ 3/22/2026 6:34:52 PM - 3/23/2026 2:34:52 AM; Q: 6
> 7: Placeholder Name... @ 3/23/2026 6:34:52 PM - 3/24/2026 2:34:52 AM; Q: 0
> 8: Placeholder Name... @ 3/24/2026 6:34:52 PM - 3/25/2026 2:34:52 AM; Q: 3
> 9: Placeholder Name... @ 3/25/2026 6:34:52 PM - 3/26/2026 2:34:52 AM; Q: 6
> 10: Placeholder Name... @ 3/26/2026 6:34:52 PM - 3/27/2026 2:34:52 AM; Q: 6
> Testing Updating with a new Sleep
> New first sleep: 1: New Shweep @ 3/17/2026 6:36:42 PM - 3/24/2026 6:36:42 PM; Q: 3
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
> 1: Placeholder Name... @ 3/1/2026 12:00:00 AM - 3/1/2026 8:00:00 AM; Q: 0
> 2: Placeholder Name... @ 3/2/2026 12:00:00 AM - 3/2/2026 8:00:00 AM; Q: 1
> 3: Placeholder Name... @ 3/3/2026 12:00:00 AM - 3/3/2026 8:00:00 AM; Q: 2
> 4: Placeholder Name... @ 3/4/2026 12:00:00 AM - 3/4/2026 8:00:00 AM; Q: 3
> 5: New Shweep @ 3/17/2026 6:35:58 PM - 3/24/2026 6:35:58 PM; Q: 3
> 6: Placeholder Name... @ 3/6/2026 12:00:00 AM - 3/6/2026 8:00:00 AM; Q: 5
> 7: Placeholder Name... @ 3/7/2026 12:00:00 AM - 3/7/2026 8:00:00 AM; Q: 6
> 8: Placeholder Name... @ 3/8/2026 12:00:00 AM - 3/8/2026 8:00:00 AM; Q: 7
> 9: Placeholder Name... @ 3/9/2026 12:00:00 AM - 3/9/2026 8:00:00 AM; Q: 8
> 10: Placeholder Name... @ 3/10/2026 12:00:00 AM - 3/10/2026 8:00:00 AM; Q: 9
> Testing Updating with a new Sleep
> Sleep To Update: 5: New Shweep @ 3/17/2026 6:35:58 PM - 3/24/2026 6:35:58 PM; Q: 3
> New first sleep: 5: New Shweep @ 3/17/2026 6:36:42 PM - 3/24/2026 6:36:42 PM; Q: 3
> Testing Deleting a Sleep at index 120
> Deleted Sleep: No Sleep Found...
> Long Project 356: 356: Take A Week... by 12/26/2032 12:00:00 AM whr 0 / 40 PD:
>         2486: 12/20/2032 8:00:00 AM - 12/20/2032 2:00:00 PM :: Cool Notes (:
>         2487: 12/21/2032 8:00:00 AM - 12/21/2032 2:00:00 PM :: Cool Notes (:
>         2488: 12/22/2032 8:00:00 AM - 12/22/2032 2:00:00 PM :: Cool Notes (:
>         2489: 12/23/2032 8:00:00 AM - 12/23/2032 2:00:00 PM :: Cool Notes (:
>         2490: 12/24/2032 8:00:00 AM - 12/24/2032 2:00:00 PM :: Cool Notes (:
>         2491: 12/25/2032 8:00:00 AM - 12/25/2032 2:00:00 PM :: Cool Notes (:
>         2492: 12/26/2032 8:00:00 AM - 12/26/2032 2:00:00 PM :: Cool Notes (:
> 
> Reading Long Project Operations with a limit of 10
> 1: Take A Week... by 3/8/2026 12:00:00 AM whr 0 / 40 PD:
> 
> 2: Take A Week... by 3/15/2026 12:00:00 AM whr 0 / 40 PD:
> 
> 3: Take A Week... by 3/22/2026 12:00:00 AM whr 0 / 40 PD:
> 
> 4: Take A Week... by 3/29/2026 12:00:00 AM whr 0 / 40 PD:
> 
> 5: Take A Week... by 4/5/2026 12:00:00 AM whr 0 / 40 PD:
> 
> 6: Take A Week... by 4/12/2026 12:00:00 AM whr 0 / 40 PD:
> 
> 7: Take A Week... by 4/19/2026 12:00:00 AM whr 0 / 40 PD:
> 
> 8: Take A Week... by 4/26/2026 12:00:00 AM whr 0 / 40 PD:
> 
> 9: Take A Week... by 5/3/2026 12:00:00 AM whr 0 / 40 PD:
> 
> 10: Take A Week... by 5/10/2026 12:00:00 AM whr 0 / 40 PD:
