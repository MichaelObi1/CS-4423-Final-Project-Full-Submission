using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeDeath : StateMachineBehaviour
{
    public float fadeTime = 0.8f;
    private float elapsedTime = 0f;
    SpriteRenderer sr;
    GameObject objRemove;
    Color startColor; 

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       elapsedTime = 0f;
       sr = animator.GetComponent<SpriteRenderer>();
       startColor = sr.color;
       objRemove = animator.gameObject;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        elapsedTime += Time.deltaTime;

        float newAlpha = startColor.a * (1 - (elapsedTime / fadeTime));

        sr.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

        if(elapsedTime > fadeTime)
        {
            Destroy(objRemove);
        }
    }
}
