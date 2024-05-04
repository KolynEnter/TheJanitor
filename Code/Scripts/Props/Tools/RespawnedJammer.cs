using CS576.Janitor.Process;
using UnityEngine;


/*
    The jammer that is being placed on ground
    When placed, shoots a beam to the sky.
    When the beam contacts with the spaceship,
        deal damage to the spaceship.
*/
namespace CS576.Janitor.Tools
{
    public class RespawnedJammer : MonoBehaviour
    {
        [SerializeField]
        private GameObject _beamPrefab;

        private Timer _selfDestroyTimer;

        private GameObject _beam = null;

        private void OnEnable()
        {
            _beam = null;
            _selfDestroyTimer = new Timer(10.0f);
            _selfDestroyTimer.Reset();
        }

        public void OnPlace()
        {
            _beam = Instantiate(_beamPrefab);
            _beam.transform.position = transform.position + 
                                        new Vector3(0.0f, 0.2f, 0.0f);
        }

        private void Update()
        {
            if (_beam == null)
                return;

            if (!_selfDestroyTimer.IsTimeOut())
            {
                _selfDestroyTimer.ElapseTime();
            }
            else
            {
                Destroy(_beam);
                Destroy(gameObject);
            }

            if (_beam.transform.localScale.y > 80.0f)
                return;
            
            _beam.transform.localScale += new Vector3(0.0f, 0.2f, 0.0f);
            _beam.transform.position += new Vector3(0.0f, 0.1f, 0.0f);
        }
    }
}
