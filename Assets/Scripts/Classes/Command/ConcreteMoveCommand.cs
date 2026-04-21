using System;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Classes.Command
{
    public class ConcreteMoveCommand : AbstractPieceCommand
    {
        
        public ConcreteMoveCommand(IMove move) : base(move)
        {
        }
        public override void Execute(Vector2Int target)
        {
          
            _move.MovePiece(GameManager.Instance.Pieces, target); 
        }
        public override void Undo()
        {
            throw new NotImplementedException();
        }
    }
}