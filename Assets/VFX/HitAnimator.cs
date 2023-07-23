using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAnimator : MonoBehaviour
{
    public Animator animator;

    private void Update()
    {
        animator.speed = TimeController.speedScale;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
