using UnityEngine.UI;
using UnityEngine;

public class ScoreMonitor : MonoBehaviour
{
    public IntVariable marioScore;
    public Text text;
    public void UpdateScore()
    {   
        Debug.Log("Score +1");
        text.text = "Score: " + marioScore.Value.ToString();
    }

    public void Start()
    {
        UpdateScore();
    }

    void OnApplicationQuit()
    {
	    marioScore.SetValue(0);
    }
}