using UnityEngine;


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
