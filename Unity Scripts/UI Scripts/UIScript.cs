using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    private static int points = 0;
    static Text scoreValue;
    private float timer = 0f;
	private int minutes = 0;
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
		// Updating timer value...
		timer += Time.deltaTime;
        if(timer > 60f)
        {
            timer -= 60f;
            minutes++;
        }
        timerValue.text = "Time: " + minutes + ":" + time.ToString("F2");
    }

	// Updates UI with +1 coin collected
	// Called from Coin's script
    public static void updateCoins()
    {
        points++;
        scoreValue.text = "Score: " + points;
    }

}
