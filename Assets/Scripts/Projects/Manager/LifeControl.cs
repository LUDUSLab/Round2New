using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeControl : MonoBehaviour
{


    public DinoBehaviour player;

    public Sprite threeHp;
    public Sprite twoHp;
    public Sprite oneHp;

    void Update()
    {

        switch (player.getHp())
        {
            case 3:
                this.transform.GetComponent<UnityEngine.UI.Image>().sprite = threeHp;
                break;
            case 2:
                this.transform.GetComponent<UnityEngine.UI.Image>().sprite = twoHp;
                break;
            case 1:
                this.transform.GetComponent<UnityEngine.UI.Image>().sprite = oneHp;
                break;
        }
    }
}
