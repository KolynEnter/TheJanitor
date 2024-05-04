using TMPro;
using UnityEngine;
using UnityEngine.UI;


/*
    Managed the UI part of the weight & 
    capacity label.
*/
namespace CS576.Janitor.UI
{
    public class CapacityUIManager : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _capacityText;

        [SerializeField]
        private Image _capacityFillImage;

        public void UpdateUI(float weight, float capacity)
        {
            _capacityText.text = "Capacity: " + weight + "/" + capacity;
            _capacityFillImage.fillAmount = weight / capacity;
        }
    }
}
