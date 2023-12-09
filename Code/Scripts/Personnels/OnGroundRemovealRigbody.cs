using UnityEngine;


/*
    Removes the rigidbody in spaceship trash
    When done, destroy this class as well

    However, there are 5 possible things to collide with
        1. The ground
        2. Other trash
        3. The player
        4. The spaceship
        5. Any other things
    
    case 1 is desired, when contact ground, remove the rigidbody
        and this class
    case 2 happens quite frequently, but does not influence
        the result if reach ground at the end
    case 3 is like case 2
    case 4 is like case 2
    case 5 is undesired. This means that trash has never reached
        the ground. It could be stuck on a tree, building...
*/
namespace CS576.Janitor.Process
{
    public class OnGroundRemovalRigidbody: MonoBehaviour
    {
        public GoalManager goalManager;

        private bool _hasCollideToBadThing = false;

        private void OnTriggerEnter(Collider other)
        {
            if (_hasCollideToBadThing)
                return;
            
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                Rigidbody rigidbody = transform.parent.GetComponent<Rigidbody>();
                if (rigidbody != null)
                {
                    // Remove the rigidbody
                    Transform rigbodyTransform = rigidbody.transform;
                    Destroy(rigidbody);

                    rigbodyTransform.position = new Vector3(rigbodyTransform.position.x, 
                                                                0.05f,
                                                                rigbodyTransform.position.z);

                    // Remove this class from trash
                    Destroy(gameObject);
                }
            } 
            else if (other.gameObject.layer != LayerMask.NameToLayer("Trash") &&
                !other.gameObject.CompareTag("Player")&&
                !other.gameObject.CompareTag("Spaceship"))
            {
                // case 4, destroy this trash
                _hasCollideToBadThing = true;
                
                // regenerate city hp before destroy
                Debug.Log("Trash generated in a bad location, regenerate city hp and destroy trash.");
                goalManager.RegenerateCityHPWithAttackPower();
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
