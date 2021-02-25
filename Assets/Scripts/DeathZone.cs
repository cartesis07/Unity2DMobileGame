using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour
{
    private Transform playerSpawn;
    private Animator fadeSystem;

    private SpriteRenderer srplayer;

    private Player_Movement player;

    private GameObject playerGameObject;

    private ParticleSystem destroyParticles;

    public Rigidbody2D playerRb2D;
    public Rigidbody2D rigidStructure2D;

    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("Player_Spawn").transform;
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        srplayer = playerGameObject.GetComponent<SpriteRenderer>();
        player = playerGameObject.GetComponent<Player_Movement>();
        playerRb2D = playerGameObject.GetComponent<Rigidbody2D>();
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
        destroyParticles = GameObject.FindGameObjectWithTag("DestroyParticules").GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && player.ISTRIGGER == false)
        {
            player.ISTRIGGER = true;
            srplayer.enabled = false;
            player.enabled = false;
            StartCoroutine(ReplacePlayer(collision));
        }
    }

    private IEnumerator ReplacePlayer(Collider2D _collision)
    {
        createParticles();
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1.2f);
        ResetGravity();
        DestroyMissiles();
        _collision.transform.position = playerSpawn.position;
        player.enabled = true;
        srplayer.enabled = true;
        player.ISTRIGGER = false;
    }

    private void createParticles()
    {
        destroyParticles.Stop();
        if (destroyParticles.isStopped)
        {
            destroyParticles.Play();
        }
    }

    private void DestroyMissiles()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Missile");
        foreach (GameObject enemy in enemies)
        {
            GameObject.Destroy(enemy);
        }
    }

    private void ResetGravity()
    {
        if (playerRb2D.gravityScale == -1.0f)
        {
            playerRb2D.gravityScale = 1.0f;
            rigidStructure2D.gravityScale = 1.0f;
        }
    }
}
