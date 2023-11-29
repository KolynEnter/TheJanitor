using UnityEngine;
using UnityEngine.SceneManagement;

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
