using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public GameObject particle;

    [SerializeField]
    private float speed;
    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;
    public float minSwipeLength = 5f;
    public float jumpHeight = 10f;

    public enum Swipe { None, Up, Down, Left, Right };

    private Vector2 firstClickPos;
    private Vector2 secondClickPos;

    bool gameStart;
    bool gameOver;

    Rigidbody rb;
        
    public static Swipe swipeDirection;

    // Use this for initialization

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.GetComponent<TrailRenderer>().enabled = false;
        
    }


    void Start()
    {
        gameStart = false;
        gameOver = false;
    }


    // Update is called once per frame
    void Update()
    {

        if (!gameStart)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rb.velocity = new Vector3(speed * Time.smoothDeltaTime, 0, 0);
                gameStart = true;

                GameManager.instance.StartGame();

            }

        }

        if (!Physics.Raycast(transform.position, Vector3.down, 10f))
        {
            gameOver = true;
            rb.velocity = new Vector3(0, -25f, 0);

            Camera.main.GetComponent<CameraFollow>().gameOver = true;
            GameManager.instance.gameOver();
        }

        if (Input.GetMouseButtonDown(0) && !gameOver)
        {
            switchDirection();

        }

    }

    void switchDirection()
    {
        if (rb.velocity.z > 0)
        {
            rb.velocity = new Vector3(speed, 0, 0);
        }
        else if (rb.velocity.x > 0)
        {
            rb.velocity = new Vector3(0, 0, speed);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Diamond")
        {
            ScoreManager.instance.score += 10;
            GameObject part = Instantiate(particle, col.gameObject.transform.position, Quaternion.identity) as GameObject;

            Destroy(col.gameObject);
            Destroy(part, 1f);

        }
        if (col.gameObject.tag == "increaseCapsule")
        {
            increaseSpeed();
            gameObject.GetComponent<TrailRenderer>().enabled = true;

            if (transform.localScale.x < 4)
            {
                increaseSize();
            }

            Destroy(col.gameObject);
            Invoke("disableTrail", 1.5f);
        }

        if (col.gameObject.tag == "decreaseCapsule")
        {
            decreaseSpeed();
            if (transform.localScale.x > 0.5)
            {
                decreaseSize();
            }
            Destroy(col.gameObject);
        }
    }

    void increaseSpeed()
    {
        speed++;

        if (rb.velocity.z > 0)
        {
            rb.velocity = new Vector3(0, 0, speed);
        }
        else if (rb.velocity.x > 0)
        {
            rb.velocity = new Vector3(speed, 0, 0);
        }
    }

    void decreaseSpeed()
    {
        speed--;

        if (rb.velocity.z > 0)
        {
            rb.velocity = new Vector3(0, 0, speed);
        }
        else if (rb.velocity.x > 0)
        {
            rb.velocity = new Vector3(speed, 0, 0);
        }
    }

    private void disableTrail()
    {
        gameObject.GetComponent<TrailRenderer>().enabled = false;
    }

    private void increaseSize()
    {
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }

    private void decreaseSize()
    {
        transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
    }

}

