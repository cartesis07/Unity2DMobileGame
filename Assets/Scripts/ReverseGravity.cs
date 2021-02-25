using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseGravity : MonoBehaviour
{
    public Rigidbody2D playerRb2D;
    public Rigidbody2D rigidStructure2D;

    private bool isTriggered;

    private void Awake()
    {
        playerRb2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        rigidStructure2D = GameObject.FindGameObjectWithTag("Structure").GetComponent<Rigidbody2D>();
        isTriggered = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTriggered)
        {
            if (playerRb2D.gravityScale == 1.0f)
            {
                playerRb2D.gravityScale = -1.0f;
                rigidStructure2D.gravityScale = -1.0f;
            }
            else
            {
                playerRb2D.gravityScale = 1.0f;
                rigidStructure2D.gravityScale = 1.0f;
            }
            isTriggered = true;
            StartCoroutine(Pause());
        }
    }

    public IEnumerator Pause()
    {
        yield return new WaitForSeconds(1f);
        isTriggered = false;
    }
}
