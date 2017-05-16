using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public DinoBehaviour player;

    public float minX;
    public float maxX;

    void Update()
    {
        if (player.transform.position.x > minX && player.transform.position.x < maxX) transform.position = new Vector3(player.transform.position.x, transform.position.y, -10);
        else if(player.transform.position.x < minX) transform.position = new Vector3(minX, transform.position.y, -10);
        else if(player.transform.position.x > maxX) transform.position = new Vector3(maxX, transform.position.y, -10);
    }

}
