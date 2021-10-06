using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isSpecial = false;
    GameObject player;
    Animator anim;
    public AudioSource drum;
    public AudioSource bgm;
    public AudioSource talk;
    public AudioSource introTalk;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        player = GameObject.Find("Player");
        anim = player.GetComponentInChildren<Animator>();
    }
    public void StartG()
    {
        
        anim.SetTrigger("Start");
        bgm.volume = 0.4f;
        talk.Play();
        introTalk.Stop();

        Invoke("drum1", 1.7f);
        Invoke("StartGame", 3.0f);
        
    }

    // Start is called before the first frame update
    public void StartGame()
    {
        
        SceneManager.LoadScene("BetaProject");

    }

    public void drum1()
    { 
        drum.Play();
    }
}
