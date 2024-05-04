using CS576.Janitor.Process;
using UnityEngine;


/*
    Switch the skybox for game scene in the beginning
    of the game
*/
namespace CS576.Janitor.UI
{
    public class SkyboxSwitcher : MonoBehaviour, IRequireGameSetterInitialize
    {
        [SerializeField]
        private GameObject _blueSkyBox;

        [SerializeField]
        private GameObject _purpleSkyBox;

        public void Initialize(GameSetter gameSetter)
        {
            if (gameSetter.GetGameLevel.GetMode == GameMode.Invasion)
            {
                _blueSkyBox.SetActive(false);
                _purpleSkyBox.SetActive(true);
            }
            else
            {
                _blueSkyBox.SetActive(true);
                _purpleSkyBox.SetActive(false);
            }
        }
    }
}
