using CS576.Janitor.Process;
using UnityEngine;
using System.Collections.Generic;


/*
    The Behavior tree for NPC
*/
namespace CS576.Janitor.NPC
{
    public class People : MonoBehaviour, IRequireGameSetterInitialize
    {
        public enum Personality
        {
            GoodGuy,
            BadGuy,
            NormalGuy
        }

        public enum Occupation
        {
            Pedestrian,
            Businessman,
            Police,
            Jogger
        }

        public enum Move
        {
            Idle,
            Walk,
            Run
        }

        [SerializeField]
        private Personality _personality;
        [SerializeField]
        private Occupation _occupation;
        public Occupation GetOccupation
        {
            get { return _occupation; }
        }

        public Move currentMove = Move.Idle; // only for debug purpose, not used anywhere
        private BehaviorTree _tree = null;

        private void Update()
        {
            if (_tree != null)
            {
                _tree.Update();
            }
        }

        public void Initialize(GameSetter gameSetter)
        {
            _tree = ScriptableObject.CreateInstance<BehaviorTree>();

            RepeatNode rootNode = ScriptableObject.CreateInstance<RepeatNode>();
            _tree.rootNode = rootNode;

            if (gameSetter.GetGameLevel.GetMode == GameMode.Invasion)
            {
                WanderNode scareRunNode = ScriptableObject.CreateInstance<WanderNode>();
                scareRunNode.assignedSpeed = 5.0f;
                scareRunNode.wanderRadius = 100f;
                scareRunNode.npcTransform = transform;
                rootNode.child = scareRunNode;

                return;
            }

            SelectorNode firstNode = ScriptableObject.CreateInstance<SelectorNode>();
            firstNode.nodeChances = new List<float>();
            rootNode.child = firstNode;

            WanderNode slowWalkNode = GetSlowWalkNode();
            AssignWalkNode(firstNode, slowWalkNode, gameSetter);
            AssignRunNode(firstNode, slowWalkNode);
            AssignStopNode(firstNode, slowWalkNode);
        }

        private void AssignWalkNode(SelectorNode firstNode, WanderNode slowWalkNode, GameSetter gameSetter)
        {
            SequencerNode walkOnStreetNode = ScriptableObject.CreateInstance<SequencerNode>();
            walkOnStreetNode.children.Add(slowWalkNode);
            firstNode.children.Add(walkOnStreetNode);
            firstNode.nodeChances.Add(50f);

            DropTrashNode dropTrashNode = GetDropTrashNode(gameSetter);
            if (dropTrashNode != null)
                walkOnStreetNode.children.Add(dropTrashNode);

            GrabTrashNode grabTrashNode = GetGrabTrashNode();
            if (grabTrashNode != null)
                walkOnStreetNode.children.Add(grabTrashNode);
        }

        private void AssignRunNode(SelectorNode firstNode, WanderNode slowWalkNode)
        {
            SequencerNode runNode;
            if (_occupation == Occupation.Jogger)
            {
                runNode = ScriptableObject.CreateInstance<SequencerNode>();
                firstNode.children.Add(runNode);
                firstNode.nodeChances.Add(80f);
            }
            else
            {
                runNode = ScriptableObject.CreateInstance<SequencerNode>();
                firstNode.children.Add(runNode);
                firstNode.nodeChances.Add(10f);
            }

            WanderNode slowRunNode = ScriptableObject.CreateInstance<WanderNode>();
            slowRunNode.assignedSpeed = 4.0f;
            slowRunNode.wanderRadius = 40f;
            slowRunNode.npcTransform = transform;
            runNode.children.Add(slowRunNode);

            /*
                After running has ended, choose between run & walk
                Sudden stop is bad for your health
            */
            if (_occupation == Occupation.Jogger)
            {
                SelectorNode runCheckNode = ScriptableObject.CreateInstance<SelectorNode>();
                runCheckNode.nodeChances = new List<float>();
                runCheckNode.children.Add(slowRunNode);
                runCheckNode.nodeChances.Add(80f);
                runCheckNode.children.Add(slowWalkNode);
                runCheckNode.nodeChances.Add(20f);
                runNode.children.Add(runCheckNode);
            }
            else
            {
                SelectorNode runCheckNode = ScriptableObject.CreateInstance<SelectorNode>();
                runCheckNode.nodeChances = new List<float>();
                runCheckNode.children.Add(slowRunNode);
                runCheckNode.nodeChances.Add(40f);
                runCheckNode.children.Add(slowWalkNode);
                runCheckNode.nodeChances.Add(60f);
                runNode.children.Add(runCheckNode);
            }
        }

        private void AssignStopNode(SelectorNode firstNode, WanderNode slowWalkNode)
        {
            SequencerNode stopNode = ScriptableObject.CreateInstance<SequencerNode>();
            firstNode.children.Add(stopNode);
            firstNode.nodeChances.Add(30f);

            IdleNode idleNode = ScriptableObject.CreateInstance<IdleNode>();
            idleNode.assignedIdleTime = 3.0f;
            idleNode.npcTransform = transform;
            stopNode.children.Add(idleNode);
            stopNode.children.Add(slowWalkNode);
        }

        private WanderNode GetSlowWalkNode()
        {
            WanderNode slowWalkNode = ScriptableObject.CreateInstance<WanderNode>();
            slowWalkNode.assignedSpeed = 1.0f;
            slowWalkNode.wanderRadius = 20f;
            slowWalkNode.npcTransform = transform;
            
            return slowWalkNode;
        }

        private DropTrashNode GetDropTrashNode(GameSetter gameSetter)
        {
            DropTrashNode dropTrashNode;
            if (_personality == Personality.BadGuy)
            {
                dropTrashNode = ScriptableObject.CreateInstance<DropTrashNode>();
                dropTrashNode.alertDistance = 20f;
            }
            else if (_occupation == Occupation.Businessman)
            {
                dropTrashNode = ScriptableObject.CreateInstance<DropTrashNode>();
                dropTrashNode.alertDistance = 10f;
            }
            else
            {
                return null;
            }
            dropTrashNode.npcTransform = transform;
            dropTrashNode.modifiedTrashGenerateRate = 
                gameSetter.GetGameLevel.GetModifiedTrashGenerateRate;
            
            for (int i = 0; i < dropTrashNode.modifiedTrashGenerateRate.Length; i++)
            {
                dropTrashNode.modifiedTrashGenerateRate[i].generateRate *= 10;
            }

            return dropTrashNode;
        }
    
        private GrabTrashNode GetGrabTrashNode()
        {
            if (_personality == Personality.GoodGuy || 
                _occupation == Occupation.Police)
            {
                GrabTrashNode grabTrashNode = ScriptableObject.CreateInstance<GrabTrashNode>();
                grabTrashNode.npcTransform = transform;
                grabTrashNode.searchRadius = 10f;
                
                return grabTrashNode;
            }

            return null;
        }
    }
}
