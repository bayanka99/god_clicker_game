using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager gameManager;
    private Button button;
    public int difficulty;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(set_difficulty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void set_difficulty()
    {
        gameManager.startgame(difficulty);
    }
}


