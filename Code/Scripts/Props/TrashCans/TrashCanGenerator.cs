using CS576.Janitor.Process;
using UnityEngine;
using System.Linq;

namespace CS576.Janitor.Trashes
{

    public class TrashCanGenerator : MonoBehaviour, IRequireGameSetterInitialize
    {
        [SerializeField]
        private GameObject _landfillCanPrefab;

        [SerializeField]
        private GameObject _paperCanPrefab;

        [SerializeField]
        private GameObject _canTrashCanPrefab;

        [SerializeField]
        private GameObject _plasticCanPrefab;

#nullable enable
        private GameObject? FindCanPrefab(TrashType trashType)
        {
            switch (trashType)
            {
                case TrashType.Paper:
                    return _paperCanPrefab;
                case TrashType.Cans:
                    return _canTrashCanPrefab;
                case TrashType.Plastic:
                    return _plasticCanPrefab;
                case TrashType.Landfill:
                    return _landfillCanPrefab;
                default:
                    return null;
            }
        }
#nullable disable

        public void Initialize(GameSetter gameSetter)
        {
            TrashCanWithChance[] trashCanChance = gameSetter.GetGameLevel.GetTrashCanChance;
            WaypointGO[] allTrashCanWaypoints = FindObjectsOfType<WaypointGO>();
            allTrashCanWaypoints = allTrashCanWaypoints
                                        .Where(x=>x.GetDedication == WaypointDedication.TrashCan).ToArray();
            

            foreach (WaypointGO trashCanWaypoint in allTrashCanWaypoints)
            {
                ArrayShuffler.Shuffle(trashCanChance);
                for (int i = 0; i < trashCanChance.Length; i++)
                {
                    float chance = Random.Range(0, 100);
                    if (chance < trashCanChance[i].chance || 
                        i == trashCanChance.Length-1)
                    {
#nullable enable
                        // generate this type of can in this waypoint spot
                        GameObject? optionalCanPrefab = FindCanPrefab(trashCanChance[i].trashType);
#nullable disable
                        if (optionalCanPrefab != null)
                        {
                            GameObject canPrefab = (GameObject) optionalCanPrefab;
                            GameObject newCan = Instantiate(canPrefab);
                            newCan.transform.position = new Vector3(trashCanWaypoint.transform.position.x, 
                                                                    0.0f, 
                                                                    trashCanWaypoint.transform.position.z);
                            newCan.transform.rotation = trashCanWaypoint.transform.rotation;
                        }
                        break;
                    }
                }
            }
        }
    }
}
