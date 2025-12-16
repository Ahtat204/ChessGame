using UnityEngine;
using Assets.Scripts.Enums;
using Assets.Scripts.Classes.GameClasses;

namespace Assets.Scripts.Classes.PieceComponents
{
   
    /// <summary>
    /// decoupling piece selection from destination selection 
    /// </summary>
    [RequireComponent(typeof(PieceMovementComponent))]
    [RequireComponent(typeof(PieceSelectionComponent))]
    [RequireComponent(typeof(Piece))]
    class TargetSelectionComponents : MonoBehaviour
    {
        public Vector2 Target { get; private set; }
        private PieceSelectionComponent _selection;
        private PieceMovementComponent _movementComponent;
        private void Start()
        {
            _selection = GetComponent<PieceSelectionComponent>();
            _movementComponent = GetComponent<PieceMovementComponent>();
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && _selection.Status == SelectionStatus.Selected)
            {
                Target = Board.BoardInstance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
                var command=new MoveCommand(_movementComponent, Target);
                GameManager.Instance.invoker.ExecuteCommand(command);
            } 
        }
    }
}