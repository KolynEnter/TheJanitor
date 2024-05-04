using UnityEngine;


/*
    The animation for spaceship trash indicator
    code from assignement 5: Virus.cs
*/
namespace CS576.Janitor.Trashes
{
    public class SpaceshipTrashIndicatorAnimation : MonoBehaviour
    {
        private void Update()
        {
            transform.localScale = new Vector3(
                1.4f + 0.5f * Mathf.Abs(Mathf.Sin(4.0f * Time.time)),
                1.5f,
                1.4f + 0.5f * Mathf.Abs(Mathf.Sin(4.0f * Time.time))
            );
        }
    }
}
