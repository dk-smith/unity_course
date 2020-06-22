using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bullet = null;
    [SerializeField] private int ammo = 40;

    public void Shoot() {
        if (ammo > 0) {
            Instantiate(bullet, transform.position, Quaternion.identity);
            ammo--;
            Debug.Log("AMMO "+ammo);
        }
    }
}
