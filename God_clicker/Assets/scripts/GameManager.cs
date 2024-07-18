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
    public List<AudioClip> zombie_sounds;
    public List<AudioClip> zombie_dead_sounds;
    

    private float score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI Game_over_Text;
    public TextMeshProUGUI tutorial_text;
    public TextMeshProUGUI lives_text;
    public TextMeshProUGUI lives_inc_powerup_text;
    public TextMeshProUGUI lives_dec_powerup_text;
    public TextMeshProUGUI score_inc_powerup_text;
    public GameObject pause_game_screen;
    public TextMeshProUGUI all_enemies_killed_text;
    public TextMeshProUGUI enemies_spawned_text;
    public TextMeshProUGUI powerups_spawned_text;
    public GameObject main_menu;
    public GameObject credits_screen;
    public GameObject how_to_play_screen;
    public GameObject indicator;
    public GameObject ground;
    public GameObject player;
    public int difficulty;
    public bool game_is_paused = false;
    public Button restart_button;
    public float spawnrate = 1.0f;
    public bool game_is_active;
    public Material easy_material;
    public Material med_material;
    public Material hard_material;

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


    public void show_credits_screen()
    {
        main_menu.gameObject.SetActive(false);
        credits_screen.gameObject.SetActive(true);


    }

    public void startgame(int difficulty)
    {
       
        this.difficulty=difficulty;
        game_is_active = true;
        game_is_paused = false;
        player.gameObject.SetActive(true);
        

        score = 0;
        switch (difficulty)
        {
            case 1:
                this.ground.GetComponent<Renderer>().material = easy_material;
                lives = 10; break;
             case 2:
                this.ground.GetComponent<Renderer>().material = med_material;
                lives =5; break;
            case 3:
                this.ground.GetComponent<Renderer>().material = hard_material;
                lives =3; break;
        }
        scoreText.text = "Score: " + score;
        lives_text.text = "Lives: " + lives;
        main_menu.gameObject.SetActive(false);
        lives_text.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        spawnrate /= (float)difficulty;
 
        StartCoroutine(SpawnAndPause());


        
        StartCoroutine(spawn_shit());
        ac = GetComponent<AudioSource>();
     


    }

    private IEnumerator SpawnAndPause()
    {
  
        yield return new WaitForSeconds(1f);

         int sound_index = Random.Range(0, 3);
            

            
        Vector3 spawnPosition = new Vector3(-11, 0, 10);
        // Instantiate(targets[index],spawnPosition, Quaternion.identity);
        GameObject newTarget = Instantiate(targets[0], spawnPosition, Quaternion.identity);
        GameObject indicator2 = Instantiate(this.indicator, newTarget.transform.position - new Vector3(0, 0, 0), Quaternion.identity);
        indicator2.SetActive(true);
        newTarget.transform.LookAt(player.transform);
        tutorial_text.gameObject.SetActive(true);
        ac.PlayOneShot(zombie_sounds[sound_index], 1.0f);
        // Pause the game
        Time.timeScale = 0;

        // Wait for player interaction
        while (newTarget!=null)
        {
            yield return null; // Wait until the player clicks
        }
        indicator2.SetActive(false);
        tutorial_text.gameObject.SetActive(false);

        // Resume the game
        Time.timeScale = 1;

    }

    // Update is called once per frame
    void Update()
    {

        
        if (Input.GetKeyDown(KeyCode.Space))
       {
            if(this.game_is_paused)
            {
                Time.timeScale = 1;
                this.game_is_paused = false;
                pause_game_screen.gameObject.SetActive(false);
            }
            else
            {

                Time.timeScale = 0;
                this.game_is_paused = true;
                pause_game_screen.gameObject.SetActive(true);
            }

            
            
        }

    }



    public void destroy_blood(GameObject blood_stains)
    {
        StartCoroutine(DestroyBloodStainsAfterDelay(blood_stains));
    }

    private IEnumerator DestroyBloodStainsAfterDelay(GameObject bloodStains)
    {
        yield return new WaitForSeconds(10);

        // Check if blood stains GameObject is valid before destroying
        if (bloodStains != null)
        {
            Destroy(bloodStains);
        }
    }

    IEnumerator spawn_shit()
    {
        while (game_is_active)
        {
            
            yield return new WaitForSeconds(spawnrate);
            int index = Random.Range(0, 2);
            int sound_index = Random.Range(0, 3);
            
            Vector3 spawnPosition = generate_position();
            // Instantiate(targets[index],spawnPosition, Quaternion.identity);
            GameObject newTarget = Instantiate(targets[index], spawnPosition, Quaternion.identity);
            ac.PlayOneShot(zombie_sounds[sound_index], 1.0f);
            newTarget.transform.LookAt(player.transform);
            double probability = 0.09; 
            //double probability = 1;

            if (Random.value < probability)
            {
                    Instantiate(targets[2], new Vector3(Random.Range(-20, 20), 0, Random.Range(-10, 10)), Quaternion.identity);
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

    public void Update_score(float points)
    {
        int sound_index = Random.Range(0, 3);
        ac.PlayOneShot(zombie_dead_sounds[sound_index], 1.0f);
        
        score = score + points;
        scoreText.text = "Score: " + score;
    }

    public void edit_life(float number, Collider other, AudioClip bite_sound)
    {
        lives = lives + number;
        lives_text.text = "Lives: " + lives;
        ac.PlayOneShot(bite_sound, 1.0f);
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
        if(Time.timeScale ==0)
        {
            Time.timeScale = 1;
        }
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