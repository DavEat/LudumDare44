using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public Vector3 rotation = Vector3.up;

    void Update()
    {
        transform.Rotate(rotation);
    }
}
