# Object-Oriented Text Adventure Game

A text-based adventure game developed in C# demonstrating object-oriented programming principles and design patterns.

## Project Overview

This project was developed as part of my Object-Oriented Programming course at Swinburne University. It showcases the implementation of complex OOP concepts including inheritance hierarchies, interfaces, polymorphism, and design patterns.

## Features

- **Command Pattern Implementation**: Extensible command system for game actions (look, move, etc.)
- **Graph-Based Navigation**: Locations connected via path objects for exploration
- **Nested Inventory System**: Players can carry items and bags with their own inventories
- **Polymorphic Design**: Interface-based polymorphism (IHaveInventory) across multiple container types
- **File I/O**: Save and load game state using StreamWriter/StreamReader
- **Unit Testing**: Comprehensive test suite using NUnit framework

## Technical Highlights

- **Design Patterns**: Command pattern for extensible actions
- **Inheritance Hierarchy**: IdentifiableObject → GameObject → Item/Player/Location
- **LINQ Operations**: Modern C# features (FirstOrDefault, Any, Select)
- **Abstraction**: Abstract classes and virtual methods for extensibility

## Technologies Used

- C# (.NET 9.0)
- NUnit for unit testing
- Object-oriented design principles

## Project Structure

- `IdentifiableObject.cs` - Base class for all identifiable game objects
- `GameObject.cs` - Abstract class extending IdentifiableObject
- `Player.cs` - Player character with inventory management
- `Item.cs` - Base class for game items
- `Bag.cs` - Container items with nested inventory
- `Location.cs` - Game locations with paths
- `Command.cs` - Abstract command class
- `LookCommand.cs` / `MoveCommand.cs` - Concrete command implementations
- `Inventory.cs` - Inventory management system

## Course Information

- **Course**: Object-Oriented Programming (COS20007)
- **Institution**: Swinburne University of Technology
- **Grade**: Distinction (77/100)
- **Year**: 2025

## Note

This is an academic project developed for coursework. The code demonstrates learning outcomes for object-oriented programming concepts and is not intended for production use.
