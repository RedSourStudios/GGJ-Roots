using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Walker : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public Transform upCol;
    public Transform downCol;
    public Transform headPoint;
    private bool colliding;
    public LayerMask layerMask;
    public LayerMask groundLayerMask;
    private CapsuleCollider2D capsule;

    private GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player"); //não sei o quão pesado fica todos os inimigos utilizarem essa função
        //para procurar o player, caso fique mto ruim transformar o script do player stats em singleton
    }

    
    void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        colliding = Physics2D.Linecast(upCol.position, downCol.position, layerMask);
        if(colliding || !Grounded()) {
            //Debug.Log("Trocando de direção"); //Caso bata em paredes, espinhos ou detecte que não há chão
            speed *= -1;
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
        }
    }

    private bool Grounded()
    {
        return Physics2D.Raycast(downCol.position, Vector2.down, 0.3f, groundLayerMask);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player") {
            float height = col.contacts[0].point.y - headPoint.position.y;

            if(height > 0) {
                col.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(col.gameObject.GetComponent<Rigidbody2D>().velocity.x, 0f);
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                //Debug.Log("Personagem pulou na cabeça do inimigo");
                //Destruir inimigo
                capsule.enabled = false; //para evitar encostar mais vezes antes de destruído
                rb.bodyType = RigidbodyType2D.Static;
                Destroy(gameObject, 0.5f); //tempo da animação
            }
            else {
                player.GetComponent<Player_Stats>().TakeDamage(1);
                //chamar player tomando dano
            }
        }
    }


}
