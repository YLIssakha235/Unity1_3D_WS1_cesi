using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorAnimator : MonoBehaviour
{
    private bool m_bIsOpen;
    public Animator Animator;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !Animator.GetBool("b_OpenDoor")) return;
        Animator.SetBool("b_OpenDoor", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player") && Animator.GetBool("b_OpenDoor")) return;
        Animator.SetBool("b_OpenDoor", false);
    }
}