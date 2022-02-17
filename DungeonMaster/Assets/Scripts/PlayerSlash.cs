using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlash : MonoBehaviour
{
    int lastingFrame;
    // Start is called before the first frame update
    void Start()
    {
        lastingFrame=0;
    }

    // Update is called once per frame
    void Update()
    {
        if(lastingFrame++>60){
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D (Collider2D other) 
	{
		
	}
}
