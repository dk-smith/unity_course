using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Remover : MonoBehaviour
{
	[SerializeField] private int damage = 300;

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Player")
		{
			col.gameObject.GetComponent<PlayerControl>().TakeDamage(damage);
		}
		else if(col.gameObject.tag == "Enemy")
		{
			col.gameObject.GetComponent<Enemy>().TakeDamage(damage);
		}
		else
		{
			Destroy(col.gameObject);	
		}
	}
}
