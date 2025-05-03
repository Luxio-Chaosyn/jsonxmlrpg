# JSONXMLRPG

This is a C# .NET console application that allows users to:

- Load JSON data describing people with attributes
- Perform LINQ queries like search, sort, and group
- Export data to XML
- Play a fun guessing game using character stats

## Features

1. **Search** — Find people by name.
2. **Sort** — Sort the list by age.
3. **Group** — Group people by city.
4. **Export** — Save the list to an XML file.
5. **Guessing Game** — Pick a character and guess another randomly chosen person using attribute-based hints.
6. **Exit** — Close the application.

## Game Rules

- Choose a character as your avatar.
- You have:
  - 🎯 `Intelligence` = Number of hints
  - ❤️ `Strength` = HP
  - 🌀 `Agility` = Dodge chance
- Each incorrect guess triggers a dice roll (1–20). If it exceeds 10 + your agility minus the target agility, you take 1 damage.
- Guess correctly before HP runs out!

## Sample Input

Here's a sample JSON you can load into the program:

```json
[
  { "Name": "Alice", "Age": 30, "City": "New York", "Strength": 6, "Agility": 14, "Intelligence": 3 },
  { "Name": "Bob", "Age": 25, "City": "Chicago", "Strength": 10, "Agility": 13, "Intelligence": 2 },
  { "Name": "Charlie", "Age": 35, "City": "New York", "Strength": 14, "Agility": 12, "Intelligence": 1 }
]
