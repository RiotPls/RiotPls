These are the coding conventions to follow in this project.

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
* US names are prefered. Example, Color is prefered over Colour.

# Declarations #

* Use `var` everywhere. Unless it's not possible.
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

* Use `is null` and `is object` for null checking. When we null check, we want to check if the object points to null. 
We don't want to do equatily comparison to a null object (which is an overridable behavior that may lead to NRE and is hard to debug)
