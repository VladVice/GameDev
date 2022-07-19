using UnityEngine;
using TMPro;

public class ScreenTextInf : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI tx_Object;
    DropPoint dropPoint;

    int Cash = 0;

    void Start()
    {
        tx_Object.text = "Stolen goods: "+ Cash;
    }

    public void UpdateScreenStat()
    {
        Cash = dropPoint.Cash;
        tx_Object.text = "Stolen goods: "+ Cash;
    }
}
