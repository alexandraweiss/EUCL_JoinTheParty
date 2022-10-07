using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    protected Animator animator;
    protected AnimatorController controller;

    private float aggression = NPCManager.START_AGGRESSION;
    public float Aggression {
        set => aggression = Mathf.Clamp01(value);
        get => aggression;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayIdle()
    {
        //Debug.Log("IDLE");
        animator.Play("npc.idle");
    }

    public void PlayDance()
    {
        //Debug.Log("DANCE");
        animator.Play("npc.dance");
    }

    public void PlayMarch()
    {
        //Debug.Log("MARCH");
        animator.Play("npc.protest");
    }
}
