using System.Collections;
using UnityEngine;

using UnityEngine.SceneManagement;


public class LoadSettings : MonoBehaviour
{

    public bool isAlreadyTriggered = false;

    public Animator fadeSystem;

    public Rigidbody2D playerRb2D;

    public Player_Movement playerMovement;

    private void Awake()
    {
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
        playerRb2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isAlreadyTriggered)
        {
            StartCoroutine(loadNextScene());
            isAlreadyTriggered = true;
        }
    }

    public IEnumerator loadNextScene()
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene("Settings");
        playerMovement.Settings();
        isAlreadyTriggered = false;
    }
}