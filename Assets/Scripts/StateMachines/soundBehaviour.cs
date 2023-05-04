using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundBehaviour : StateMachineBehaviour
{
    public AudioClip soundToPlay;
    public float vol = 1f;
    public bool playOnEnter = true, playOnExit = false, playOnDelay = false;

    //Delay timer
    public float playDelay = 0.25f;
    private float timeEntered = 0;
    private bool delayHasPlayed = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnEnter)
        {
            AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, vol);
        }
        
        timeEntered = 0;
        delayHasPlayed = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnDelay && !delayHasPlayed)
        {
            timeEntered += Time.deltaTime;

            if (timeEntered > playDelay)
            {
                AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, vol);
                delayHasPlayed = true;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnExit)
        {
            AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, vol);
        }
    }
}
