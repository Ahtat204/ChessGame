chessgame: A Unity Chess game Implementation ♟️

This repository, contains the core C\# scripts ,with other assets for a Chess game implemented using the **Unity** game engine. The design follows several object-oriented principles, utilizing an abstract base class for pieces, behavior-specific decorators, and command patterns for movement.

-----

## 📂 Core Scripts Directory Structure

The `Assets/Scripts` directory is organized into four main categories: `Classes`, `Enums`, `Interfaces`, and `Structs`.

```
└── Scripts/
    ├── SelectedPiece.cs         
    ├── Classes/
    │   ├── BehaviorClasses/
    │   │   ├── KingCastling.cs
    │   │   ├── MoveCommand.cs
    │   │   ├── MovementManager.cs
    │   │   └── SelectableDecorator.cs
    │   ├── GameClasses/
    │   │   ├── Board.cs
    │   │   ├── GameManager.cs
    │   │   ├── InputManager.cs
    │   │   ├── MainMenuController.cs
    │   │   └── Proxies/
    │   │       ├── King.Proxy.cs
    │   │       ├── Pawn.Proxy.cs
    │   │       └── PieceMovementProxy.cs
    │   ├── Pieces/
    │   │   ├── Bishop.cs
    │   │   ├── King.cs
    │   │   ├── Knight.cs
    │   │   ├── Pawn.cs
    │   │   ├── Queen.cs
    │   │   └── Rook.cs
    │   ├── Piece.cs
    │   └── UtilityClass.cs
    ├── Enums/
    │   ├── GameState.cs
    │   ├── MoveType.cs
    │   ├── PieceColor.cs
    │   └── SelectionStatus.cs
    ├── Interfaces/
    │   ├── ICapturable.cs
    │   ├── ICommand.cs
    │   ├── IPromotable.cs
    │   └── ISelectable.cs
    └── Structs/
        └── Coordinates.cs
```

-----

## 🛠️ Key Components & Design

### **Classes**

| File | Description |
| :--- | :--- |
| `Piece.cs` | **Abstract Base Class** for all chess pieces, defining essential properties like `PossibleMoves`, `Color`, and `Value`, and an abstract method for calculating legal moves. |
| `UtilityClass.cs` | Provides static utility methods, such as a conditional `DebugLog` for use in the Unity Editor. |
| `SelectedPiece.cs` | A **Singleton** MonoBehaviour used to track the currently selected chess piece, acting as a global selector/detector. |

#### Behavior Classes

| File | Description |
| :--- | :--- |
| `MovementManager.cs` | Handles the **piece's movement logic**, input detection, and collision for capturing. It relies on the piece's `PossibleMoves` list. |
| `SelectableDecorator.cs` | Implements the `ISelectable` interface, handling **selection and deselection** logic for a piece, often decorated onto a `Piece` object. |
| `MoveCommand.cs` | A generic class implementing `ICommand`, designed to encapsulate a piece movement as a command, allowing for **undo/redo functionality** (though `Execute()` and `Undo()` are currently not fully implemented). |
| `KingCastling.cs` | Dedicated class for handling the specific behavior of the **King's Castling** move. |

#### Game Classes

| File | Description |
| :--- | :--- |
| `Board.cs` | A **Singleton** MonoBehaviour providing centralized access to the Unity `Tilemap` and the main `Camera`, simplifying world-to-cell coordinate conversions. |
| `GameManager.cs` | Manages the **overall game state**, including player turns (`Turn`), game conditions (`GameState`), and tracking the pieces on the board. |
| `InputManager.cs` | A **Singleton** class for centralizing input handling (e.g., mouse direction and position). |
| `MainMenuController.cs` | Handles scene management logic, specifically for loading the main "GameScene" from the **Main Menu**. |
| `Proxies/` | Directory for classes implementing the **Proxy pattern** to restrict or modify piece movement under special game rules. |
| `Proxies/King.Proxy.cs` | **Restricts the King's movement** to prevent it from moving into a threatened square (check). |
| `Proxies/Pawn.Proxy.cs` | (Planned) Likely for handling complex Pawn movements like **En Passant** or **Promotion**. |
| `Proxies/PieceMovementProxy.cs` | (Planned) Responsible for preventing pieces from moving through or occupying squares with friendly pieces. |

#### Piece Subclasses

Concrete implementations of the abstract `Piece` class for each chess piece, defining its specific `Value` and move calculation logic.

  * `Bishop.cs`
  * `King.cs`
  * `Knight.cs`
  * `Pawn.cs`
  * `Queen.cs`
  * `Rook.cs`

-----

### **Enums & Structs**

  * **Enums:**
      * `GameState.cs`: Defines states like `WaitingForPlayer`, `Check`, `Checkmate`.
      * `MoveType.cs`: Specifies move types like `Castling`.
      * `PieceColor.cs`: Defines `White` and `Black`.
      * `SelectionStatus.cs`: Defines `Selected` and `UnSelected` states for pieces.
  * **Structs:**
      * `Coordinates.cs`: Used for board positions.

-----

### **Interfaces**

| Interface | Description |
| :--- | :--- |
| `ICommand.cs` | Standard interface for the **Command Pattern**, requiring `Execute()` and `Undo()` methods. |
| `ISelectable.cs` | Defines the behavior for objects that can be selected, requiring `OnSelect()` and `OnDeselect()`. |
| `ICapturable.cs` | (Planned) Interface for pieces that can be captured. |
| `IPromotable.cs` | (Planned) Interface for pieces that can be promoted (Pawn). |

-----

> This organized structure promotes maintainability and clarity, separating core game logic from Unity-specific behavior and leveraging design patterns like **Singleton**, **Command**, and **Decorator** for robust and scalable development.
