using UnityEngine;

namespace CS576.Janitor.Process
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Variables/GameConfig", order = 3)]
    public class GameConfiguration : ScriptableObject
    {
        public GameMode gameMode;

        public GameDifficulty gameDifficulty;

        [SerializeField]
        private ToolType[] _additionalToolTypes;
        public ToolType Tool1
        {
            get { return _additionalToolTypes[0]; }
            set { _additionalToolTypes[0] = value; }
        }
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
