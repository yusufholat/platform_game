using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour {
    [SerializeField] GameObject attackEffect;
    float timeBtwAttack;
    [SerializeField] float startTimeBtwAttack;

    [SerializeField] Transform attackPos;
    [SerializeField] LayerMask whatIsEnemy;
    [SerializeField] float attackRange;
    PlayerInputActions playerInputAction;

    private void Awake() {
        playerInputAction = new PlayerInputActions();
        playerInputAction.Player.Enable();
    }

    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (timeBtwAttack <= 0) {
            Debug.Log(playerInputAction.Player.Attack.ReadValue<float>());
            if (playerInputAction.Player.Attack.ReadValue<float>() > 0) {
                timeBtwAttack = startTimeBtwAttack;
                Instantiate(attackEffect, attackPos.position, Quaternion.identity);
            }

        } else timeBtwAttack -= Time.deltaTime;
    }

    public void Attack(InputAction.CallbackContext context) {
        Debug.Log("gird ");
        if (context.started && timeBtwAttack <= 0) {
            Debug.Log("vurd");

        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
