using System;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Classes.Command
{
    public class MoveCommand : PieceCommand
    {
        public MoveCommand(IMove move) : base(move)
        {
         // for testing 
        }

        public override void Execute()
        {
            _move.MovePiece(GameManager.Instance.Pieces, new Vector2(3.06f, 3.2f)); 
        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }
    }
}