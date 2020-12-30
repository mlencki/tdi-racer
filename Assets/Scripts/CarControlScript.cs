using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CarControlScript: MonoBehaviour
{
    public bool godMode = false;
    public GameObject[] lanes;
    public int currentLane = 4;

    protected Vector3 position;
    protected Vector3 velocity = Vector3.zero;
    protected GameStateManager gameStateManager;
    protected Quaternion carRotation;

    public void Start () 
    {
        this.gameStateManager = GameObject.Find("GameStateManager").GetComponent<GameStateManager>();
        this.position = this.transform.position;
        this.lanes = GameObject.FindGameObjectsWithTag("Lane");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.MoveRight();
        }
    }

    public void FixedUpdate()
    {
        this.position.y = -10 + Mathf.Sin(Time.time * 25f) * 25f;
        this.carRotation = Quaternion.LookRotation(new Vector3(lanes[currentLane - 1].transform.position.x, 0, 200) - this.transform.position);

        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, this.carRotation, 0.4f);
        this.transform.position = Vector3.SmoothDamp(this.transform.position, this.position, ref this.velocity, 0.4f);
    }

    protected void MoveLeft()
    {
        if(this.currentLane == 1)
        {
            return;
        }

        this.currentLane--;

        this.position.x = this.lanes[currentLane - 1].transform.position.x;
    }

    protected void MoveRight()
    {
        if (this.currentLane == 4)
        {
            return;
        }

        this.currentLane++;

        this.position.x = this.lanes[currentLane - 1].transform.position.x;
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Car" && !godMode)
        {
            this.gameStateManager.gameOver = true;
            this.gameStateManager.gameSpeed = 0;

            this.TurnOnPhysics(collision);

            GameObject.Find("Canvas").transform.Find("GameOverScreen").gameObject.SetActive(true);

            Destroy(this);
        }
    }

    protected void TurnOnPhysics(Collision collision)
    {
        this.gameObject.GetComponent<Rigidbody>().useGravity = true;

        this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 100, 100);
        collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 100, -100);
    }
}
