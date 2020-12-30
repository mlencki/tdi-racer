using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript: MonoBehaviour
{
    public float speed;

    protected Vector3 position;
    protected GameStateManager gameStateManager;

    public void Start ()
    { 
        this.gameStateManager = GameObject.Find("GameStateManager").GetComponent<GameStateManager>();

        this.speed = Random.Range(0.5f, 1) * this.gameStateManager.gameSpeed;

        this.position = this.transform.position;

        if(this.IsOnOppositeLane())
        {
            speed *= 2;
        }
    }
	
    public void Update ()
    {
        if (this.IsDestroyable())
        {
            Destroy(this.gameObject);
        }

        if(this.gameStateManager.gameOver)
        {
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
            Destroy(this);
        }
    }

    public void FixedUpdate()
    {
        this.position.z -= speed;
        this.transform.position = position;

        this.RunEngineVibrationsEffect();
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Car" && !this.gameStateManager.gameOver) {
            if (this.AreCollidedCarsVisible(collision)) {
                this.AdjustSpeedToCollidedCar(collision);
            } else {
                Destroy(collision.gameObject);
            }
        }
    }

    protected bool IsDestroyable()
    {
        return this.transform.position.z < -120;
    }

    protected bool IsOnOppositeLane()
    {
        return this.transform.rotation.y > 0;
    }

    protected bool AreCollidedCarsVisible(Collision collision)
    {
        return gameObject.transform.position.z < 600;
    }

    protected void AdjustSpeedToCollidedCar(Collision collision)
    {
        this.speed = collision.gameObject.GetComponent<CarScript>().speed;
        Physics.IgnoreCollision(collision.collider, this.gameObject.GetComponent<Collider>());

        collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }

    protected void RunEngineVibrationsEffect()
    {
        this.position.y = -10 + Mathf.Sin(Time.time * 20f);
    }
}
