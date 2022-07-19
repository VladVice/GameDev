using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemPickup : MonoBehaviour
{
    public Transform Hand;

    bool isInHands;
    public int price; // Item prace -> Move later to scriptable objects maybe

    Ray ray;
    RaycastHit hit;
  
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Item")
        {
            // Get first child obj and set it active
            hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false); // will disable all active item texts (Works fine as only one child obj will be enabled at the time)
        }
    }

    private void OnMouseDown()
    {
        PickUp();
    }

    void PickUp()
    {
        float pickUpDistance = 3.0f; // To mesure dist from obj to hand
        float dist = Vector3.Distance(transform.position, Hand.position);  // Charater -> Hand position

        if (dist < pickUpDistance)
        {

            if (isInHands == false) // Empty hand
            {
                GetComponent<Rigidbody>().useGravity = false; 
                GetComponent<Rigidbody>().isKinematic = true;
                transform.GetComponent<NavMeshObstacle>().enabled = false;
                transform.position = Hand.position; // Set position
                transform.parent = GameObject.Find("HandTarget").transform; // find hand to attache object to
                isInHands = true; // bool to determin if you are holding soemthing
            }
            else // Is holding something
            {
                transform.parent = null; // Unparrent object from hand
                GetComponent<Rigidbody>().isKinematic = false; 
                GetComponent<Rigidbody>().useGravity = true;
                transform.GetComponent<NavMeshObstacle>().enabled = true; // Enable obstackle (In future will allow to block polcie man by creating baricades
                isInHands = false; // Clear hand
            }
        }
    }
}
