using CS576.Janitor.Process;
using UnityEngine;


/*
    Spawn NPCs in the beginning of the game
*/
namespace CS576.Janitor.NPC
{
    public class NPCPortal : MonoBehaviour, IRequireGameSetterInitialize
    {
        [SerializeField]
        private GameObject[] _npcs;

        public void Initialize(GameSetter gameSetter)
        {
            foreach (GameObject npc in _npcs)
            {
                People people = npc.GetComponent<People>();
                if (people == null)
                    throw new System.Exception(npc + " lacks People component!");

                GameObject npcCopy = Instantiate(npc);
                npcCopy.transform.localPosition = transform.position;
                npcCopy.transform.SetParent(transform);
                npcCopy.GetComponent<People>().Initialize(gameSetter);
            }
        }
    }
}
