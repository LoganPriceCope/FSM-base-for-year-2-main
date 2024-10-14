using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.VFX;

namespace Player
{


    public class PlayerScript : MonoBehaviour
    {
        public Rigidbody2D rb;
        public BoxCollider2D bc;

        public bool isTouchingGround;
        public bool facingDirection; // true = Left, false = right

        // variables holding the different player states
        public IdleState idleState;
        public RunningState runningState;
        public JumpingState jumpingState;
        public SpriteRenderer sr;

        public StateMachine sm;
        public Animator animator;



        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            bc = GetComponent<BoxCollider2D>();
            sr = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            
            sm = gameObject.AddComponent<StateMachine>();

            // add new states here
            idleState = new IdleState(this, sm);
            runningState = new RunningState(this, sm);
            jumpingState = new JumpingState(this, sm);

            // initialise the statemachine with the default state
            sm.Init(idleState);
        }

        // Update is called once per frame
        public void Update()
        {
            sm.CurrentState.LogicUpdate();

            //output debug info to the canvas
            string s;
            s = string.Format("last state={0}\ncurrent state={1}", sm.LastState, sm.CurrentState);

            UIscript.ui.DrawText(s);

            UIscript.ui.DrawText("Press I for idle / R for run");

        }



        void FixedUpdate()
        {
            sm.CurrentState.PhysicsUpdate();
        }



        public void CheckForRun()
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) // key held down
            {
                sm.ChangeState(runningState);
                return;
            }
        }


        public void CheckForIdle()
        {
            if (Input.GetKey(KeyCode.LeftArrow)==false && Input.GetKey(KeyCode.RightArrow)==false && isTouchingGround == true) // key held down
            {
                sm.ChangeState(idleState);
            }

        }

        public void CheckForJump()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                sm.ChangeState(jumpingState);
            }
        }

        public void CheckForLanding()
        {
            if (isTouchingGround == true)
            {
                sm.ChangeState(idleState);
            }
        }

        public void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Ground")
            {
                isTouchingGround = false;
            }
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log(collision.gameObject.tag);
            if (collision.gameObject.tag == "Ground")
            {
                isTouchingGround = true;
            }
        }
    }
}