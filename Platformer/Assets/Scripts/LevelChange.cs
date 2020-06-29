using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    [SerializeField] private string scene;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            ChangeLevel(scene);
        }
    }

    private void ChangeLevel(string scene) {
        SceneManager.LoadScene(scene);
    }
}
