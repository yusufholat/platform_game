using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour {
    Vector2 startPos;
    [SerializeField] GameObject cam;
    [SerializeField] float parallaxEffect;

    void Start() {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update() {
        float distanceX = (cam.transform.position.x * parallaxEffect);
        float distanceY = (cam.transform.position.y * parallaxEffect);
        transform.position = new Vector3(startPos.x + distanceX, startPos.y + distanceY, transform.position.z);
    }
}
