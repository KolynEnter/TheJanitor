using UnityEngine;
using System.Collections;
using CS576.Janitor.Trashes;


namespace CS576.Janitor.Tools
{
    public class TrashPopper : BaseTool
    {
        [SerializeField]
        private PopperArea _area;

        public override void OnSwitchToThisTool()
        {
            base.OnSwitchToThisTool();
            _area.enabled = true;
            _janitorAnim.SetBool("IsHoldingPopper", true);
        }

        public override void OnSwitchToOtherTool()
        {
            base.OnSwitchToOtherTool();
            _area.enabled = false;
            _janitorAnim.SetBool("IsHoldingPopper", false);
        }

        public override void Grab(GameObject gameObj)
        {
            if (!_janitorAnim.GetBool("IsUsingPopper"))
                return;

            TrashObject trashObj = gameObj.GetComponent<TrashObject>();
            if (trashObj == null)
                return;

            if (CanWorkOnTrash(trashObj))
            {
                PlayGrabbing(gameObj, trashObj);
            }
        }

        protected override void PlayGrabbing(GameObject gameObj, TrashObject trashObj)
        {
            Trash trash = trashObj.GetTrash;
            
            StartCoroutine(TakeCareTrash(0f, 0.3f, gameObj, trash, "", true));
        }

        protected override IEnumerator TakeCareTrash(float pickupTime, 
                                        float ccEnableTime, 
                                        GameObject gameObj, 
                                        Trash trash, 
                                        string animationName, 
                                        bool shouldDestroy)
        {
            _onHoldingTrash = trash;
            gameObj.transform.SetParent(_trashParent.transform);
            gameObj.transform.localPosition = GetRelativeHoldPosition + trash.GetToolPositionAdjustment;
            gameObj.transform.localScale = new Vector3(trash.GetToolScaleAdjustment, 
                                                        trash.GetToolScaleAdjustment, 
                                                        trash.GetToolScaleAdjustment);
            gameObj.transform.localRotation = Quaternion.Euler(GetRelativeHoldRotation + 
                                                                trash.GetToolRotationAdjustment);

            yield return null;
            yield return new WaitForSeconds(ccEnableTime);

            Destroy(gameObj);
            _onHoldingTrash = null;
            _onTrashGrab.TriggerEvent(gameObj);
            UI.AudioManager.Instance.PlaySFX(UI.GameSFX.PickupTrash);
        }

        public void RisePopper()
        {
            _janitorAnim.SetBool("IsUsingPopper", true);
            _janitorAnim.SetBool("IsHoldingPopper", true);
        }

        public void PutDownPopper()
        {
            _janitorAnim.SetBool("IsUsingPopper", false);
        }
    }
}
