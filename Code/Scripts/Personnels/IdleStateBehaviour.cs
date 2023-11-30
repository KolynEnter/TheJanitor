using UnityEngine;


/*
    I learned a little about StateMachineBehaviour from here:
    https://docs.unity3d.com/ScriptReference/StateMachineBehaviour.html

    This script is used to update the 'IsIdle' variable when the
    player enters/exits the idle animation
*/
namespace CS576.Janitor.Character
{
    public class IdleStateBehaviour : StateMachineBehaviour
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("IsIdle", true);
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("IsIdle", false);
        }
    }
}
