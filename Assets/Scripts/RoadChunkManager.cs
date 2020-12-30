using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoadChunkManager: MonoBehaviour
{
    public GameObject chunk;
    public List<GameObject> chunkList;

    protected GameStateManager gameStateManager;

    public void Start ()
    {
        this.gameStateManager = GameObject.Find("GameStateManager").GetComponent<GameStateManager>();
    }
	
    public void Update ()
    {
        if (this.CanInstantiateNewChunk()) {
            this.InstantiateNewChunk();
        }
    }

    protected void InstantiateNewChunk()
    {
        GameObject newChunk = Instantiate(this.chunk, this.GetInstatiationPosistion(), Quaternion.identity);
        this.chunkList.Add(newChunk);
    }

    protected Vector3 GetInstatiationPosistion()
    {
        float zPos;

        if (chunkList.Count() == 0) {
            zPos = -100;
        } else {
            zPos = this.chunkList.Last().transform.position.z + 500;
        }

        return new Vector3(0, 0, zPos - this.gameStateManager.gameSpeed);
    }

    protected bool CanInstantiateNewChunk()
    {
        return this.chunkList.Count() < 3;
    }
}
