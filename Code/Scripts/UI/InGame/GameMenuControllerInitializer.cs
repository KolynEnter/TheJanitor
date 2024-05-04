using UnityEngine;


/*
    Help class that initializes the 
        GameMenuController because SO
        does not have a Start()
*/
namespace CS576.Janitor.UI
{
    public class GameMenuControllerInitializer : MonoBehaviour
    {
        [SerializeField]
        private GameMenuController _menuController;

        private void Start()
        {
            _menuController.Initialize();
        }
    }
}
