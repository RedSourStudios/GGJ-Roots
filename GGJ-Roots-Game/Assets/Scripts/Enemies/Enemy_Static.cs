using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Static : MonoBehaviour
{
    //detecção
    //[SerializeField] private Transform rightSpot;
    //[SerializeField] private Transform leftSpot;
    //public float detectionSize;
    private CapsuleCollider2D capsule;
    private BoxCollider2D detectionBox;
    [SerializeField] bool playerDetected;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullet;
    private float timer;
    public float timeToSpawnBullet;
    
    void Start()
    {
        timer = timeToSpawnBullet;
        capsule = GetComponent<CapsuleCollider2D>();
    }

    
    void Update()
    {
        timer -= Time.deltaTime;
        if(playerDetected && timer <= 0) {
            InstantiateBullet();
        }
    }

    void OnDrawGizmos() {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(transform.position, new Vector3(rightSpot.position.x - leftSpot.position.x, detectionSize, 0f));
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "Player") {
            playerDetected = true;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if(col.gameObject.tag == "Player") {
            playerDetected = false;
        }
    }

    void InstantiateBullet() {
        timer = timeToSpawnBullet;
        Instantiate(bullet, firePoint.position, transform.rotation);
    }
}
