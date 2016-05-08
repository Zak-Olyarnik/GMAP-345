using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text lastScore;  // displays score
    public GameObject t;    // GameObject parented to lastScore

    void Start()
    {
        if (GameController.score != 0)
        {
            t.SetActive(true);      // enabling lastScore by itself didn't work for some reason, so
            lastScore.text = "score - " + GameController.score; // I SetActive() its parent instead
        }
    }

    // loads main level
    public void StartClick ()
        {Application.LoadLevel("Main");}

}
