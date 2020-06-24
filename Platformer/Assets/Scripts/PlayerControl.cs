using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
	[SerializeField] private int health = 100;
	[SerializeField] private float moveForce = 150f;
	[SerializeField] private float maxSpeed = 5f;
	[SerializeField] private float jumpForce = 50f;
	[SerializeField] private int grenades= 5;
	[SerializeField] private GameObject shootPoint = null;
	[SerializeField] private Transform groundCheck = null;
	[SerializeField] private LayerMask groundLayers = 0;
	[SerializeField] private GameObject grenade = null;
	private int direction = 1;
	private Rigidbody2D rb = null;
	public bool grounded = false;

	private void Awake() {
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullets"), LayerMask.NameToLayer("Player"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullets"), LayerMask.NameToLayer("EnemyBullets"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullets"), LayerMask.NameToLayer("Enemy"));
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"));
	}

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	void Update() {
		if (health <= 0) return;
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

		if (Input.GetButtonDown("Fire1"))
			shootPoint.GetComponent<Shooting>().Shoot();

		if (Input.GetKeyDown(KeyCode.F))
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
		}
	}

	public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
			health = 0;
			Die();
		}
    }

	private void Die() {
		GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = false;
		StartCoroutine("ReloadGame");
    }

	IEnumerator ReloadGame()
	{			
		yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}

	private void OnGUI() {
		GUIStyle style = new GUIStyle(GUI.skin.label);
		style.margin = new RectOffset(10, 10, 4, 4);
		style.fontStyle = FontStyle.Bold;
		GUILayout.BeginHorizontal(style);
		GUILayout.Label("Health: "+health, style);	
		GUILayout.Label("Ammo: "+shootPoint.GetComponent<Shooting>().getAmmo(), style);
		GUILayout.Label("Grenades: "+grenades, style);
		GUILayout.EndHorizontal();
	}
}
