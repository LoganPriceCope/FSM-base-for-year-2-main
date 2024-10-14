
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
namespace Player
{
    public class RunningState : State
    {
        // constructor
        public RunningState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            player.animator.Play("arthur_run");
        }

        public override void Exit()
        {
            player.animator.Play("arthur_stand");
        }

        public override void HandleInput()
        {
           // base.HandleInput();
        }

        public override void LogicUpdate()
        {
          //  base.LogicUpdate();
            player.CheckForIdle();
            player.CheckForJump();
         //   Debug.Log("checking for idle");
        }

        public override void PhysicsUpdate()
        {
            //  base.PhysicsUpdate();
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                player.sr.flipX = true;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                player.sr.flipX = false;
            }
        }
    }
}