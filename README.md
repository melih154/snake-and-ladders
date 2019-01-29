# snake-and-ladders

## Installation Requirements
.NET Core 2.1 SDK

## Run

### Run Tests
Change directory to individual tests folder and then run
`cd tests\SnakeLadders.Tests.Unit`
`dotnet test SnakeLadders.Tests.Unit.csproj`

## How did you approach the problem?

By listing all the given requirements within Given/When/Then statements.

1. Designing the basic models including PlayerToken, Board and Game
2. Designing the interfaces of these class/interfaces Eg. Game should have the ability to move, Dice should have the ability to roll
3. Designing the interaction between these class/interfaces Eg. A board should have player tokens, a game should have a board and a dice

## How did you make key design decisions and what alternatives did you consider?

TDD friendly implementation:
- Implement interfaces for the functionalities than can be mocked. Eg. Dice roll result
- Simple object model - reflecting the given scenario. Eg. Definition of Board, Game, PlayerToken and Dice - one can easily 
- Configurable settings. Eg. Dice min and max value & game initial and winnder square settings can be injected. This helped on defining unit test cases.

## How do you envision your solution evolving in the future?

### Feature 2
- Implementation of additional features defined on spec requires the seperation of game and board base classes, so one can use SimpleGame and SimpleBoard for the existing game(only Feature 1) and develop and use SnakeLaddersGame and SnakeLaddersBoard objects for Feature 2.

### Feature 3 & 4
- Game does not have any mechanism to control which player has the turn. A complete game engine may require to store that information. In future may need to add game state storage mechanism. A fancy one would include mechanism to revert the movements, using Command Design Pattern
- Adding game modes (Eg. Computer or Human against each other) May add a console app to play the game with silent or console mode.
