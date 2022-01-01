using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyDead : MonoBehaviour, IEnemy {
    int health = 50;
    public GameObject flyEnemy;
    public void TakeDamage(int damage) {
        Debug.Log("damage taken");
        health -= damage;
        if (health <= 0) Destroy(flyEnemy);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.CompareTag("attackrange")) {
            TakeDamage(50);
        }
    }
}
