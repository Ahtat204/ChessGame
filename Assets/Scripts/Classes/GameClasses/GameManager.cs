using System;
using Assets.Scripts.Enums;
using Assets.Scripts.Structs;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Classes.GameClasses
{
    public class GameManager:MonoBehaviour
    {

        [SerializeField]private List<GameObject> pieces;
        private GameState _gameState;
        private MoveType _moveType;
        private Turn _turn;
        private Coordinates _coordinates;


        private void Start()
        {
            pieces = new (32);
        }
    }
}