using UnityEngine;
using TMPro;


/*
    The representation form of the Chat
    (What appears to the players)
*/
namespace CS576.Janitor.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class Message : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _textComponent;
        public TextMeshProUGUI TextComponent
        {
            get { return _textComponent; }
        }
    }
}
