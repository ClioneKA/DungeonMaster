using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepMovement : MonoBehaviour
{
    public Transform[] drops;
    public Transform blood;

    public float speed;
    public int maxLife;
    int life;
    float moveX;
    float moveY;

    bool SuperArmor;
    int SACoolDown;
    
    Rigidbody2D rbody;
    SpriteRenderer Sp;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("changeDirection", 0f, Random.Range(1f, 2f));
        life = maxLife;
        SuperArmor = false;
    }

    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        Sp = GetComponent<SpriteRenderer>();
        rbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rbody.interpolation = RigidbodyInterpolation2D.Extrapolate;
    }

    void FixedUpdate()
    {
        rbody.velocity = new Vector2 (moveX * Time.deltaTime * speed, moveY * Time.deltaTime * speed);
    }

    void changeDirection()
    {
        moveX = Random.Range(-1f, 1f);
        moveY = Random.Range(-1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(moveX>0){
            Sp.flipX = true;
        }
        else if(moveX<0){
            Sp.flipX = false;
        }
        if(SuperArmor){
            moveX = 0;
            moveY = 0;
            if(SACoolDown%20>10){
                Sp.color = Color.white;
            }
            else{
                Sp.color = Color.red;
            }
            if(SACoolDown++>240){
                SuperArmor=false;
                SACoolDown=0;
                Sp.color = Color.white;
            }
        }
    }
    void OnTriggerEnter2D (Collider2D other) 
    {
        if (other.gameObject.layer == 9){
			if(--life>0){
                SuperArmor = true;
            }
            else{
                Instantiate(blood, transform.position, Quaternion.identity);
                //Drop Items
                float dropRate = Random.Range(0f, 1f);
                if(dropRate <= 0.90f){
                    Instantiate(drops[0], transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
            } 
		}
    }
}
