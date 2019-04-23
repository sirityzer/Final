using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehavior : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
