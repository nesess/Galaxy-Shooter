using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text text;

    [SerializeField]
    private Text BestText;

    [SerializeField]
    private Text GameOver_Text;

    [SerializeField]
    private Text Restart_Text;

    [SerializeField]
    private Text Quit_Text;

    private Player player;
    [SerializeField]
    private Image _liveImg;

    [SerializeField]
    private Sprite[] _liveSprites;

    private int bestScore;



    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        bestScore = PlayerPrefs.GetInt("Score" , 0);
        BestText.text = "Best: " + bestScore;
    }

    
    void Update()
    {
        

        text.text = "Score: " + player.getScore();
        updateLives(player.getLife());
        if (Input.GetKeyDown(KeyCode.R) && player == null) 
        {
            SceneManager.LoadScene("Game");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    public void checkBestScore()
    {
        if (player.getScore() > bestScore)
        {
            bestScore = player.getScore();
            BestText.text = "Best: " + bestScore;
            PlayerPrefs.SetInt("Score", player.getScore());
        }
            
    }

    public void updateLives(int num)
    {
        if (player == null)
        {
            _liveImg.sprite = _liveSprites[0];
            GameOver_Text.gameObject.SetActive(true);
            Restart_Text.gameObject.SetActive(true);
            Quit_Text.gameObject.SetActive(true);


        }
        _liveImg.sprite = _liveSprites[num];
    }

    
    public void GameOverRoutineStarter()
    {
        StartCoroutine(GameOver());
    }
    private IEnumerator GameOver()
    {
        while(true)
        {
            GameOver_Text.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            GameOver_Text.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    

}
