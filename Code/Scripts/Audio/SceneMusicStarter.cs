using CS576.Janitor.Process;
using UnityEngine;


/*
    Plays the music it is assigned to when the scene starts
*/
namespace CS576.Janitor.UI
{
    public class SceneMusicStarter : MonoBehaviour, IRequireGameSetterInitialize
    {
        [SerializeField]
        private GameMusic _music;

        private bool _isPlaying = false;

        public void Initialize(GameSetter gameSetter)
        {
            if (gameSetter.GetGameLevel.GetMode == GameMode.Invasion)
            {
                AudioManager.Instance.PlayMusic(GameMusic.Invasion);
            }
            else
            {
                AudioManager.Instance.PlayMusic(_music);
            }

            _isPlaying = true;
        }

        private void Start()
        {
            if (!_isPlaying)
                AudioManager.Instance.PlayMusic(_music);
        }
    }
}
