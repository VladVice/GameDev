using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    public LayerMask ground; // lai detektotu mouseClick uz zemes
    public bool isDisabled = false;

    private NavMeshAgent agent;
    bool gameOver;

    Animator anim;
    NPC npc;
    

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        npc = GetComponent<NPC>();
        anim = GetComponent<Animator>();
    }

    public void IsPlayerDisabled() // Game over state
    {
        agent.ResetPath(); // Clear agent current path
        anim.SetBool("isDisabled", true); // Stop anims
        Debug.Log("Disabling Player"); // Debug IsPlayerDisabled state
        isDisabled = true; //  Maybe in future add something more to this state
        gameOver = true; // Game over state
    }

    private void Update()
    {
        if (isDisabled == false)
        {
            if (Input.GetMouseButtonDown(0)) // Primary mouse button
            {
                Ray rayTarget = Camera.main.ScreenPointToRay(Input.mousePosition); // panemsim poziciju no vietas kur uzpiez ar peli.
                RaycastHit hitInfo;

                if (Physics.Raycast(rayTarget, out hitInfo, 100, ground)) // (Kur ir uzspiests, info no reycast, max distance, zemes layer)
                {
                    agent.SetDestination(hitInfo.point); // Sutam speletaju uz poziiciju kur uzpiests.
                }
            }

            if (agent.velocity.magnitude > 0f) // Parbaudam vai Aìents ir kustibâ
            {
                if (agent.remainingDistance <= agent.stoppingDistance + 0.2f) // pârbaudam galamçría distanci.
                {
                    anim.SetBool("isWalking", false);
                }
                else
                {
                    anim.SetBool("isWalking", true);
                }
            }
        }
        else
        {
            if (gameOver == false)
            {
                IsPlayerDisabled();
            }
        }
    }
}
