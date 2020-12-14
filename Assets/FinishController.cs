using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishController : MonoBehaviour
{
    public GameObject winDisplayer;
    public GameObject loseDisplayer;
    public Text killText;
    public Text timeText;
    public static bool isGameFinished;
    public static FinishController instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void GotoMainMenu(){
        SceneManager.LoadScene("MainMenuScene");
    }

    public void DisplayFinish(bool isWin, int killCount, int secondCount){
        isGameFinished = true;
        killText.text = "Your Kill: " + killCount.ToString();
        timeText.text = "Time Elapsed: " + ((int)secondCount/60).ToString() + ":" + (secondCount%60).ToString();
        gameObject.SetActive(true);
        if (isWin){
            winDisplayer.SetActive(true);
        } else {
            loseDisplayer.SetActive(true);
        }
    }


    
}
