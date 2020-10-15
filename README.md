# ComputerStore
Practice your refactoring skills with Computer Store kata.

Some challenges:
- Move from procedural to Object Oriented style
- Test static dependencies (using seams)
- Remove main code smells (primitive obsession, feature envy, etc.)
- Avoid anemic domain classes (move behaviour to domain classes)

This kata was developed in c# for dotnet core but it will probably work on .net framework and can be easily translated to other OOP languages like Java. 

The project is intended to be technology agnostic. Dependencies on frameworks like web or database where removed so no complex setup is needed.

Controllers are not real controllers, are just regular classes with public methods that can be invoked from tests. Since Request object is not available authentication is acomplished by passing user credentials on controllers constructors.

Database is an in-memory database implementation with Entity Framework or ORM flavour (apologies to ORM developers).

Have fun!
