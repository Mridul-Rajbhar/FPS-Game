using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    #region Private
    [SerializeField]
    Animator animator;
    #endregion

    #region Public
    #endregion


    public void Attack(bool attk)
    {
        if(animator.parameterCount>0)
            animator.SetBool("attack",attk);
    }
    public void trigFire()
    {
        //Debug.Log("Called");
        animator.SetTrigger("Fire");
    }

    public void holdFire(bool fire)
    {
        animator.SetBool("HoldFire",fire);
    }

}
