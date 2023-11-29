using UnityEngine;


/*
    Stores SFX sound along with its type
*/
namespace CS576.Janitor.UI
{
    [System.Serializable]
    public struct SFXSound
    {
        public GameSFX sfx;
        public AudioClip clip;
    }
}
