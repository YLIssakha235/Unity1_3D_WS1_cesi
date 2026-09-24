using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public Transform Pivot;
    private bool m_bIsOpen;

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
        //if the GameObject is not the player and the door is not open
        if (!other.CompareTag("Player") && !m_bIsOpen)
            return;
        //if the GameObject is the player and the door is not open
        if (other.CompareTag("Player") && !m_bIsOpen)
        {
            Pivot.Rotate(new Vector3(0, 90, 0));
            m_bIsOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if the GameObject is not the player and the door is open
        if (!other.CompareTag("Player") && m_bIsOpen)
            return;
        //if the GameObject is the player and the door is not open
        if (other.CompareTag("Player") && m_bIsOpen)
        {
            Pivot.Rotate(new Vector3(0, -90, 0));
            m_bIsOpen = false;
        }
    }

}