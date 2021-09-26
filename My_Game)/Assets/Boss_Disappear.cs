using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Disappear : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Destroyd");
        Destroy(animator.gameObject);
    }

  
}
