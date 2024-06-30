using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    public TextMeshProUGUI lives_inc_powerup_text;
    public TextMeshProUGUI lives_dec_powerup_text;
    public TextMeshProUGUI score_inc_powerup_text;
    public TextMeshProUGUI all_enemies_killed_text;
    public TextMeshProUGUI enemies_spawned_text;
    public TextMeshProUGUI powerups_spawned_text;
    public GameObject main_menu;
    public GameObject how_to_play_screen;
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

    public void show_how_to_play_screen()
    {
        main_menu.gameObject.SetActive(false);
        how_to_play_screen.gameObject.SetActive(true);
        

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
            double probability = 0.01; 
            if (Random.value < probability)
            {
                    Instantiate(targets[4], new Vector3(Random.Range(-22, 22), 0, Random.Range(-21, 21)), Quaternion.identity);
            }
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

    public void Update_score_powerup()
    {
        this.score = this.score + 10000;
        scoreText.text = "Score: " + score;
        score_inc_powerup_text.gameObject.SetActive(true);
        StartCoroutine(HideTextAfterDelay(3.0f, score_inc_powerup_text));
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

    internal void add_lives()
    {
        this.lives = this.lives + 1;
        lives_inc_powerup_text.gameObject.SetActive(true);
        lives_text.text = "Lives: " + lives;
        StartCoroutine(HideTextAfterDelay(3.0f,lives_inc_powerup_text));
    }

    IEnumerator HideTextAfterDelay(float delay, TextMeshProUGUI powerup_text)
    {
        yield return new WaitForSeconds(delay);
        powerup_text.gameObject.SetActive(false); 

    }

    internal void remove_lives()
    {
        this.lives = this.lives - 2;
        lives_dec_powerup_text.gameObject.SetActive(true);
        lives_text.text = "Lives: " + lives;
        StartCoroutine(HideTextAfterDelay(3.0f, lives_dec_powerup_text));
    }

    internal void destory_all_enemies()
    {
       
        all_enemies_killed_text.gameObject.SetActive(true);
        StartCoroutine(HideTextAfterDelay(3.0f, all_enemies_killed_text));
    }

    internal void spawned_enemies()
    {
        enemies_spawned_text.gameObject.SetActive(true);
        StartCoroutine(HideTextAfterDelay(3.0f, enemies_spawned_text));
    }



    internal void spawn_boxes_powerup()
    {
        powerups_spawned_text.gameObject.SetActive(true);
        StartCoroutine(HideTextAfterDelay(3.0f, powerups_spawned_text));
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