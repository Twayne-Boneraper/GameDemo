using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public GameObject simpleTouch;
    public GameObject bagButton;
    public GameObject gameOverPanel;
    public GameObject hpSprite;
    public GameObject pauseButton;
    public GameObject joyStick;
    public GameObject dashButton;
    public GameObject player;
    //public AudioSource bgm;

    public Animator pausePanelAnimator;
    public AudioMixer bgmMixer;
    public Slider bgmSlider;

    //public GameObject hpSlider;
    
    //public Camera mainCamera;

    public bool isGameOver = false;

    private int playAudioTimes = 1;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //gameOver();
    }

    // Update is called once per frame
    void Update()
    {
        judgeGameOver();
        dropOutMap();
        if (!isGameOver)
        {
            playAudioTimes = 1;
        }
    }


    public void decreasePlayerHp()
    {
        hpSprite.GetComponent<hpSprite>().hpPrefabList[test.instance.hp - 1].SetActive(false);

        //test.instance.hp -= 20;
        test.instance.hp -= 1;
    }

    public void gameOver()
    {
        isGameOver = true;
        pauseButton.SetActive(false);
        simpleTouch.SetActive(false);
        dashButton.SetActive(false);
        bagButton.SetActive(false);
        gameOverPanel.SetActive(true);
        test.instance.bgmAudioSource.Pause();
        if (playAudioTimes == 1)
        {
            SoundManager.instance.PlayAudio(SoundManager.instance.gameOverClip);
            playAudioTimes++;
        }
        
        //test.instance.hp = 0;
        //hpSlider.SetActive(false);
        //test.instance.enabled = false;
        //test.instance.GetComponentInParent<Rigidbody2D>()
        //Time.timeScale = 0;
        //mainCamera.GetComponent<Die>().gameOverShader();
    }

    public void judgeGameOver()
    {
        if (test.instance.hp <= 0)
        {
            gameOver();
        }
    }

    public void pauseGame()
    {
        pausePanelAnimator.SetBool("showPausePanel",true);
        pausePanelAnimator.SetBool("hidePausePanel", false);
        bagButton.GetComponent<Button>().enabled = false;
        pauseButton.GetComponent<Button>().enabled = false;
        joyStick.SetActive(false);
        dashButton.SetActive(false);
        test.instance.bgmAudioSource.Pause();
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
        pausePanelAnimator.SetBool("hidePausePanel",true);
        pausePanelAnimator.SetBool("showPausePanel", false);
        bagButton.GetComponent<Button>().enabled = true;
        pauseButton.GetComponent<Button>().enabled = true;
        joyStick.SetActive(true);
        dashButton.SetActive(true);
        test.instance.bgmAudioSource.UnPause();
    }

    public void dropOutMap()
    {
        if (player.transform.position.y < -10f)
        {
            test.instance.hp = 0;
            hpSprite.SetActive(false);
        }
    }

    public void retryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void bgmVolum(float value)
    {
        //value=bgmSlider.value;
        bgmMixer.SetFloat("bgm", value);
    }

}
