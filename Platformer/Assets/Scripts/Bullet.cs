using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private int damage = 30;

    void Start() {
        transform.localScale = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.localScale.x, 1 ,1);
    }

    void Update() {
        transform.Translate(Vector2.right * transform.localScale.x * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy") {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }   
        Destroy(gameObject);
    }
}
