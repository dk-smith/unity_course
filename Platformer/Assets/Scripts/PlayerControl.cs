using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	[SerializeField] private int health = 100;
	[SerializeField] private float moveForce = 150f;
	[SerializeField] private float maxSpeed = 5f;
	[SerializeField] private float jumpForce = 50f;
	[SerializeField] private float throwAngle = 50f;
	[SerializeField] private int grenades= 5;
	[SerializeField] private GameObject shootPoint = null;
	[SerializeField] private Transform groundCheck = null;
	[SerializeField] private LayerMask groundLayers = 0;
	[SerializeField] private GameObject grenade = null;
	private int direction = 1;
	private Rigidbody2D rb = null;
	public bool grounded = false;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	void Update() {
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, groundLayers);
		if (Input.GetKeyDown(KeyCode.Space) && grounded)
			Jump();

		float horizontal = Input.GetAxis("Horizontal");
		if (Mathf.Abs(horizontal) > Mathf.Epsilon) {
			direction = horizontal > 0 ? 1 : -1;
			rb.AddForce(Vector2.right * moveForce * Time.deltaTime * direction);
			if (Mathf.Abs(rb.velocity.x) >= maxSpeed) {
				rb.velocity = new Vector2(maxSpeed * direction, rb.velocity.y);
			}
		}
		else {
			rb.velocity = new Vector2(0, rb.velocity.y);
		}
		if (transform.localScale.x != direction) Flip();

		if (Input.GetKeyDown(KeyCode.F))
			shootPoint.GetComponent<Shooting>().Shoot();

		if (Input.GetKeyDown(KeyCode.E))
			ThrowGrenade();
	}

	void Flip() {
		transform.localScale = new Vector3(direction, 1, 1);
	}

	void Jump() {
		rb.AddForce(Vector2.up * jumpForce);
	}

	void ThrowGrenade() {
		if (grenades > 0) {
			grenades--;
			Instantiate(grenade, shootPoint.transform.position, Quaternion.identity);
			Debug.Log("GRENADES "+grenades);
		}
	}

	public void TakeDamage(int damage) {
        health -= damage;
        //if (health <= 0) Die();
    }

	private void Die() {
        Destroy(gameObject);
    }
}
