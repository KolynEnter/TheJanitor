using UnityEngine;


/*
    A SO used to store game mode configurations.
    It is then passed into the game scene (i.e. Harborview) 
    and its stored value will be used from there.
*/
namespace CS576.Janitor.Process
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Variables/GameConfig", order = 3)]
    public class GameConfiguration : ScriptableObject
    {
        public GameMode gameMode;

        public GameDifficulty gameDifficulty;

        // The two (or one, if invasion) tools chose by the player
        [SerializeField]
        private ToolType[] _additionalToolTypes;

        // The first tool chose by the player
        public ToolType Tool1
        {
            get { return _additionalToolTypes[0]; }
            set { _additionalToolTypes[0] = value; }
        }

        // The second tool chose by the player
        // If invasion mode, the tool2 button will be hidden
        // and player will definitely have jammer as his second tool
        public ToolType Tool2
        {
            get 
            {
                if (gameMode == GameMode.Invasion)
                {
                    return ToolType.Jammer;
                }
                else
                {
                    return _additionalToolTypes[1];
                }
            }
            set { _additionalToolTypes[1] = value; }
        }

        public void Defaultize()
        {
            gameMode = GameMode.Classic;
            gameDifficulty = GameDifficulty.Normal;
            _additionalToolTypes = new ToolType[2];
            _additionalToolTypes[0] = ToolType.Picker;
            _additionalToolTypes[1] = ToolType.Popper;
        }
    }
}
