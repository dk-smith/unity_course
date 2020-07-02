using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private int damage = 80;
    [SerializeField] private float throwForce = 40f;
    [SerializeField] private float radius = 3f;
    [SerializeField] private float expForce = 50f;
    [SerializeField] private float expTime = 3f;
    [SerializeField] private AudioClip clip = null;

    private void Start() {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        float speed = Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.x);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(player.localScale.x, Mathf.Sin(Mathf.Deg2Rad * 30f)) * (throwForce + speed));// * Mathf.Sin(Mathf.Deg2Rad * 30f));
        GetComponent<Rigidbody2D>().AddTorque(2f);
        Invoke("Explode", expTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy") {
            Explode();
        }
    }

    private void Explode() {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, radius)) {
            Rigidbody2D rigidBody = collider.GetComponent<Rigidbody2D>();
            if (rigidBody != null)
                rigidBody.AddForce((collider.transform.position - transform.position).normalized * expForce);
            if (collider.gameObject.tag == "Enemy")
                collider.GetComponent<Enemy>().TakeDamage(damage);
        }
        GameObject explosion = new GameObject("Explosion", typeof(AudioSource));
        AudioSource audio = explosion.GetComponent<AudioSource>();
        audio.clip = clip;
        audio.time = 0.4f;
        Destroy(explosion, audio.clip.length - audio.time);
        audio.Play();
        Destroy(gameObject);
    }


}
