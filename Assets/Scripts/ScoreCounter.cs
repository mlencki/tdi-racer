using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter: MonoBehaviour
{
    protected GameStateManager gameStateManager;
    protected float metersTraveled = 0;
    protected Text scoreText;

    public void Start ()
    {
        this.gameStateManager = GameObject.Find("GameStateManager").GetComponent<GameStateManager>();
        this.scoreText = GameObject.Find("Canvas").transform.Find("ScoreText").GetComponent<Text>();
        this.StartCoroutine("RecountScore");
    }

    public void Update()
    {
        if (this.gameStateManager.gameOver) {
            GameObject.Find("Canvas")
                .transform.Find("GameOverScreen")
                .transform.Find("FinalScoreText")
                .GetComponent<Text>().text = (this.metersTraveled / 1000).ToString("F2");

            this.gameObject.SetActive(false);
        }
    }

    protected IEnumerator RecountScore()
    {
        while (!this.gameStateManager.gameOver) {
            this.metersTraveled += 5;
            this.scoreText.text = ((this.metersTraveled/1000).ToString("F2"))+" km";
            yield return new WaitForSeconds(0.4f / this.gameStateManager.gameSpeed);
        }
    }
}
