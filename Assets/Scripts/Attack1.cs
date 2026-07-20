using UnityEngine;
using PlayerMovementNameSpace;

public class Attack1 : StateMachineBehaviour
{
    private GameObject player;
    private PlayerMovement playerMove;
    private float originalSpeed;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMove = player.GetComponent<PlayerMovement>();
        originalSpeed = playerMove.MoveSpeed;
        Debug.Log("Original speed is: " + originalSpeed);
        playerMove.MoveSpeed = 0;
        Debug.Log("Now in lock out, speed is: " + playerMove.MoveSpeed);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerMove.MoveSpeed = originalSpeed;
        Debug.Log("Speed is now reset to: " + playerMove.MoveSpeed);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
