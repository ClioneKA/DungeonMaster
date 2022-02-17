using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartClick()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
    public void QuitClick()
    {
        Application.Quit();
    }
    public void BackClick()
    {
        SceneManager.LoadScene("Opening", LoadSceneMode.Single);
    }
}
