using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI Game_over_Text;
    public TextMeshProUGUI lives_text;
    public GameObject main_menu;
    public GameObject player;
    public int difficulty;
    public Button restart_button;
    public float spawnrate = 1.0f;
    public bool game_is_active;
    private float lives;
    private AudioSource ac;
    void Start()
    {
        
    }

    public void startgame(int difficulty )
    {
        this.difficulty=difficulty;
        game_is_active = true;
        player.gameObject.SetActive(true);

        score = 0;
        switch (difficulty)
        {
            case 1:
                lives = 5; break;
             case 2:
                lives=3; break;
            case 3:
                lives=1; break;
        }
        scoreText.text = "Score: " + score;
        lives_text.text = "Lives: " + lives;
        main_menu.gameObject.SetActive(false);
        lives_text.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        spawnrate /= (float)difficulty;
        StartCoroutine(spawn_shit());
        ac = GetComponent<AudioSource>();

        /*
        ac = GetComponent<AudioSource>();

        

      
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     IEnumerator spawn_shit()
    {
        while (game_is_active)
        {
            
            yield return new WaitForSeconds(spawnrate);
            int index = Random.Range(0, targets.Count);
            Vector3 spawnPosition = generate_position();
            // Instantiate(targets[index],spawnPosition, Quaternion.identity);
            GameObject newTarget = Instantiate(targets[index], spawnPosition, Quaternion.identity);
            newTarget.transform.LookAt(player.transform);
        }
    }

    private Vector3 generate_position()
    {
        int x = Random.Range(-25, 25);
        if(x<= 20 && x>=-20)
        {
            int z=(Random.Range(0, 2) == 0) ? -22 : 22;
            return new Vector3(x, 0, z);
        }
        return new Vector3(x, 0, Random.Range(-22, 22));


    }

    public void Update_score(float points, AudioClip click_sounds)
    {
        ac.PlayOneShot(click_sounds, 1.0f);
        score = score + points;
        scoreText.text = "Score: " + score;
    }

    public void edit_life(float number, Collider other)
    {
        lives = lives + number;
        lives_text.text = "Lives: " + lives;
        if (lives <= 0)
        {
            Destroy(other.gameObject);
            this.gameover();
        }
    }

    public void gameover()
    {
        
            Game_over_Text.gameObject.SetActive(true);
        
         restart_button.gameObject.SetActive(true);
        game_is_active = false;

    }

    public void restart_game()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}


/*
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> targets;
    public float spawnrate = 1.0f;

    
   
    private AudioSource ac;
    void Start()
    {



    }


    // Update is called once per frame
    void Update()
    {

    }

}
*/