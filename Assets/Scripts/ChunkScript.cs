using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkScript: MonoBehaviour
{
    protected float speed = 0;
    protected Vector3 position;
    protected GameStateManager gameStateManager;
    protected RoadChunkManager chunkManager;

    public void Start ()
    {
        this.gameStateManager = GameObject.Find("GameStateManager").GetComponent<GameStateManager>();
        this.chunkManager = GameObject.Find("ChunkManager").GetComponent<RoadChunkManager>();
        this.position = this.transform.position;
        this.speed = this.gameStateManager.gameSpeed;
    }
	
    public void Update ()
    {
        this.UpdateChunkSpeed();

        if(this.IsDestroyable()) {
            this.chunkManager.chunkList.RemoveAt(1);
            Destroy(this.gameObject);
        }
    }

    public void FixedUpdate()
    {
        this.UpdateChunkPosition();
    }

    protected void UpdateChunkPosition()
    {
        this.position.z = this.position.z - this.speed;
        this.transform.position = this.position;
    }

    protected void UpdateChunkSpeed()
    {
        this.speed = this.gameStateManager.gameSpeed;
    }

    protected bool IsDestroyable()
    {
        return this.transform.position.z < -550;
    }
}
