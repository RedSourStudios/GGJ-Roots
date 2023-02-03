using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObject : MonoBehaviour
{
    public Transform playerTransform;
    private Transform transform;
    private Vector3 aux;
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {   
        aux.y = playerTransform.position.y;
        transform.position = aux;
    }
}
