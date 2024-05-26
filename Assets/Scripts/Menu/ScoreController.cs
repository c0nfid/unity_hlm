using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Lumin;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public int Score = 0;

    [SerializeField] private Text scorePoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scorePoints.text = "Score:   " + Score.ToString();
    }
}
