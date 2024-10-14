
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
namespace Player
{
    public class JumpingState : State
    {
        // constructor
        public JumpingState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            player.rb.AddForce(player.transform.up * 20f);
            player.animator.Play("jump_anim");
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
                player.CheckForLanding();

            
            //base.LogicUpdate();
            //player.CheckForIsLanded();
          //  Debug.Log("checking for land");
        }

        public override void PhysicsUpdate()
        {
          //  player.transform.position = new Vector2(0.23f, 2.64f);
        }
    }
}