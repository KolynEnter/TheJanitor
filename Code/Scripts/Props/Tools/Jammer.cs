using UnityEngine;
using CS576.Janitor.Process;


/*
    The jammer
    Used to jam spaceship's shield
    While holding jammer, cannot pick up any trash
    weight cost: 50 each
*/
namespace CS576.Janitor.Tools
{
    public class Jammer : BaseTool
    {
        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private GameObject _respawnJammerPrefab;

        [SerializeField]
        private JammerTracker _tracker;

        [SerializeField]
        private StringEvent _chatEvent;

        private Timer _respawnJammerTimer = new Timer(0.5f);

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

        private void Update()
        {
            if (!_respawnJammerTimer.IsTimeOut())
            {
                _respawnJammerTimer.ElapseTime();
                return;
            }

            if (Input.GetMouseButton(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, 
                                    out hit, 
                                    GetGrabRangeModifier, 
                                    ~(1 << LayerMask.NameToLayer("Ignore Raycast"))))
                {
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
                    {
                        if (!_tracker.HasJammer)
                        {
                            _chatEvent.TriggerEvent("You are out of jammers, dump trash to make some more.");
                            _respawnJammerTimer.Reset();
                            return;
                        }

                        Vector3 respawnJammerPosition = new Vector3(hit.point.x,
                                                                    0.36f,
                                                                    hit.point.z);
                        Debug.Log("Click on ground, place jammer on "+ respawnJammerPosition +".");

                        GameObject respawnJammer = Instantiate(_respawnJammerPrefab);
                        respawnJammer.transform.position = respawnJammerPosition;

                        respawnJammer.GetComponent<RespawnedJammer>().OnPlace();

                        _respawnJammerTimer.Reset();
                        _tracker.DecrementJammerQuantity();
                    }
                }
            }
        }
    }
}
