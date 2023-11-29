using UnityEngine;
using System.Collections;
using CS576.Janitor.Trashes;


namespace CS576.Janitor.Tools
{
    public class HandTool : BaseTool
    {
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
            if (trash.IsLightWeight)
            {
                StartCoroutine(TakeCareLightTrash(gameObj, trash, "HandGrabsLg"));
            }
            else
            {   
                if (trash.ShouldDoubleHand)
                {

                }
                else
                {
                    StartCoroutine(TakeCareSingleHandHeavyTrash(gameObj, trash, "HandGrabsHvy"));
                }
            }
        }

        private IEnumerator TakeCareLightTrash(GameObject gameObj, Trash trash, string animationName)
        {
            _janitorAnim.SetTrigger(animationName);
            yield return TakeCareTrash(1.5f, 2f, gameObj, trash, animationName, true);
        }

        private IEnumerator TakeCareSingleHandHeavyTrash(GameObject gameObj, Trash trash, string animationName)
        {
            _janitorAnim.SetTrigger(animationName);
            yield return TakeCareTrash(1f, 1.3f, gameObj, trash, animationName, false);
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

            gameObj.transform.SetParent(transform);
            gameObj.transform.localPosition = GetRelativeHoldPosition + trash.GetToolPositionAdjustment;
            gameObj.transform.localScale = new Vector3(trash.GetToolScaleAdjustment, trash.GetToolScaleAdjustment, trash.GetToolScaleAdjustment);
            gameObj.transform.localRotation = Quaternion.Euler(GetRelativeHoldRotation + trash.GetToolRotationAdjustment);

            yield return new WaitForSeconds(ccEnableTime);
            yield return null;

            if (shouldDestroy)
            {
                Destroy(gameObj);
                _onHoldingTrash = null;
                _isAnimationOnGoing = false;
            }

            _onTrashGrab.TriggerEvent(gameObj);
            UI.AudioManager.Instance.PlaySFX(UI.GameSFX.PickupTrash);

            _janitorAnim.ResetTrigger(animationName);
            _characterController.enabled = true;
        }

        public override void DestroyGrabbingTrash()
        {
            base.DestroyGrabbingTrash();
            _janitorAnim.SetTrigger("ExitHandGrabsHvy");
        }
    }
}
