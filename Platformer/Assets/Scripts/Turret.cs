using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform turret;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootRange = 5f;
    [SerializeField] private float shootAngle = 160f;
    [SerializeField] private float shootTemp = .5f;
    private Transform shootPoint;
    private Transform player;
    private bool shoot = false;


    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        shootPoint = turret.GetChild(0);
    }

    private void Start() {
        StartCoroutine("Shoot");
    }

    private void Update() {
        float distance = Vector2.Distance(turret.position, player.position);
        float angle = Vector2.Angle(Vector2.right, player.position - turret.position);
        if (distance < shootRange && angle <= 90 + shootAngle/2  && angle >= 90 - shootAngle/2 
        && turret.position.y >= player.position.y 
        && Physics2D.Linecast(shootPoint.position, player.position).collider.tag == "Player") {
            turret.rotation = Quaternion.Lerp(turret.rotation, Quaternion.AngleAxis(-angle, Vector3.forward), .3f);
            shoot = true;
        }
        else shoot = false;
    }

    IEnumerator Shoot() {
        while (true) {
            yield return new WaitUntil(() => shoot);
            while (shoot) {
                yield return new WaitForSeconds(shootTemp);
                Instantiate(bullet, shootPoint.position, turret.rotation);
            }
        }
    }

}
