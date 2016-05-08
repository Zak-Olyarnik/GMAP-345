using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text scoreText;          // displays score
    static public int score;        // running total score, must be accessible from other scripts
    static public int itemCount;    // running total items collected, must be accessible from other scripts
    static public bool freeze;      // whether or not the player has a "freeze" power-up
    static public GameController instance;      // instance of the GameController

    void Start()
    {
        instance = this;    // sets instance of the gameController
        score = 0;          // initializes score and itemCount
        itemCount = 0;
    }

    // gets current instance of the gameController
    public static GameController getInstance()
        {return instance;}

	void Update ()
    {
        if (itemCount == 15)    // checks for game end condition
        { Invoke("LoadLevel", .7f); }    // invoke allows a delay
	}

    // updates and displays the score
    public void UpdateScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }

    // sets game to "frozen", must be accessible from other scripts
    public void Freeze()
    {
        freeze = true;
        EnemyController.freezeTime = 4.5f;
    }

    // returns to menu
    void LoadLevel()
        { Application.LoadLevel("Menu"); }
}
