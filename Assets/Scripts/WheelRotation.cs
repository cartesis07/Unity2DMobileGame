using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    public Transform transform;

    void FixedUpdate()
    {
        transform.Rotate(0f, 0f, 2f, Space.Self);
    }
}
