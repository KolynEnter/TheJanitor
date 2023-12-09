using CS576.Janitor.Character;
using UnityEngine;


/*
    This is the beam shot by tool jammer
    If the beam collides with spaceship's shield, deal damage to spaceship
    entity
    The amount of damage is undecided yet

    Each beam damages the ship at most once
*/
namespace CS576.Janitor.Tools
{
    public class JammerBeam : MonoBehaviour
    {
        [SerializeField]
        private GameObject _explosionFX;

        private bool _hasDamagedShip = false;

        private void OnTriggerEnter(Collider other)
        {
            if (_hasDamagedShip)
                return;
            
            if (other.gameObject.CompareTag("Spaceship"))
            {
                _hasDamagedShip = true;
                Spaceship spaceship = other.GetComponent<Spaceship>();
                if (spaceship != null)
                {
                    spaceship.TakeDamage();
                }
                else
                {
                    Debug.LogError("Spaceship is null for other");
                }

                // Finding the contact point
                // https://discussions.unity.com/t/how-to-find-the-point-of-contact-with-the-function-ontriggerenter/13338
                Vector3 contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                GameObject fx = Instantiate(_explosionFX);
                fx.transform.position = contactPoint;
            }
        }
    }
}
