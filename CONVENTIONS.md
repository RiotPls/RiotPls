Here are the coding conventions to follow in this project. Pull Requests (PRs) that don't match the conventions will have changes requested.

Most of the conventions match [Microsoft's C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions).

# Nomenclatures #

* Namespaces, Classes, Structures: PascalCase 
* Interfaces: PascalCase (with `I` prefix)
* Methods: PascalCase
* Private Fields: camelCase (with '_' prefix)
* Protected Fields: camelCase 
* Internal/Public Fields: PascalCase
* Static/Constant members: PascalCase (regardless of the access modifier)
* Delegates: PascalCase
* Events: PascalCase

# Naming Conventions #

* All public members must have full name. Acronyms are to be avoided (LeagueOfLegends instead of LoL, TeamfightTactics instead of TFT, etc...). 
* American English is the preferred language. For example, Color is prefered over Colour.

# Declarations #

* Usage of `var` is preferred, except where the variable type is not obvious.
* Declare all fields/variables in their respective line.
* Use All Man's Brances, you can free to omit braces if they aren't needed as long as it doesn't hurt readability or makes the code ambiguous.

# Indentation #

* Use 4 spaces indentation.

# Line Wraps #

* Maximun line length is 120.

# Latest Language Features #

* Using new language features such a Switch Expression is completely optional. 
Yet, don't submit PRs just to change a normal switch into a switch expression or viceversa.

# Null checking #

* Use `is null` and `is object` for checking for null, instead of `== null`.
Equality comparison to a null object (i.e. `== null`) is overridable behavior that may lead to a NullReferenceException and is harder to debug.

# Documentation #
* Add four spaces after the `///` for the content of `<summary>` tags.
* Documentation is required on all public-facing members, and classes.
* Accurate and proper English is expected in documentation, but this may be updated by a maintainer in a Pull Request.
* Articles (`the`, `a/an`, `this`) are expected.
