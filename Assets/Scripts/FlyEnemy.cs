using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour {

    [SerializeField] float speed;
    private GameObject player;
    private bool chase = false;
    private Vector3 startingPoint;

    private void Start() {
        player = GameObject.Find("Player");
        startingPoint = transform.position;
    }

    private void Update() {
        if (player == null) return;

        if (chase == true)
            Chase();
        else returnStartingPoint();

        Flip();
    }

    private void Chase() {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void Flip() {
        if (transform.position.x > player.transform.position.x) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        } else {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }


    private void returnStartingPoint() {
        transform.position = Vector2.MoveTowards(transform.position, startingPoint, speed * 0.75f * Time.deltaTime);
        print(startingPoint);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            chase = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            chase = false;
        }
    }
}
