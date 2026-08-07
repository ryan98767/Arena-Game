using UnityEngine;
using PlayerMovementNameSpace;


namespace PlayerState
{   
    public enum States
    {
        Normal,
        Blocking,
        InDialogue
    }

    public class PlayerStates : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMove;
        [SerializeField] private PlayerAttack playerAttack;

        private States currentState = States.Normal;

        public States CurrentState => currentState;

        public void SetState(States newState)
        {
            currentState = newState;

            Debug.Log($"SetState called: {newState}, playerMove null? {playerMove == null}, playerMove instance ID: {playerMove?.GetInstanceID()}");

            switch (newState)
            {
                case States.Normal:
                    playerMove.enabled = true;
                    playerAttack.enabled = true;
                    break;
                case States.Blocking:
                    playerMove.enabled = false;
                    playerAttack.enabled = true;
                    break;
                case States.InDialogue:
                    playerMove.enabled = false;
                    playerAttack.enabled = false;
                    break;
            }
        }
    }
}
