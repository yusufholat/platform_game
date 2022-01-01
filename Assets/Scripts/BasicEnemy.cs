using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour, IEnemy {
    [SerializeField] float speed;
    [SerializeField] float distance;
    private bool movingRight = true;
    [SerializeField] Transform groundDetection;
    [SerializeField] LayerMask groundlayer;
    public int health = 100;

    private void Update() {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo =
        Physics2D.Raycast(groundDetection.position, Vector2.down, distance, groundlayer);

        if (groundInfo.collider == false) {
            if (movingRight == true) {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            } else {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) Destroy(gameObject);
    }
}
