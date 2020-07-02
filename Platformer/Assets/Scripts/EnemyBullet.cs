using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private int damage = 8;

    void Update() {
        transform.Translate(Vector2.right * transform.localScale.x * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<PlayerControl>().TakeDamage(damage);
        }   
        Destroy(gameObject);
    }
}
