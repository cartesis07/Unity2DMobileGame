using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    public Player_Movement player;

    public Animator fadeSystem;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>();
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.LEVEL = 1;
            player.SavePlayer();
            StartCoroutine(loadMenu());
        }
    }

    public IEnumerator loadMenu()
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene("Menu");
        player.Menu();
    }
}
