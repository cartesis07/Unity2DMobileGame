using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMissile : MonoBehaviour
{
    public GameObject missile;

    private bool isTriggered;

    private Transform levelPoint;

    private void Awake()
    {
        levelPoint = GameObject.FindGameObjectWithTag("EndLevel").GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTriggered)
        {
            Instantiate(missile, levelPoint.position, Quaternion.identity);
            isTriggered = true;
            StartCoroutine(Pause());
        }
    }

    private IEnumerator Pause()
    {
        yield return new WaitForSeconds(0.5f);
        isTriggered = false;
    }
}
