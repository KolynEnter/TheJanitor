using UnityEngine;


/*
    Plays SFX sound for this game
    This class enables the sound to be embeded into OnClick() of buttons
*/
namespace CS576.Janitor.UI
{
    public class SFXPlayer : MonoBehaviour
    {
        public void PlayButtonClickSFX()
        {
            PlaySFX(GameSFX.ButtonClick);
        }

        public void PlayDumpTrashSFX()
        {
            PlaySFX(GameSFX.DumpTrash);
        }

        public void PlayRunningSFX()
        {
            PlaySFX(GameSFX.Running);
        }

        public void PlayTrashPickupSFX()
        {
            PlaySFX(GameSFX.PickupTrash);
        }

        public void PlayVaccumStartSFX()
        {
            PlaySFX(GameSFX.VaccumStart);
        }

        public void PlayVaccumSuckingSFX()
        {
            PlaySFX(GameSFX.VaccumSucking);
        }

        public void PlayVaccumEndSFX()
        {
            PlaySFX(GameSFX.VaccumEnd);
        }

        private void PlaySFX(GameSFX sfx)
        {
            AudioManager.Instance.PlaySFX(sfx);
        }
    }
}
