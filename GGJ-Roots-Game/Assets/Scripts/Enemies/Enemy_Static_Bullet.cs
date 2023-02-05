using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Static_Bullet : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float autoDestruct;
    [SerializeField] private float autoDestructTime = 5f;
    //private Transform transform;
    [SerializeField] private float speed;
    private CircleCollider2D explosionRange;
    private CapsuleCollider2D capsule;
    private bool notExploded = true;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        capsule = GetComponent<CapsuleCollider2D>();
        explosionRange = GetComponent<CircleCollider2D>();
        autoDestruct = autoDestructTime;
    }

    void Update()
    {
        Follow();
        autoDestruct -= Time.deltaTime;

       if(autoDestruct <= 0 && notExploded) {
        Explode();
       }
    }

    void Explode() {
        notExploded = false;
        Debug.Log("Explodiu");
        explosionRange.enabled = true;
        //animação
        //som
        Destroy(gameObject, 1f); //tempo da animação
    }

    void Follow() { //utilizar navmesh2d?
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<Player_Stats>().TakeDamage(2);
        }
    }

    void OnCollisionEnter(Collision col) {
        if(col.gameObject.tag == "Player" || col.gameObject.tag == "Wall" || col.gameObject.tag == "Ground") { 
            Debug.Log("Encostei");
            Explode();
        }
    }



}
