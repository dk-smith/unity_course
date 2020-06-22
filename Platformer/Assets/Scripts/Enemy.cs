using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private float spotDistance = 8;
    [SerializeField] private Transform attackPoint = null;
    private int direction = 1;
    private Transform player;
    private Rigidbody2D rigidbody;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("Flip", 3f, 3f);
    }

    private void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        Flip();
    }
    
    private void Update() {
        float distance = Vector2.Distance(attackPoint.position, player.position);
        if (distance <= spotDistance) {
            Collider2D collider = Physics2D.Linecast(attackPoint.position, attackPoint.position + direction * attackPoint.right * spotDistance).collider;
            if( collider != null && collider.tag == "Player" ) {
                rigidbody.AddForce(Vector2.right * direction * 80f);
                if (Mathf.Abs(rigidbody.velocity.x) >= 2f)
				    rigidbody.velocity = new Vector2(2f * direction, rigidbody.velocity.y);
			}
        }
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) Die();
    }

    private void Die() {
        Destroy(gameObject);
    }

    void Flip() {
        direction *= -1;
		transform.localScale = new Vector3(direction, 1, 1);
	}
}
