using UnityEngine;
using TMPro;
using CS576.Janitor.Trashes;

namespace CS576.Janitor.Prop
{
    public class TrashSlot : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _quantityComponent;

        [SerializeField]
        private GameObject _trashSlot;

        [SerializeField]
        private GameObject _pointer;

#nullable enable
        private GameObject? _storedTrashGO;

        public GameObject? GetStoredTrashGO
        {
            get 
            {
                return _storedTrashGO;    
            }
        }
#nullable disable

        public void ChangeQuantity(int newQuantity)
        {
            _quantityComponent.text = newQuantity.ToString();
        }

#nullable enable
        public void SetTrashSlot(GameObject? trashGO)
        {
            if (_trashSlot.transform.childCount > 0)
            {
                Destroy(_trashSlot.transform.GetChild(0).gameObject);
            }
            
            _storedTrashGO = trashGO;
            if (trashGO == null)
                return;
            
            GameObject _trashGO = (GameObject) trashGO;
            _trashGO.transform.SetParent(_trashSlot.transform);
            _trashGO.layer = LayerMask.NameToLayer("NotLightUI");

            Trash trash = _trashGO.GetComponent<TrashObject>().GetTrash;
            _trashGO.transform.localPosition = trash.GetItemPosition;
            _trashGO.transform.localScale = new Vector3(trash.GetItemScale, trash.GetItemScale, trash.GetItemScale);
            _trashGO.transform.localRotation = Quaternion.Euler(trash.GetItemRotation);
        }
#nullable disable   

        public void ShowPointer()
        {
            _pointer.SetActive(true);
        }

        public void HidePointer()
        {
            _pointer.SetActive(false);
        }
    }
}
