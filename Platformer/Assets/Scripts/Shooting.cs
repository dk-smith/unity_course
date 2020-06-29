using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bullet = null;
    [SerializeField] private int ammo = 40;
    private AudioSource audioSource = null;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        audioSource.time = 0.2f;
    }

    public void Shoot() {
        if (ammo > 0) {
            audioSource.Play();
            Instantiate(bullet, transform.position, Quaternion.identity);
            ammo--;
        }
    }

    public int getAmmo() {
        return ammo;
    }
}
