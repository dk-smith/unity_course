using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveForce = 150f;
    [SerializeField] private float maxSpeed = 2f;
    [SerializeField] private float attackForce = 180f;
    [SerializeField] private float attackMaxSpeed = 3.5f;
    [SerializeField] private int health = 100;
    [SerializeField] private int damage = 5;
    [SerializeField] private float spotDistance = 8f;
    [SerializeField] private float meeleDistance = 4f;
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private Transform groundPoint = null;
    [SerializeField] private LayerMask groundLayers = 0;
    [SerializeField] private GameObject bullet = null;
    [SerializeField] private float shootTemp = .5f;
	public bool grounded = false;
    private sbyte direction = 1;
    private Transform player;
    private Rigidbody2D rigidBody = null;
    private bool attack = false;
    private bool shoot = false;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        Flip(System.Convert.ToSByte(Random.Range(0, 10) % 2 == 0 ? 1 : -1));
        StartCoroutine("Shoot");
    }
    
    private void Update() {
        Debug.DrawLine(shootPoint.position, new Vector2(shootPoint.position.x, groundPoint.position.y));
        grounded = Physics2D.Linecast(transform.position, groundPoint.position, groundLayers);
        float currentMaxSpeed = maxSpeed;
        float distance = Vector2.Distance(shootPoint.position, player.position);
        if (distance < spotDistance 
            && player.position.y >= transform.position.y - 1.5f
            && player.position.y <= transform.position.y + 1.5f) {
            attack = true;
            currentMaxSpeed = attackMaxSpeed;
            if ((player.position.x < transform.position.x && direction == 1) 
                || (player.position.x > transform.position.x && direction == -1)) Flip();
        } else { 
            attack = false;
            shoot = false;
        }

        RaycastHit2D fallCheck = Physics2D.Linecast(shootPoint.position, new Vector2(shootPoint.position.x, groundPoint.position.y));
        if (!attack && grounded) {
            if (!fallCheck) {
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
                Flip();
            }
            else rigidBody.AddForce(Vector2.right * direction * moveForce * Time.deltaTime);
        }
        else if (attack && grounded) {
            if (!fallCheck) {
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
                shoot = true;
            }
            else {
                if (distance > meeleDistance) shoot = true; else shoot = false;
                rigidBody.AddForce(Vector2.right * direction * attackForce * Time.deltaTime);
            }
        }

        if (Mathf.Abs(rigidBody.velocity.x) >= currentMaxSpeed)
            rigidBody.velocity = new Vector2(currentMaxSpeed * direction, rigidBody.velocity.y);
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

    void Flip(sbyte newDirection) {
        direction = newDirection;
        transform.localScale = new Vector3(direction, 1, 1);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<PlayerControl>().TakeDamage(damage);
            rigidBody.AddForce(Vector2.left * direction * 80f);
        } else Flip();
    }

    IEnumerator Shoot() {
        while (true) {
            yield return new WaitUntil(() => shoot);
            while (shoot) {  
                Transform newBullet = Instantiate(bullet, shootPoint.position, Quaternion.identity).transform;
                newBullet.localScale = new Vector2(direction, 1);
                yield return new WaitForSeconds(shootTemp);
                //Physics2D.IgnoreCollision(newBullet.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
            }
        }
    }

}
