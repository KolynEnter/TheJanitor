using UnityEngine;


/*
    Plays the music it is assigned to when the scene starts
*/
namespace CS576.Janitor.UI
{
    public class SceneMusicStarter : MonoBehaviour
    {
        [SerializeField]
        private GameMusic _music;

        private void Start()
        {
            AudioManager.Instance.PlayMusic(_music);
        }
    }
}
