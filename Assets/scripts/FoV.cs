using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoV : MonoBehaviour
{
    public GameObject[] PotentialTargets;  // Policista mçríi
    
    NPC npc;

    [SerializeField]
    public Vector3 lastTargetPos;
    [SerializeField]
    public Transform targetToChase;
    bool targetLost = false; // Kad pazaudejam speletaju panemam pedejas kordinates vienreiz.

    public bool hasAtarget; // pados bool uz NPC chaseTarget
    public GameObject playerToDisable; // Lai noteiktu konkrçtâ speletaja objektu

    private void Start()
    {
        npc = GetComponent<NPC>();
    }

    void Update() // Is called on each rendered frame.
    {
        bool localTarget = false;

        foreach (GameObject target in PotentialTargets) // Parbaudam targetus ieks Potencialajiem targetiem (swap to for cikls?)
        {
            if (CanSee(target) == true)   // Ja ir mçríis
            {
                localTarget = true;

                playerToDisable = target;
            }
        }

        hasAtarget = localTarget; // Padaram localTarget bool vertibu pieejamu
    }

    public bool CanSee(GameObject target)  // metode kas noteiks vai objekts redz meerki
    {   
        float distance = 20.0f; // Cik talu objekts redz
        float arc = 60.0f; // redzes lenkis
                      // 
        Vector3 rayOffset = new Vector3(0, 2f, 0); // Offsets prieks raycast lai ir redzes augstuma

        // Raycast vizualizcija ieks scenes
        Vector3 forward = transform.TransformDirection(Vector3.forward) * distance;
        Debug.DrawRay(transform.position + rayOffset, forward, Color.green);

        if (Vector3.Distance(transform.position, target.transform.position) < distance) // Parbaudam vai objekta un merkja tekosa poziicija ir mazaka par distance
        {
            Vector3 temp = target.transform.position - transform.position;
            //Vector3 fwd = transform.TransformDirection(Vector3.forward);  Dont need to check whats directly in front.

            if (Vector3.Angle(transform.forward, temp + rayOffset) < arc) // Field of View
            {
                Debug.DrawRay(transform.position + rayOffset, temp, Color.red); // DEBUG RAYCAST

                RaycastHit hitInfo; // iegustam informaciju par objektu
                Ray ray = new Ray(transform.position + rayOffset, temp); // Draw new raycast each time

                if (Physics.Raycast(ray, out hitInfo, distance) == true)
                {
                    if(hitInfo.collider.gameObject.tag != "wall") // Parbaudam vai nav siena
                    {
                        if (hitInfo.collider.gameObject.tag == "Player")  //  Parbaudam vai speletajs
                        {
                            Debug.Log("I see a player");
                            npc.isChasing = true; // Cheisojam speletaju
                            targetToChase = target.transform;
                            targetLost = true; // Merki pazaudejam panemam pedejas kordinates.

                            return true;
                        }    
                    }
                }
            }
            // Out off field of view
            else
            {
                if(targetLost == true)
                {
                    targetToChase = null;
                    lastTargetPos = target.transform.localPosition;
                    targetLost = false; // Izsledzam jo kordinates jau ir panemtas, lai neapdeito visu laiku.
                }
            }
        }

        return false;
    }
}