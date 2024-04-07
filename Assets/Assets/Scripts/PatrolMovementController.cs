using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovementController : MonoBehaviour
{
    [SerializeField] private Transform[] checkpointsPatrol;
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float raycastDistance = 10f;
    [SerializeField] private float vida = 100;
    [SerializeField] private HealthBarController healthBar;
    [SerializeField] private GameObject NV;
    [SerializeField] private GameObject PL;
    private Transform currentPositionTarget;
    private int patrolPos = 0;
    private bool playerDetected = false;
    private void Start() {
        currentPositionTarget = checkpointsPatrol[patrolPos];
        transform.position = currentPositionTarget.position;
    }

    private void Update() {
        CheckNewPoint();
        if (playerDetected == false)
        {
            MoveAlongPatrol();
        }
        else
        {
            ChasePlayer();
        }
        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);
        if (vida <= 0)
        {
            Destroy(this.gameObject);
            NV.GetComponent<NVControler>().NV = NV.GetComponent<NVControler>().NV+1;
            PL.GetComponent<PlayerController>().Victory = PL.GetComponent<PlayerController>().Victory - 1;
        }
    }
    private void FixedUpdate()
    {
        // Determinar la dirección del raycast según la dirección de movimiento del enemigo
        Vector2 raycastDirection = myRBD2.velocity.normalized;

        // Realizar el raycast en la dirección determinada
        RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection, raycastDistance, playerLayer);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                playerDetected = true;
            }
        }
        else
        {
            playerDetected = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bullet"))
        {
            ReceiveDamage(10);
            vida = vida - 10;
        }
    }
    private void ReceiveDamage(int damageAmount)
    {
        healthBar.UpdateHealth(-damageAmount);
    }
    private void CheckNewPoint(){
        if(Mathf.Abs((transform.position - currentPositionTarget.position).magnitude) < 0.25){
            patrolPos = patrolPos + 1 == checkpointsPatrol.Length? 0: patrolPos+1;
            currentPositionTarget = checkpointsPatrol[patrolPos];
            myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized*velocityModifier;
            CheckFlip(myRBD2.velocity.x);
        }
        
    }
    private void MoveAlongPatrol()
    {
        myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized * velocityModifier;
    }

    private void ChasePlayer()
    {
        myRBD2.velocity = myRBD2.velocity.normalized * (velocityModifier * 2); 
    }

    private void CheckFlip(float x_Position){
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }
}
