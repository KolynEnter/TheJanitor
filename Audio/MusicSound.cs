using UnityEngine;


/*
    Stores a music along with its type
*/
namespace CS576.Janitor.UI
{
    [System.Serializable]
    public struct MusicSound
    {
        public GameMusic music;
        public AudioClip clip;
    }
}
