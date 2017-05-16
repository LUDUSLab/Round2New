using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    public float backgroundSize;

    private Transform cameraTransform;
    private Transform[] layers;
    private float viewzone = 1;
    private int leftIndex;
    private int rightIndex;

    public float parallaxSpeed;
    private float lastCameraX;
    private float lastZ;
    private float lastY;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraX = cameraTransform.position.x;
        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            layers[i] = transform.GetChild(i);

        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }

    void Update()
    {
        float deltaX = cameraTransform.position.x - lastCameraX;
        lastZ = transform.position.z;
        lastY = transform.position.y;
        transform.position += Vector3.right * (deltaX * parallaxSpeed);
        transform.position = new Vector3(transform.position.x, lastY, lastZ);
        lastCameraX = cameraTransform.position.x;

        if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewzone))
            ScrollLeft();
            
        if (cameraTransform.position.x > (layers[rightIndex].transform.position.x + viewzone))
            ScrollRight();
    }
	
    private void ScrollLeft()
    {
        int lastRight = rightIndex;
        lastZ = layers[rightIndex].position.z;
        lastY = layers[rightIndex].position.y;
        layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize);
        layers[rightIndex].position = new Vector3(layers[rightIndex].position.x, lastY, lastZ);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
            rightIndex = layers.Length - 1;
    }

    private void ScrollRight()
    {
        int lastLeft = leftIndex;
        lastZ = layers[rightIndex].position.z;
        lastY = layers[rightIndex].position.y;
        layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);
        layers[leftIndex].position = new Vector3(layers[leftIndex].position.x, lastY, lastZ);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
            leftIndex = 0;
    }
}
