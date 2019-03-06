using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {

    public GameObject increaseCapsule;
    public GameObject decreaseCapsule;
    public GameObject diamonds;
    public GameObject platform;
    Vector3 lastPos;
    float size;
    public bool gameOver;

	// Use this for initialization
	void Start () {
        lastPos = platform.transform.position;
        size = platform.transform.localScale.x;
        gameOver = false;

        for(int i =0;i< 20; i++)
        {
            SpawnPlatform();
        }

	}
	
    public void startSpawningPlatform()
    {
                InvokeRepeating("SpawnPlatform", 1f, 0.2f);
    }
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.gameEnd == true)
        {
            CancelInvoke("SpawnPlatform");
        }
	}

    void SpawnPlatform()
    {
        int rand = Random.Range(0, 6);
            if(rand < 3)
        {
            SpawnX();

        }
            else if (rand >= 3)
        {
            SpawnZ();
        }
    }

    void SpawnX()
    {
        Vector3 pos = lastPos;
        pos.x += size;
        lastPos = pos;

        Instantiate(platform, pos, Quaternion.identity);

        int random = Random.Range(0, 5);
        if(random < 1)
        {
            Instantiate(diamonds, new Vector3(pos.x,pos.y+1,pos.z), diamonds.transform.rotation);
        }

        int increaseRandom = Random.Range(0, 20);
        if (increaseRandom < 1)
        {
            Instantiate(increaseCapsule, new Vector3(pos.x + 1, pos.y + 1, pos.z), increaseCapsule.transform.rotation);
        }

        int decreaseRandom = Random.Range(0, 40);
        if (decreaseRandom < 1)
        {
            Instantiate(decreaseCapsule, new Vector3(pos.x - 1, pos.y + 1, pos.z), decreaseCapsule.transform.rotation);
        }


    }

    void SpawnZ()
    {
        Vector3 pos = lastPos;
        pos.z += size;
        lastPos = pos;

        Instantiate(platform, pos, Quaternion.identity);

        int random = Random.Range(0, 4);
        if (random < 1)
        {
            Instantiate(diamonds, new Vector3(pos.x, pos.y + 1, pos.z), diamonds.transform.rotation);
        }

        int increaseRandom = Random.Range(0, 20);
        if (increaseRandom < 1)
        {
            Instantiate(increaseCapsule, new Vector3(pos.x + 1, pos.y + 1, pos.z), increaseCapsule.transform.rotation);
        }

        int decreaseRandom = Random.Range(0, 40);
        if (decreaseRandom < 1)
        {
            Instantiate(decreaseCapsule, new Vector3(pos.x - 1, pos.y + 1, pos.z), decreaseCapsule.transform.rotation);
        }


    }
}
