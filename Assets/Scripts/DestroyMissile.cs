using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMissile : MonoBehaviour
{
    public GameObject destroyParticlesGO;

    private ParticleSystem destroyParticles;

    public SpriteRenderer missileRenderer;

    public BoxCollider2D missileboxc;

    private GuidedMissile guidedmissile;

    private void Awake()
    {
        destroyParticles = destroyParticlesGO.GetComponent<ParticleSystem>();
        destroyParticles.Stop();
        missileRenderer = this.GetComponent<SpriteRenderer>();
        missileboxc = this.GetComponent<BoxCollider2D>();
        guidedmissile = this.GetComponent<GuidedMissile>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Blocks1") || collision.CompareTag("Border") || collision.CompareTag("Player") || collision.CompareTag("Grip") || collision.CompareTag("TriggerBlock"))
        {
            guidedmissile.missileRotateSpeed = 0f;
            guidedmissile.missileSpeed = 0f;
            missileboxc.enabled = false;
            missileRenderer.enabled = false;
            StartCoroutine(DestroyThisMissile());
        }
    }

    private IEnumerator DestroyThisMissile()
    {
        createParticles();
        yield return new WaitForSeconds(1f);
        GameObject.Destroy(this);
    }

    private void createParticles()
    {
        destroyParticles.Stop();
        if (destroyParticles.isStopped)
        {
            destroyParticles.Play(true);
            Debug.Log(destroyParticles.isPlaying);
        }
    }
}
