using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GuidedMissile : MonoBehaviour
{

    public Transform target;

    private Rigidbody2D missileRb2D;

    public float missileSpeed;

    public float missileRotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        missileSpeed = Random.Range(3f, 7f);
        missileRotateSpeed = Random.Range(250f, 320f);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        missileRb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.position - missileRb2D.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        missileRb2D.angularVelocity = -rotateAmount * missileRotateSpeed;

        missileRb2D.velocity = transform.up * missileSpeed;
    }
}
