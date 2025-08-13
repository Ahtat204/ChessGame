using Assets.Scripts.Enums;
using Assets.Scripts.Structs;
using UnityEngine;

namespace Assets.Scripts.Classes.GameClasses
{
    public class GameManager:MonoBehaviour
    {
        private GameState _gameState;
        private MoveType _moveType;
        private Turn _turn;
        private Coordinates _coordinates;

      private void  Awake()
        {
            // Initialize game state, move type, and turn
            _turn = new Turn();
            _moveType = MoveType.Normal;
          _gameState=new GameState();
          
          
          // Additional initialization logic can go here
        }
    }
}