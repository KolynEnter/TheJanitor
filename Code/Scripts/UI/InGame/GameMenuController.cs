using UnityEngine;
using CS576.Janitor.Process;


/*
    This class control in-game menu. 
    Notice that it does not control game result panels
    Instead, it checks if there is any game result panel shown.
    A game result panel is panel that appears in the win/lose stage
*/
namespace CS576.Janitor.UI
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Game/MenuController", order = 0)]
    public class GameMenuController : ScriptableObject
    {
        [SerializeField]
        private GameEvent _showHomePanel;
        [SerializeField]
        private GameEvent _showSoundPanel;
        [SerializeField]
        private GameEvent _showGuidePanel;
        [SerializeField]
        private GameEvent _showHelpPanel;
        [SerializeField]
        private GameEvent _hideAllPanels;

        public bool _hasPanelShown = false;
        public bool HasPanelShown
        {
            get { return _hasPanelShown; }
        }

        private GameResultPanel[] _allGameResultPanels = null;

        /*
            Checks if there is any game result panel (exists in scene)
            is shown
        */
        public bool HasAnyGameResultPanelShown
        {
            get
            {
                if (_allGameResultPanels == null)
                {
                    return false;
                }
                for (int i = 0; i < _allGameResultPanels.Length; i++)
                {
                    if (_allGameResultPanels[i] != null && _allGameResultPanels[i].IsShown)
                        return true;
                }

                return false;
            }
        }

        /*
            Since this is a scriptable object, there is no Start()
            Instead, it is intialized through another class existing in the scene
        */
        public void Initialize()
        {
            _hasPanelShown = false;
            _allGameResultPanels = FindObjectsOfType<GameResultPanel>();
        }

        /*
            Switch to the specified menu
        */
        public void Switch(MenuType menuType)
        {
            if (HasAnyGameResultPanelShown)
                return;

            _hasPanelShown = true;
            
            switch(menuType)
            {
                case MenuType.Home:
                    Activate(_showHomePanel);
                    return;
                case MenuType.Sound:
                    Activate(_showSoundPanel);
                    return;
                case MenuType.Guide:
                    Activate(_showGuidePanel);
                    return;
                case MenuType.Help:
                    Activate(_showHelpPanel);
                    return;
            }
        }

        private void Activate(GameEvent gameEvent)
        {
            gameEvent.TriggerEvent();
        }
    
        /*
            Hide all panels on the in-game scene.
            Notice that this do not applies to the game result panel
            because once a game result panel is shown, the game is ended
            so there is no need to hide it.
            In fact, hiding result panel would be considered a bug.
        */
        public void HideAllPanels()
        {
            _hasPanelShown = false;
            _hideAllPanels.TriggerEvent();
        }
    }
}
