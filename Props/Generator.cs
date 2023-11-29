using UnityEngine;

namespace CS576.Janitor.Prop
{
    public class Generator : MonoBehaviour
    {
        public virtual void Generate()
        {
            
        }

        public Vector3 GetRandomGeneratePosition
        {
            get
            {
                BoxCollider collider = GetComponent<BoxCollider>();
                Vector3 size = collider.size;
                Vector3 pos = this.transform.position;

                return new Vector3(
                    pos.x + Random.Range(-size.x, 0),
                    pos.y + 0.05f,
                    pos.z + Random.Range(-size.z, 0)
                );
            }
        }

        public Transform GetRootParentTransform
        {
            get { return this.transform.parent.parent.parent; }
        }

        public Quaternion GetRandomRotationWith(RotationPreference preference)
        {
            Vector3 randomRotationVector = new Vector3
                                            (
                                                Random.Range(0, 360),
                                                Random.Range(0, 360),
                                                Random.Range(0, 360)
                                            );
            switch (preference)
            {
                case RotationPreference.ExcludeX:
                    randomRotationVector.x = 0;
                    break;
                case RotationPreference.ExcludeY:
                    randomRotationVector.y = 0;
                    break;
                case RotationPreference.ExcludeZ:
                    randomRotationVector.z = 0;
                    break;
                case RotationPreference.OnlyX:
                    randomRotationVector.y = 0;
                    randomRotationVector.z = 0;
                    break;
                case RotationPreference.OnlyY:
                    randomRotationVector.x = 0;
                    randomRotationVector.z = 0;
                    break;
                case RotationPreference.OnlyZ:
                    randomRotationVector.x = 0;
                    randomRotationVector.y = 0;
                    break;
                case RotationPreference.PreventTopDown:
                    if (randomRotationVector.x < -90)
                    {
                        randomRotationVector.x = -90;
                    }
                    if (randomRotationVector.x > 90)
                    {
                        randomRotationVector.x = 90;
                    }
                    if (randomRotationVector.z < -90)
                    {
                        randomRotationVector.z = -90;
                    }
                    if (randomRotationVector.z > 90)
                    {
                        randomRotationVector.z = 90;
                    }
                    break;
                case RotationPreference.Allway:
                    break;
            }

            return Quaternion.Euler(randomRotationVector);
        }
    }
}
