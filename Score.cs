using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreBoard;
    string originalText;
    int scorePoints;

    // Start is called before the first frame update
    void Start()
    {
        scorePoints = 0;
        originalText = scoreBoard.text;
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    internal void AddPoints(int amount)
    {
        scorePoints += amount;
        scoreBoard.text = originalText +" " +scorePoints.ToString();
    }

    internal void RemovePoints(int amount)
    {
        scorePoints -= amount;
        scoreBoard.text += scorePoints.ToString();
    }
    
}
