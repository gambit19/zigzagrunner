using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public bool gameEnd;    

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void StartGame()
    {
        UIManager.instance.GameStart();
        ScoreManager.instance.startScore();
        GameObject.Find("PlatformSpawner").GetComponent<PlatformSpawner>().startSpawningPlatform();
    }

    public void gameOver()
    {
        UIManager.instance.GameOver();
        ScoreManager.instance.stopScore();

        gameEnd = true;
    }

    // Use this for initialization
    void Start () {
        gameEnd = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
