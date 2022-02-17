using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTomb : MonoBehaviour
{
    public Transform Player1;
    public Transform Player2;
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
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Player2"){
            GameObject[] check1, check2;
            check1 = GameObject.FindGameObjectsWithTag("Player");
            check2 = GameObject.FindGameObjectsWithTag("Player2");
            if(check1.Length==0 && check2.Length==1){
                Instantiate(Player1, transform.position, Quaternion.identity);
                GameManager GC = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
                GC.needNewUI = true;
            }
            else if (check1.Length==1 && check2.Length==0){
                Instantiate(Player2, transform.position, Quaternion.identity);
                GameManager GC = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
                GC.needNewUI = true;
            }
            Destroy(gameObject);
        }
        
    }
}
