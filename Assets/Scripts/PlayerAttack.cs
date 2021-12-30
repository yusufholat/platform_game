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
    [SerializeField] int damage;

    private Animator anim;

    private void Awake() {
        playerInputAction = new PlayerInputActions();
        playerInputAction.Player.Enable();
        anim = GetComponent<Animator>();
    }

    void Update() {
        if (timeBtwAttack <= 0) {
            if (playerInputAction.Player.Attack.ReadValue<float>() > 0) {
                timeBtwAttack = startTimeBtwAttack;
            }
        } else timeBtwAttack -= Time.deltaTime;
    }

    public void Attack(InputAction.CallbackContext context) {
        if (context.started && timeBtwAttack <= 0) {
            anim.SetTrigger("attackTrigger");
            // Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
            // foreach (var enemy in enemies) {
            //     enemy.GetComponent<IEnemy>().TakeDamage(damage);
            // }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            Debug.Log("dddddddd");
            other.GetComponent<IEnemy>().TakeDamage(damage);
        }
    }
}
