using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPoint : MonoBehaviour
{
    GameObject CashInItem;
    ScreenTextInf textUpdate;
    public int Cash = 0;

    private void Start()
    {
      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            CashInItem = collision.collider.gameObject;
            CashIn();
        }
    }

    void CashIn()
    {
        Cash += CashInItem.GetComponent<ItemPickup>().price;
        //textUpdate.UpdateScreenStat();
        Destroy(CashInItem);
    }
}
