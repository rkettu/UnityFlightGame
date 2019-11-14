using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    private static int points = 0;
    static Text scoreValue;
    public float timer = 0f;
    Text timerValue;
    // Start is called before the first frame update
    void Start()
    {
        timerValue = transform.FindChild("TimeText").GetComponent<Text>();
        scoreValue = transform.FindChild("ScoreText").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        //timer = timer + 1 / 30;
        timer += Time.deltaTime;
        string time = "Time: "+timer.ToString("F2");
        timerValue.text = time;
        Debug.Log(time);
       
    }

    public static void updateCoins()
    {
        points++;
        scoreValue.text = "Score: " + points;
    }

}
