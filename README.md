# Computer Store kata
Practice your refactoring skills with Computer Store kata.

Some challenges:
- Move from procedural to Object Oriented style
- Test static dependencies (using seams)
- Remove main code smells (primitive obsession, feature envy, etc.)
- Avoid anemic domain classes (move behaviour to domain classes)

This kata was developed in c# for dotnet core but it will probably work on .net framework and can be easily translated to other OOP languages like Java. 

The project is intended to be technology agnostic. Dependencies on frameworks like web or database were removed so no complex setup is needed.

Controllers are not real controllers, are just regular classes with public methods that can be invoked from tests. Authentication is acomplished by passing user credentials to controllers constructors since Request object is not available

Database is an in-memory database implementation with Entity Framework or ORM flavour (apologies to ORM developers).

Finally some good news, project comes with many end to end tests (not all edge cases are tested). It can help you on refactoring as long as controllers methods signatures are respected...

Have fun!
