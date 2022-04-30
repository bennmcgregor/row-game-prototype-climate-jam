using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaucetAction : Interactable
{
    private int faucetTriggerTime = 600;
    private Animator m_animator;
    private bool hasEventOccured = false;

    protected override void Action ()
    {
        m_animator.SetBool ("IsFaucetOn", !m_animator.GetBool("IsFaucetOn"));
    }

     public void Start ()
     {
         m_animator = GetComponent<Animator>();
     }

    protected override void ChildUpdate ()
    {
        if (!hasEventOccured)
        {
            Animator m_animator = GetComponent<Animator>();
        if (m_animator.GetBool ("IsFaucetOn"))
        {
            if (faucetTriggerTime > 0)
            {
                faucetTriggerTime--;
            }
            else
            {
                GetComponent<DialogueTrigger>().TriggerDialogue();
                //m_animator.SetBool ("IsFaucetOn", false);
                hasEventOccured = true;
            }
        }
        }
    }
    
}
