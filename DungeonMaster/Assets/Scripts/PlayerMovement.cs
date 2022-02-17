using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float speed = 100f;
    float moveX;
    float moveY;
    bool SuperArmor;
    int SACoolDown;
    
    public int meleeCDFrame;

    public int life;
    public int maxLife;

    string player_control;

    public Transform Slash;
    public Transform Tomb;
    Rigidbody2D rbody;
    SpriteRenderer Sp;
    AudioSource hurtAudio;

    int face;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.tag == "Player") player_control = "p1";
        else if (gameObject.tag == "Player2") player_control = "p2";
        life = maxLife;
        SuperArmor = false;
    }
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        Sp = GetComponent<SpriteRenderer>();
        hurtAudio = GetComponent<AudioSource>();
        rbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rbody.interpolation = RigidbodyInterpolation2D.Extrapolate;
    }

    void FixedUpdate()
    {
        rbody.velocity = new Vector2 (moveX * Time.deltaTime * speed, moveY * Time.deltaTime * speed);
    }

    // Update is called once per frame
    void Update()
    {
        if(SuperArmor){
            if(SACoolDown%20>10){
                Sp.color = Color.white;
            }
            else{
                Sp.color = Color.red;
            }
            if(SACoolDown++>120){
                SuperArmor=false;
                SACoolDown=0;
                Sp.color = Color.white;
            }
        }

        moveX = Input.GetAxis("Horizontal_"+player_control);
		moveY = Input.GetAxis("Vertical_"+player_control);
        if(Mathf.Abs(moveX)>Mathf.Abs(moveY)){
            if(moveX>0){
                face = 6;
                Sp.flipX = false;
            }
            else if(moveX<0){
                face = 4;
                Sp.flipX = true;
            }
        }
        else if(Mathf.Abs(moveX)<Mathf.Abs(moveY)){
            if(moveY>0){
                face = 8;
            }
            else if(moveY<0){
                face = 2;
            }
        }
        if(Input.GetButtonDown("Fire1_"+player_control) && meleeCDFrame>90){
            Vector3 pos;
            switch(face){
                
                case 2:
                    pos = new Vector3(transform.position.x, transform.position.y-0.16f, 0);
                    Instantiate(Slash, pos, Quaternion.identity);
                    break;
                case 4:
                    pos = new Vector3(transform.position.x-0.16f, transform.position.y, 0);
                    Instantiate(Slash, pos, Quaternion.identity);
                    break;
                case 6:
                    pos = new Vector3(transform.position.x+0.16f, transform.position.y, 0);
                    Instantiate(Slash, pos, Quaternion.identity);
                    break;
                case 8:
                    pos = new Vector3(transform.position.x, transform.position.y+0.16f, 0);
                    Instantiate(Slash, pos, Quaternion.identity);
                    break;
            }
            meleeCDFrame=0;
        }
        meleeCDFrame++;
    }

    void OnTriggerEnter2D (Collider2D other) 
    {
        if (other.gameObject.layer == 8 && !SuperArmor){
			if(--life>0){
                SuperArmor=true;
                hurtAudio.Play();
            } 
            else{
                Instantiate(Tomb, transform.position, Quaternion.identity);
                Destroy(gameObject); 
            } 
            GameManager GC = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
            GC.needNewUI = true;
		}
    }
}
