using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

	[SerializeField] private float speed = 5f;
	[SerializeField] private float jumpForce = 5f;
	[SerializeField] private GameObject shootPoint = null;
	private int direction = 1;
	private Rigidbody2D rb = null;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	void Update() {
		float horizontal = Input.GetAxis("Horizontal");

		if (Mathf.Abs(horizontal) > Mathf.Epsilon) {
			direction = horizontal > 0 ? 1 : -1;
			transform.Translate(Vector2.right * speed * Time.deltaTime * direction);
		}

		if (transform.localScale.x != direction) Flip();

		if (Input.GetKeyDown(KeyCode.F))
			shootPoint.GetComponent<Shooting>().Shoot();

		if (Input.GetKeyDown(KeyCode.Space))
			Jump();
	}

	void Flip() {
		transform.localScale = new Vector3(direction, 1, 1);
	}

	void Jump() {
		rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
	}
}
