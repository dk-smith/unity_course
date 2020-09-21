using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMoving : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * 0.05f);
    }
}
