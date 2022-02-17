using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform Wall;
    public Transform Ground;
    public Transform Door;
    public Transform Ob1;
    public Transform Ob2;

    public Transform Player1;
    public Transform Player2;

    public Transform Player1_icon;
    public Transform Player2_icon;
    public Transform HpBar;

    public Transform[] Mobs;

    public bool needNewMap;
    public bool needNewUI;

    enum gameState{
        idle,
        init,
        generate,
        spawn,
        game,
        cleanup,
        gameover
    }

    enum playerState{
        alive,
        tomb,
        dead
    }


    static int mapX = 30;
    static int mapY = 20;
    int[,] map= new int[mapX*2+1, mapY*2+1];
    int level;
    int p1maxLife, p2maxLife;
    gameState currentState;
    gameState nextState;
    playerState p1State;
    playerState p2State;

    void mapGenerator()
    {
        for(int x=-mapX;x<=mapX;x++){
            for(int y=-mapY;y<=mapY;y++){
                if(x==-mapX||x==mapX||y==-mapY||y==mapY){//walls and doors
                    Vector3 pos = new Vector3(x*0.16f, y*0.16f, 0);
                    float wType = Random.Range(0,100);
                    if((x==mapX && y==mapY-1) || wType == 0 && !(x==mapX&&y==mapY) && !(x==mapX&&y==-mapY) && !(x==-mapX&&y==mapY) && !(x==-mapX&&y==-mapY)){
                        Instantiate(Door, pos, Quaternion.identity);
                        map[x+mapX, y+mapY] = 1;//Door
                    }
                    else{
                        Instantiate(Wall, pos, Quaternion.identity);
                        map[x+mapX, y+mapY] = -1;
                    }
                        
                }
                else{//grounds and obstacles
                    Vector3 pos = new Vector3(x*0.16f, y*0.16f, 0);
                
                    if(x==-mapX+1||x==mapX-1||y==-mapY+1||y==mapY-1){
                        Instantiate(Ground, pos, Quaternion.identity);
                        map[x+mapX, y+mapY] = 0;
                    }
                    else{
                        float gType = Random.Range(0,100);
                        if(gType<85){
                            Instantiate(Ground, pos, Quaternion.identity);
                            map[x+mapX, y+mapY] = 0;
                        }
                        else if(gType<86){
                            Instantiate(Ob1, pos, Quaternion.identity);
                            map[x+mapX, y+mapY] = -1;
                        }
                        else{
                            Instantiate(Ob2, pos, Quaternion.identity);
                            map[x+mapX, y+mapY] = -1;
                        }
                    }
                    
                }
            }
        }
    }

    void playerGenerator()
    {
        int x;
        int y;
        if(p1State == playerState.alive){
            do{
                x = Random.Range(-mapX+1, mapX);
                y = Random.Range(-mapY+1, mapY);
            }while(map[x+mapX, y+mapY]==-1);
            Vector3 pos = new Vector3(x*0.16f, y*0.16f, 0);
            Instantiate(Player1, pos, Quaternion.identity);
        }
        if(p2State == playerState.alive){
            do{
                x = Random.Range(-mapX+1, mapX);
                y = Random.Range(-mapY+1, mapY);
            }while(map[x+mapX, y+mapY]==-1);
            Vector3 pos = new Vector3(x*0.16f, y*0.16f, 0);
            Instantiate(Player2, pos, Quaternion.identity);
        }
        GameObject P1O = GameObject.FindGameObjectWithTag("Player");
        if(P1O != null){
            PlayerMovement P1 = P1O.GetComponent<PlayerMovement>();
            P1.maxLife = p1maxLife;
        }
        GameObject P2O = GameObject.FindGameObjectWithTag("Player2");
        if(P2O != null){
            PlayerMovement P2 = P2O.GetComponent<PlayerMovement>();
            P2.maxLife = p2maxLife;
        }
    }

    void mobGenerator()
    {
        int x;
        int y;
        for (int i=0;i<level*2;i++){
            do{
                x = Random.Range(-mapX+1, mapX);
                y = Random.Range(-mapY+1, mapY);
            }while(map[x+mapX, y+mapY]==-1);
            Vector3 pos = new Vector3(x*0.16f, y*0.16f, 0);
            Instantiate(Mobs[0], pos, Quaternion.identity);
        }
        for (int i=0;i<level/4+2;i++){
            do{
                x = Random.Range(-mapX+1, mapX);
                y = Random.Range(-mapY+1, mapY);
            }while(map[x+mapX, y+mapY]==-1);
            Vector3 pos = new Vector3(x*0.16f, y*0.16f, 0);
            Instantiate(Mobs[1], pos, Quaternion.identity);
        }
        for (int i=0;i<level/5+1;i++){
            do{
                x = Random.Range(-mapX+1, mapX);
                y = Random.Range(-mapY+1, mapY);
            }while(map[x+mapX, y+mapY]==-1);
            Vector3 pos = new Vector3(x*0.16f, y*0.16f, 0);
            Instantiate(Mobs[2], pos, Quaternion.identity);
        }
    }

    void clear()
    {
        GameObject[] Clear = GameObject.FindGameObjectsWithTag("Map");
        for (int i=0;i<Clear.Length;i++){
            Destroy(Clear[i]);
        }
        Clear = GameObject.FindGameObjectsWithTag("Player");
        for (int i=0;i<Clear.Length;i++){
            Destroy(Clear[i]);
        }
        Clear = GameObject.FindGameObjectsWithTag("Player2");
        for (int i=0;i<Clear.Length;i++){
            Destroy(Clear[i]);
        }
        Clear = GameObject.FindGameObjectsWithTag("Crab");
        for (int i=0;i<Clear.Length;i++){
            Destroy(Clear[i]);
        }
        Clear = GameObject.FindGameObjectsWithTag("Tomb");
        for (int i=0;i<Clear.Length;i++){
            Destroy(Clear[i]);
        }
        Clear = GameObject.FindGameObjectsWithTag("UI");
        for (int i=0;i<Clear.Length;i++){
            Destroy(Clear[i]);
        }
        Clear = GameObject.FindGameObjectsWithTag("Sheep");
        for (int i=0;i<Clear.Length;i++){
            Destroy(Clear[i]);
        }
    }

    void UIGenerator()
    {
        //Show player icon
        Vector3 pos = new Vector3(-mapX*0.16f, (-mapY-1)*0.16f, 0);
        Instantiate(Player1_icon, pos, Quaternion.identity);
        pos = new Vector3(-mapX*0.16f, (-mapY-2)*0.16f, 0);
        Instantiate(Player2_icon, pos, Quaternion.identity);
        //Show Hp Bar based on hp left
        GameObject P1O = GameObject.FindGameObjectWithTag("Player");
        if(P1O != null){
            PlayerMovement P1 = P1O.GetComponent<PlayerMovement>();
            for(int i=0;i<P1.life;i++){
                pos = new Vector3((-mapX+i+1)*0.16f, (-mapY-1)*0.16f, 0);
                Instantiate(HpBar, pos, Quaternion.identity);
            }
        }
        GameObject P2O = GameObject.FindGameObjectWithTag("Player2");
        if(P2O != null){
            PlayerMovement P2 = P2O.GetComponent<PlayerMovement>();
            for(int i=0;i<P2.life;i++){
                pos = new Vector3((-mapX+i+1)*0.16f, (-mapY-2)*0.16f, 0);
                Instantiate(HpBar, pos, Quaternion.identity);
            }
        }
    }

    void cleanUI()
    {
        GameObject[] Clear = GameObject.FindGameObjectsWithTag("UI");
        for (int i=0;i<Clear.Length;i++){
            Destroy(Clear[i]);
        }
    }

    void CheckAlive()
    {
        GameObject[] check1, check2;
        check1 = GameObject.FindGameObjectsWithTag("Player");
        if(check1.Length==0){
            p1State = playerState.dead;
            p1maxLife = 10;
        }
        else{
            p1State = playerState.alive;
            PlayerMovement P1 = check1[0].GetComponent<PlayerMovement>();
            p1maxLife = P1.maxLife;
        }
        check2 = GameObject.FindGameObjectsWithTag("Player2");
        if(check2.Length==0){
            p2State = playerState.dead;
            p2maxLife = 10;
        }
        else{
            p2State = playerState.alive;
            PlayerMovement P2 = check2[0].GetComponent<PlayerMovement>();
            p2maxLife = P2.maxLife;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = gameState.init;
        nextState = gameState.init;
        needNewMap=false;
        p1maxLife = p2maxLife = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel")){
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
        if(currentState == gameState.init){       
            nextState = gameState.generate;
            p1State = playerState.alive;
            p2State = playerState.alive;
            level = 1;
        }
        else if(currentState == gameState.generate){
            mapGenerator();
            nextState = gameState.spawn;
        }
        else if(currentState == gameState.spawn){
            playerGenerator();
            mobGenerator();
            UIGenerator();
            nextState = gameState.game;
            needNewUI=true;
        }
        else if(currentState == gameState.game){
            if(needNewMap){
                nextState = gameState.cleanup;
                level++;
                needNewMap = false;
            }
            if(needNewUI){
                cleanUI();
                UIGenerator();
                needNewUI=false;
            }
            CheckAlive();
            if(p1State == playerState.dead && p2State == playerState.dead){
                nextState = gameState.gameover;
            }
        }
        else if(currentState == gameState.cleanup){
            clear();
            nextState = gameState.generate;
        }
        else if(currentState == gameState.gameover){
            clear();
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
            nextState = gameState.idle;
        }
        currentState = nextState;

    }

}
