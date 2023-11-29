using UnityEngine;
using System.Collections;
using CS576.Janitor.Trashes;


namespace CS576.Janitor.Tools
{
    public class TrashPicker : BaseTool
    {
        [SerializeField]
        private Animator _toolAnim;

        public override void OnSwitchToThisTool()
        {
            base.OnSwitchToThisTool();
            _janitorAnim.SetBool("IsCarryTool", true);
        }

        public override void OnSwitchToOtherTool()
        {
            base.OnSwitchToOtherTool();
            _janitorAnim.SetBool("IsCarryTool", false);
        }

        public override void Grab(GameObject gameObj)
        {
            if (_isAnimationOnGoing)
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
            if (!_janitorAnim.GetBool("IsIdle"))
                return;

            Trash trash = trashObj.GetTrash;

            _toolAnim.SetTrigger("Pick");
            _janitorAnim.SetTrigger("PickerGrab");
            _toolAnim.ResetTrigger("PickExit");
            _janitorAnim.ResetTrigger("PickerGrabExit");
            StartCoroutine(TakeCareTrash(1f, 2f, gameObj, trash, "PickerGrab", true));
        }

        protected override IEnumerator TakeCareTrash(float pickupTime, 
                                        float ccEnableTime, 
                                        GameObject gameObj, 
                                        Trash trash, 
                                        string animationName, 
                                        bool shouldDestroy)
        {
            _isAnimationOnGoing = true;
            _characterController.enabled = false;
            _onHoldingTrash = trash;
            yield return new WaitForSeconds(pickupTime);
            yield return null;

            gameObj.transform.SetParent(_trashParent.transform);
            gameObj.transform.localPosition = GetRelativeHoldPosition + trash.GetToolPositionAdjustment;
            gameObj.transform.localScale = new Vector3(trash.GetToolScaleAdjustment, 
                                                        trash.GetToolScaleAdjustment, 
                                                        trash.GetToolScaleAdjustment);
            gameObj.transform.localRotation = Quaternion.Euler(GetRelativeHoldRotation + 
                                                                trash.GetToolRotationAdjustment);

            yield return new WaitForSeconds(ccEnableTime);
            yield return null;

            Destroy(gameObj);
            _onHoldingTrash = null;
            _isAnimationOnGoing = false;

            _onTrashGrab.TriggerEvent(gameObj);
            UI.AudioManager.Instance.PlaySFX(UI.GameSFX.PickupTrash);

            _toolAnim.ResetTrigger("Pick");
            _toolAnim.SetTrigger("PickExit");
            _janitorAnim.SetTrigger("PickerGrabExit");
            _janitorAnim.ResetTrigger(animationName);
            _characterController.enabled = true;
        }
    }
}
