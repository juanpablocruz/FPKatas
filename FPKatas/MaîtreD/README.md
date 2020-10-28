# The MaîtreD kata

Imagine that you're developing an online restaurant reservation system. Part of the behaviour of such a system is to decide whether or not to accept a reservation. At a real restaurant, employees fill various roles required to make it work. In a high-end restaurant, the maître d' is responsible for taking reservations.

*The objective of the exercise is to implement the MaîtreD decision logic.*

Reservations are accepted on a first-come, first-served basis. As long as the restaurant has available seats for the desired reservation, it'll accept it.

A reservation contains, at a minimum, a date and time as well as a positive quantity. Here's some examples:

| Date |	Quantity |
|--|--|
|August 8, 2050 at 19:30|	3|
|November 27, 2022 at 18:45|	4|
|February 27, 2014 at 13:22|	12|

Notice that dates can be in your future or past. You might want to assume that the maître d' would reject reservations in the past, but you can't assume _when_ the code runs (or ran), so don't worry about that. 

Notice also that quantities are positive integers. While a quantity shouldn't be negative or zero, it could conceivably be large. _Keep quantities at low two-digit numbers or less_.

A reservation will likely contain other data, such as the name of the person making the reservation, contact information such as email or phone number, possibly also an ID, and so on. You may add these details if you want to make the exercise more realistic, but they're not required.

## Boutique restaurant
Assume that the restaurant is only open for dinner, has no second seating, and a single shared table. This implies that the time of day of reservations doesn't matter, while the date still matters. Some possible test cases could be:

|Table size|	Existing reservations|	Candidate reservation|	Expected outcome|
|--|--|--|--|
|12|	none|	Quantity: 1|	Accepted|
|12|	none|	Quantity: 13|	Rejected|
|12|	none|	Quantity: 12|	Accepted|
|4|	Quantity: 2, Date: 2023-09-14|	Quantity: 3, Date: 2023-09-14|	Rejected|
|10|	Quantity: 2, Date: 2023-09-14|	Quantity: 3, Date: 2023-09-14|	Accepted|
|10|Quantity: 3, Date: 2023-09-14<br>Quantity: 2, Date: 2023-09-14 <br>Quantity: 3, Date: 2023-09-14|	Quantity: 3, Date: 2023-09-14|	Rejected|
|4|	Quantity: 2, Date: 2023-09-15|	Quantity: 3, Date: 2023-09-14|	Accepted|

This may not be an exhaustive set of test cases, but hopefully illustrates the desired behaviour.

## Haute cuisine

The single-shared-table configuration is unusual. Most restaurants have separate tables. High-end restaurants like those on the World's 50 best list, or those with Michelin stars often have only a single seating. This is a good expansion of the domain logic.

Assume that a restaurant has several tables, perhaps of different sizes. A table for four will seat one, two, three, or four people. Once a table is reserved, however, all the seats at that table are reserved. A reservation for three people will occupy a table for four, and the redundant seat is wasted. Obviously, the restaurant wants to maximise the number of guests, so it'll favour reserving two-person tables for one and two people, four-person tables for three and four people, and so on.

In order to illustrate the desired behaviour, here's some extra test cases to add to the ones already in place:

|Tables|	Existing reservations|	Candidate reservation|	Expected outcome|
|--|--|--|--|
|Two tables for two<br>Two tables for four|	none|	Quantity: 4, Date: 2024-06-07 |	Accepted|
|Two tables for two <br>Two tables for four|	none|	Quantity: 5, Date: 2024-06-07|	Rejected|
|Two tables for two<br>One table for four|	Quantity: 2, Date: 2024-06-07|	Quantity: 4, Date: 2024-06-07|	Accepted|
|Two tables for two<br>One table for four|	Quantity: 3, Date: 2024-06-07|	Quantity: 4, Date: 2024-06-07|	Rejected|

Again, you should consider adding more test cases if you're unit-testing the kata.

## Second seatings

Some restaurants (even some of those on the World's 50 best list) have a second seating. As a diner, you have a limited time (e.g. 2½ hours) to complete your meal. After that, other guests get your table.

This implies that you must now consider the time of day of reservations. You should also be able to use an arbitrary (positive) seating duration. All previous rules should still apply. New test cases include:

|Seating duration|	Tables|	Existing reservations|	Candidate reservation|	Expected outcome|
|--|--|--|--|--|
|2 hours|	Two tables for two <br> One table for four|	Quantity: 4, Date: 2023-10-22, Time: 18:00|	Quantity: 3, Date: 2023-10-22, Time: 20:00|	Accepted|
|2½ hours|	One table for two<br>Two tables for four|	Quantity: 2, Date: 2023-10-22, Time: 18:00<br>Quantity: 1, Date: 2023-10-22, Time: 18:15<br>Quantity: 2, Date: 2023-10-22, Time: 17:45|	Quantity: 3, Date: 2023-10-22, Time: 20:00|	Rejected|
|2½ hours|	One table for two<br>Two tables for four	Quantity: 2, Date: 2023-10-22, Time: 18:00<br>Quantity: 2, Date: 2023-10-22, Time: 17:45|	Quantity: 3, Date: 2023-10-22, Time: 20:00|	Accepted|
|2½ hours|	One table for two<br>Two tables for four|	Quantity: 2, Date: 2023-10-22, Time: 18:00<br>Quantity: 1, Date: 2023-10-22, Time: 18:15<br>Quantity: 2, Date: 2023-10-22, Time: 17:45|	Quantity: 3, Date: 2023-10-22, Time: 20:15|	Accepted|

If you make the seating duration short enough, you may even make room for a third seating, and so on.

## Alternative table configurations

If tables are rectangular, the restaurant has the option to combine several smaller tables into one larger. Consider a typical restaurant layout like this:

![alternate-table-configuration](https://blog.ploeh.dk/content/binary/restaurant-configuration-with-three-individual-two-person-tables.png)

There's a round four-person table, as well as a few small tables that can't easily be pushed together. There's also three (orange) two-person tables where one guest sits against the wall, and the other diner faces him or her. These can be used as shown above, but the restaurant can also push two of these tables together to accommodate four people:

![alternate-table-configuration](https://blog.ploeh.dk/content/binary/restaurant-configuration-with-two-two-person-tables-combined.png)

This still leaves one of the adjacent two-person tables as an individual table, but the restaurant can also push all three tables together to accommodate six people:


![alternate-table-configuration](https://blog.ploeh.dk/content/binary/restaurant-configuration-with-all-two-person-tables-combined.png)

Implement decision logic that allows for alternative table configurations. Remember to take seating durations into account. Consider both the configuration illustrated, as well as other configurations. Note that in the above configuration, not all two-person tables can be combined.