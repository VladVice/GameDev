using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    NavMeshAgent agent; // Navigation Agent 
    Animator anim; // Animacijas komponents
    
    public Transform[] Navs; // Masivs ar kordinatem uz kuram vares aìents staigât;
    public bool isPatrooling;
    public bool isChasing;

    [SerializeField] 
    bool gameOver;
    
    [SerializeField]
    bool hasAtarget;

    FoV fov;

    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>(); // inicializejam
        fov = GetComponent<FoV>();
        isPatrooling = true;
    }

    private void Update()
    {
        if (gameOver == false)
        {
            hasAtarget = fov.hasAtarget;

            if (agent.velocity.magnitude > 0f) // Parbaudam vai Aìents ir kustibâ
            {
                if (agent.remainingDistance <= agent.stoppingDistance + 0.2f) // pârbaudam galamçría distanci, lai apstajas laiciigak
                {
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isRunning", false);
                }
                else
                {
                    if (isChasing == true)
                    {
                        anim.SetBool("isRunning", true);
                        agent.speed = 4.0f;
                    }
                    else
                    {
                        anim.SetBool("isWalking", true);
                        agent.speed = 3.0f;
                    }
                }
            }
            // Ja ir mçríis. Follow player.
            if (hasAtarget == true)
            {
                isPatrooling = false;
                agent.ResetPath(); // Apstadinam lai neiet uz tekoso chekpointu

            }
            else
            {
                if (agent.remainingDistance == 0.0f) // 
                {
                    isPatrooling = true;
                    isChasing = false;
                }
            }

            // patrol
            if (isPatrooling == true)
            {
                if (agent.velocity.magnitude == 0f) // Vai Aìens stâv uz vietas ?
                {
                    agent.SetDestination(Navs[Random.Range(0, Navs.Length)].position); // Random patrol cords
                }
            }
            // Izpildam Pakaldzîðanos
            if (isChasing == true)
            {
                if (fov.targetToChase != null) // Parbaudam vai speletajs vel joprojam ir redzes loka
                {
                    agent.destination = fov.targetToChase.position; // Sekojam Speletajam

                    if ((fov.targetToChase.position - transform.position).magnitude < 2.0f && gameOver == false) // Cik tuvu policistam jabut 
                    {
                        //Debug.Log("I GOT HIM!!");
                        fov.playerToDisable.GetComponent<PlayerMove>().isDisabled = true; // 
                        agent.ResetPath();
                        anim.SetBool("isDancing", true); 
                        gameOver = true;
                        isChasing = false;
                        transform.GetComponent<FoV>().enabled = false; // Experimental
                    }
                }
                else
                {
                    // ja speletajs nav redzes loka ejam uz pedejo zinamo poziiciju.
                    agent.destination = fov.lastTargetPos;
                    Debug.Log("Police Agent: Moving to last seen pos!");
                }
            }
        }
    }
}

