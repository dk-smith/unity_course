using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab = null;
    [SerializeField] private float delay = 3f;
    private GameObject enemy;
    public float timer = 0f;

    void FixedUpdate() {
        if (enemy == null) {
            timer -= Time.fixedDeltaTime;
            if (timer <= 0) Spawn();
        }    
    }

    void Spawn() {
        enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        timer = delay;
    }
}
