using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D other) 
    {
        if(other.gameObject.layer == 10){
            if(gameObject.tag == "Hp_Potion"){
                PlayerMovement PM = other.gameObject.GetComponent<PlayerMovement>();
                PM.life += PM.maxLife/2;
                if(PM.life>PM.maxLife) PM.life=PM.maxLife;
                GameManager GC = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
                GC.needNewUI = true;
            }
            if(gameObject.tag == "Max_Potion"){
                PlayerMovement PM = other.gameObject.GetComponent<PlayerMovement>();
                PM.maxLife++;
                PM.life=PM.maxLife;
                GameManager GC = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
                GC.needNewUI = true;
            }
            Destroy(gameObject);
        }
        
    }
}
