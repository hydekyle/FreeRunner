using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Animator animator;

    private void OnEnable ()
    {
        animator = animator ?? GetComponent<Animator> ();
    }

    public void TriggerAnimation (string triggerName)
    {
        animator.SetTrigger (triggerName);
    }
}
