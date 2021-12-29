using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDead : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Spike") || other.CompareTag("Enemy")) {
            SceneManager.LoadScene("Level1");
        }
    }
}
