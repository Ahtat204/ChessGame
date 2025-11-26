using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Classes.GameClasses
{
    /// <summary>
    /// class responsible for handling Inputs in the Main Menu scene
    /// </summary>
    internal sealed class MainMenuController : MonoBehaviour
    {
      public void Play()
        {
            SceneManager.LoadScene("GameScene");
        }

       
    }
}
