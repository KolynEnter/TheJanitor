using UnityEngine;
using CS576.Janitor.Trashes;
using CS576.Janitor.Tools;


/*
    Controls the player input from mouse
    
    Excute code from other places when the player clicked on something
    i.e. If the player click on a trash,
    it will determine the current holding tool and
    then execute actions based on that
*/
namespace CS576.Janitor.Character
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private HoldingTool _holdingTool;

        // Current cursor pointing to the trash slot
        [SerializeField]
        private Process.IntVariable _currentTrashSlotIndex;

        [SerializeField]
        private Prop.TrashBag _trashBag;

        [SerializeField]
        private Process.StringEvent _chatEvent;

        private LongPressDumpDeterminer _lpDeterminer = new LongPressDumpDeterminer();

        [SerializeField]
        private Tools.TrashPopper _popper;

        private void Start()
        {
            Time.fixedDeltaTime = 0.5f;
        }
        
        // Decide if need to rise popper gun
        private void FixedUpdate()
        {
            if (_popper.gameObject.activeSelf)
            {
                // if left mouse pressed
                if (Input.GetMouseButton(0))
                {
                    Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    // don't rise popper if clicked on a trashcan
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.transform.gameObject.CompareTag("TrashCan"))
                        {
                            return;
                        }
                    }
                    _popper.RisePopper();
                }
                else
                {
                    _popper.PutDownPopper();
                }
            }
        }
        
        // Check for mouse inputs
        private void Update()
        {
            if (!Input.GetMouseButton(0) && !Input.GetMouseButtonUp(0))
                return;
            
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (Input.GetMouseButtonUp(0))
                {
                    // If it's Popper, don't do grab here
                    // Instead, use the Popper grab area in janitor
                    if (_holdingTool.GetTool.GetToolType != Process.ToolType.Popper)
                    {
                        ShootGrabRay(hit);
                    }
                }
                // Trigger continuous trash dumping
                if (_lpDeterminer.IsLongPressDumpOK())
                {
                    ShootTrashCanRay(hit);
                }
            }
        }

        private void ShootGrabRay(RaycastHit hit)
        {
            GameObject hitObject = hit.transform.gameObject;

            if (hitObject == null)
                return;
                
            TrashObject trashObj = hitObject.GetComponent<TrashObject>();
            if (trashObj == null)
                return;

            if (hit.distance > trashObj.GetTrash.GetGrabbingRadius + 
                                (_holdingTool.GetTool as BaseTool).GetGrabRangeModifier)
                return;
            
            _holdingTool.GetTool.Grab(hitObject);
        }

        private void ShootTrashCanRay(RaycastHit hit)
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject == null)
                return;

            TrashCan can = hitObject.GetComponent<TrashCan>();
            if (can == null)
                return;
            
            if (hit.distance > 3f)
                return;
            
            can.ReceiveTrash(_trashBag.GetTrash(_currentTrashSlotIndex.value), 
                            _trashBag.GetTrashGO(_currentTrashSlotIndex.value));
        }
    }
}
