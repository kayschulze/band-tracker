# _Band Tracker_

#### _Program tracks bands and the venues where they have played, August 29, 2017_

#### By _**Kim Schulze**_

## Description

_The program uses a many to many relationship to track bands and the venues they have played at.  A venue can host multiple band.  A band can play at multiple different venues.  This requires a join table._

## Setup/Installation Requirements

* _Use an up-to-date browser._

## Specifications
| Behavior | Input | Output | Reasoning |
| ---- | ---- | ---- | ---- |
| 1. Band object formed and overrides equals | Name: "Trees", Manager: "Nehemia", Manager Phone: "503-555-7890", Band Leader: "Tayla", Band Leader Phone: "206-555-6800" | Name: "Trees", Manager: "Nehemia", Manager Phone: "503-555-7890", Band Leader: "Tayla", Band Leader Phone: "206-555-6800", id = 1 | It is necessary to make sure that Band Objects can be equal. |
| 2. Venue object formed and overrides equals | Name: "Menashe Aaron's Table", Phone Number: "206-333-4444", Venue Contact: "Ronald Roberts" id = 2 | It is necessary to show Venue Objects are equal. |
| 3. Create and Save A Band object to a database | Name: "Trees", Manager: "Nehemia", Manager Phone: "503-555-7890", Band Leader: "Tayla", Band Leader Phone: "206-555-6800" | Name: "Trees", Manager: "Nehemia", Manager Phone: "503-555-7890", Band Leader: "Tayla", Band Leader Phone: "206-555-6800" | Part of CRUD, method creates the Band object and saves it in the database. |
| 4. Create and Save A Venue object to a database | Name: "Trees", Manager: "Nehemia", Manager Phone: "503-555-7890", Band Leader: "Tayla", Band Leader Phone: "206-555-6800", id = 1 | It is necessary to make sure that Band Objects can be equal. | Part of CRUD, method creates the Venue object and saves it in the database. |
| 5. Method gets all Bands from the database | Get All | "Trees", "Coders" | Retrieval is part of CRUD, reading all Bands in the database. |
| 6. Methods gets all Venues from the database | Get All | "Menashe Aaron's Table", "Beth Shalom's Shul" | Retrieval is part of CRUD, reading all Venues in the database. |
| 7. Find a Band based on its ID | ID: 1 | Band: "Trees" | Allows an integer to represent a Band object. |
| 8. Find a Venue based on its ID | ID: 2 | Venue: "Menashe Aaron's Table" | Allows an integer to represent a Band object. |
| 9. Update Band Name | Name: "Green Pointy Trees" | Name: "Trees" | Information must be malleable for different reasons of change. |
| 10. Update Venue Name | Name: "Menashe Aaron's Table" | Name: "Menashe Aaron's" | Information must be malleable for different reasons. |
| 11. Add Band to a Venue | Band Name: "Trees", Band ID: 1, Venue: "Menashe Aaron's", Venue ID: 2 | link band 1 to venue 2 | The purpose of this application is to link multiple relationships. |


## Known Bugs

_There are no known bugs._

## Support and contact details

_With questions contact Kim Schulze at kimberlykayschulze@gmail.com._

## Technologies Used

_Used C#, .NET, MVC, and HTML_

### License

*All Rights Reserved.  Version 1.0*

Copyright (c) 2017 **_Kim Schulze_**
