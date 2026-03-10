# Week 8

With the new data, the LongProject is the principal and the LongProjectDay is the dependent. The foreign key resides inside the dependent property, it holds the LongProjectId reference. The eager loading was chosen as it was required by the assignment. Explicit loading would be preferrable when updating calculated properties, where you can load exactly what you want to and update it accordingly. N+1 behavior would look like loading the LongProject then looping through and loading each LongProjectDay attached to the LongProject. Preserving IDataAccess is important because it allows us to use it as a fallback just in case EFCore fails.

__Console Output:__
>Launching Sqlite Provider
> 
>Using Seed Data
> 
>
> 
>ADO Net Sample
> 
>Reading Sleep Operations with a limit of 10
> 
>1: Placeholder Name... @ 3/10/2026 11:41:35 AM - 3/10/2026 7:41:35 PM; Q: 7
> 
>2: Placeholder Name... @ 3/11/2026 11:41:35 AM - 3/11/2026 7:41:35 PM; Q: 5
> 
>3: Placeholder Name... @ 3/12/2026 11:41:35 AM - 3/12/2026 7:41:35 PM; Q: 1
> 
>4: Placeholder Name... @ 3/13/2026 11:41:35 AM - 3/13/2026 7:41:35 PM; Q: 5
> 
>5: Placeholder Name... @ 3/14/2026 11:41:35 AM - 3/14/2026 7:41:35 PM; Q: 2
> 
>6: Placeholder Name... @ 3/15/2026 11:41:35 AM - 3/15/2026 7:41:35 PM; Q: 9
> 
>7: Placeholder Name... @ 3/16/2026 11:41:35 AM - 3/16/2026 7:41:35 PM; Q: 3
> 
>8: Placeholder Name... @ 3/17/2026 11:41:35 AM - 3/17/2026 7:41:35 PM; Q: 9
> 
>9: Placeholder Name... @ 3/18/2026 11:41:35 AM - 3/18/2026 7:41:35 PM; Q: 0
> 
>10: Placeholder Name... @ 3/19/2026 11:41:35 AM - 3/19/2026 7:41:35 PM; Q: 5
> 
>Testing Updating with a new Sleep
> 
>New first sleep: 1: New Shweep @ 3/10/2026 11:41:43 AM - 3/17/2026 11:41:43 AM; Q: 3
> 
>Testing Deleting a Sleep at index 100
> 
>Deleted Sleep: No Sleep Found...
> 
>Testing Transaction with id 69
> 
>Original Sleep: 69: Placeholder Name... @ 5/17/2026 11:41:35 AM - 5/17/2026 7:41:35 PM; Q: 9
> 
>Original Project: 69: Placeholder Name... @ 5/17/2026 11:41:35 AM - 5/17/2026 7:41:35 PM; Q: True
> 
>Deleting a Sleep and a Project at index 69
> 
>Deleted Sleep: No Sleep Found...
> 
>Deleted Project: No Project Found...
> 
>
> 
>EF Core Sample
> 
>Reading Sleep Operations with a limit of 10
> 
>1: Placeholder Name... @ 3/1/2026 12:00:00 AM - 3/1/2026 8:00:00 AM; Q: 0
> 
>2: Placeholder Name... @ 3/2/2026 12:00:00 AM - 3/2/2026 8:00:00 AM; Q: 1
> 
>3: Placeholder Name... @ 3/3/2026 12:00:00 AM - 3/3/2026 8:00:00 AM; Q: 2
> 
>4: Placeholder Name... @ 3/4/2026 12:00:00 AM - 3/4/2026 8:00:00 AM; Q: 3
> 
>5: Placeholder Name... @ 3/5/2026 12:00:00 AM - 3/5/2026 8:00:00 AM; Q: 4
> 
>6: Placeholder Name... @ 3/6/2026 12:00:00 AM - 3/6/2026 8:00:00 AM; Q: 5
> 
>7: Placeholder Name... @ 3/7/2026 12:00:00 AM - 3/7/2026 8:00:00 AM; Q: 6
> 
>8: Placeholder Name... @ 3/8/2026 12:00:00 AM - 3/8/2026 8:00:00 AM; Q: 7
> 
>9: Placeholder Name... @ 3/9/2026 12:00:00 AM - 3/9/2026 8:00:00 AM; Q: 8
> 
>10: Placeholder Name... @ 3/10/2026 12:00:00 AM - 3/10/2026 8:00:00 AM; Q: 9
> 
>Testing Updating with a new Sleep
> 
>Sleep To Update: 5: Placeholder Name... @ 3/5/2026 12:00:00 AM - 3/5/2026 8:00:00 AM; Q: 4
> 
>New first sleep: 5: New Shweep @ 3/10/2026 11:41:43 AM - 3/17/2026 11:41:43 AM; Q: 3
> 
>Testing Deleting a Sleep at index 120
> 
>Deleted Sleep: No Sleep Found...
> 
>Long Project 356: 356: Take A Week... by 12/26/2032 12:00:00 AM whr 0 / 40 PD:
> 
>		2486: 12/20/2032 8:00:00 AM - 12/20/2032 2:00:00 PM :: Cool Notes (:
> 
>		2487: 12/21/2032 8:00:00 AM - 12/21/2032 2:00:00 PM :: Cool Notes (:
> 
>		2488: 12/22/2032 8:00:00 AM - 12/22/2032 2:00:00 PM :: Cool Notes (:
> 
>		2489: 12/23/2032 8:00:00 AM - 12/23/2032 2:00:00 PM :: Cool Notes (:
> 
>		2490: 12/24/2032 8:00:00 AM - 12/24/2032 2:00:00 PM :: Cool Notes (:
> 
>		2491: 12/25/2032 8:00:00 AM - 12/25/2032 2:00:00 PM :: Cool Notes (:
> 
>		2492: 12/26/2032 8:00:00 AM - 12/26/2032 2:00:00 PM :: Cool Notes (: