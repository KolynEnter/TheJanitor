using UnityEngine;
using UnityEngine.SceneManagement;


/*
    Load game scene from main scene
*/
namespace CS576.Janitor.Process
{
    public class StageSceneLoader : MonoBehaviour
    {
        public void LoadHarborview()
        {
            SceneManager.LoadScene("Harborview");
        }
    }
}
