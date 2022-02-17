using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : MonoBehaviour
{
    float speed = 100f;
    float moveX;
    float moveY;
    
    public Transform Slash;
    Rigidbody2D rbody;

    int face;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
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
        moveX = Input.GetAxis("Horizontal_p2");
		moveY = Input.GetAxis("Vertical_p2");
        if(Mathf.Abs(moveX)>Mathf.Abs(moveY)){
            if(moveX>0){
                face = 6;
            }
            else if(moveX<0){
                face = 4;
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
        if(Input.GetButtonDown("Fire1_p2")){
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
        }
    }
}
