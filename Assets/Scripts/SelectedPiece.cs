using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// this class is used to as a detector for clicking on a piece 
    /// </summary>
    public sealed class SelectedPiece : MonoBehaviour
    {
        public static SelectedPiece Instance;
        public GameObject SelectedPieceObject { get;set; }
        private void Awake()
        {
            if (Instance && !Equals(Instance, this))
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void SetSelectedGameObject(GameObject selectedObject)
        {
            SelectedPieceObject = selectedObject;
        }

        public void ResetSelectedGameObject()
        {
            SelectedPieceObject = null;
        }
    }
}