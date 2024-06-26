using UnityEngine;
using System.Collections;
using CS576.Janitor.Trashes;
using CS576.Janitor.Process;


/*
    The base class for every tool in this game
        -> Tool used to pick up trash
*/
namespace CS576.Janitor.Tools
{
    public abstract class BaseTool : MonoBehaviour, ITool
    {
        [SerializeField]
        private ToolStat _toolStat;

        [SerializeField]
        protected CharacterController _characterController;

        protected Vector3 GetRelativeHoldPosition
        {
            get { return _toolStat.GetRelativeHoldPosition; }
        }

        protected Vector3 GetRelativeHoldRotation
        {
            get { return _toolStat.GetRelativeHoldRotation; }
        }

        [SerializeField]
        protected Animator _janitorAnim;

        [SerializeField]
        protected GOEvent _onTrashGrab;

        [SerializeField]
        protected GameObject _trashParent;

        [SerializeField]
        private GameEvent _onPickupSpaceshipTrash;

#nullable enable
        protected Trash? _onHoldingTrash;
        public Trash? GetOnHoldingTrash
        {
            get
            {
                return _onHoldingTrash;
            }
        }
#nullable disable

        public ToolType GetToolType
        {
            get { return _toolStat.GetToolType; }
        }

        public int GetToolWeight
        {
            get { return _toolStat.GetToolWeight; }
        }

        public float GetGrabRangeModifier
        {
            get { return _toolStat.GetGrabRangeModifier; }
        }

        protected bool _isAnimationOnGoing;

        public virtual void OnSwitchToThisTool()
        {
            
        }

        public virtual void OnSwitchToOtherTool()
        {
            _janitorAnim.Play("Idle");
        }

        public virtual void Grab(GameObject gameObj)
        {

        }

        protected void HideSpaceshipIndicator(TrashObject trashObj)
        {
            // Hide spaceship indicator
            if (trashObj.transform.childCount > 0)
            {
                Transform trashChildTransform = trashObj.transform.GetChild(0);
                trashChildTransform.gameObject.SetActive(false);

                // trigger picking up spaceship trash event
                _onPickupSpaceshipTrash.TriggerEvent();
            }
        }

        protected virtual void PlayGrabbing(GameObject gameObj, TrashObject trashObj)
        {

        }

        protected virtual IEnumerator TakeCareTrash(float pickupTime, 
                                        float ccEnableTime, 
                                        GameObject gameObj, 
                                        Trash trash, 
                                        string animationName, 
                                        bool shouldDestroy)
        {
            yield return null;
        }

        public virtual void DestroyGrabbingTrash()
        {
            Destroy(_trashParent.transform.GetChild(0).gameObject);
            _onHoldingTrash = null;
            _isAnimationOnGoing = false;
        }

        public bool CanWorkOnTrash(TrashObject trashObj)
        {
            return CanWorkOnTrash(trashObj.GetTrash);
        }

        public bool CanWorkOnTrash(Trash trash)
        {
            foreach(TrashTypeWithWeight ttww in _toolStat.GetTargetignTrashTypes)
            {
                if (trash != null && ttww.trashType == trash.GetTrashType)
                {
                    if (ttww.lightOnly == trash.IsLightWeight || !ttww.lightOnly)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
