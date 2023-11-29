using UnityEngine;
using System.Linq;


/*
    A Singleton pattern class (in this case I think
    it is a good use).
    Plays every possible sound for this game.
*/
namespace CS576.Janitor.UI
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        public SFXSound[] sfxs;
        public MusicSound[] musics;
        public AudioSource sfxSource;
        public AudioSource musicSource;

        public float GetMusicVolume
        {
            get { return musicSource.volume; }
        }

        public float GetSFXVolume
        {
            get { return sfxSource.volume; }
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void AdjustMusicVolume(float newVolume)
        {
            musicSource.volume = newVolume;
        }

        public void AdjustSFXVolume(float newVolume)
        {
            sfxSource.volume = newVolume;
        }

        public void PlaySFX(GameSFX sfx)
        {
            SFXSound sound = sfxs.FirstOrDefault(x=>x.sfx == sfx);
            sfxSource.PlayOneShot(sound.clip);
        }

        public void PlayMusic(GameMusic music)
        {
            MusicSound sound = musics.FirstOrDefault(x=>x.music == music);
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
    }
}
