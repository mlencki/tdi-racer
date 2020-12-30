using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager: MonoBehaviour
{
    public bool gameOver = false;
    public float gameSpeed;

    public void Start ()
    {
        this.gameSpeed = 3f;
        StartCoroutine("IncreaseGameSpeed");
    }

    protected IEnumerator IncreaseGameSpeed()
    {
        while (!this.gameOver) {
            this.gameSpeed += 0.1f;
            yield return new WaitForSeconds(5f);
        }
    }
}
