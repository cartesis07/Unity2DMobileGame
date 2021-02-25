using System.Collections;
using UnityEngine;

//utiliser un package particulier pour gérer les scènes
using UnityEngine.SceneManagement;

public class LoadFirstScene : MonoBehaviour
{
    public bool isAlreadyTriggered = false;

    public Animator fadeSystem;

    public Light lght;

    public Rigidbody2D playerRb2D;

    public Player_Movement playerMovement;

    public string levelstring;

    private void Awake()
    {
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
        lght = GameObject.FindGameObjectWithTag("Light").GetComponent<Light>();
        playerRb2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>();
        lght.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isAlreadyTriggered)
        {
            levelstring = "Level" + playerMovement.LEVEL.ToString();
            playerMovement.SavePlayer();
            StartCoroutine(loadNextScene());
            isAlreadyTriggered = true;
        }
    }

    public IEnumerator loadNextScene()
    {
        lght.enabled = true;
        yield return new WaitForSeconds(0.2f);
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1.1f);
        playerMovement.DisplayLvl();
        ResetGravity();
        SceneManager.LoadScene(levelstring);
        isAlreadyTriggered = false;
    }

    private void ResetGravity()
    {
        if (playerRb2D.gravityScale == -1.0f)
        {
            playerRb2D.gravityScale = 1.0f;
        }
    }
}
