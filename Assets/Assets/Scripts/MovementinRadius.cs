using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    [SerializeField] private Transform initialPosition;
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float vida = 100;
    [SerializeField] private HealthBarController healthBar;
    [SerializeField] private GameObject NV;
    [SerializeField] private GameObject PL;

    private bool playerDetected = false;
    private Rigidbody2D rb;
    private Vector2 initialPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPos = initialPosition.position;
    }

    void Update()
    {
        if (playerDetected)
        {
            MoveTowardsPlayer();
        }
        else
        {
            ReturnToInitialPosition();
        }
        if (vida <= 0)
        {
            Destroy(this.gameObject);
            NV.GetComponent<NVControler>().NV = NV.GetComponent<NVControler>().NV + 2;
            PL.GetComponent<PlayerController>().Victory = PL.GetComponent<PlayerController>().Victory - 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = true;
            ShootAtPlayer();
        }
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = false;
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = ((Vector2)player.position - rb.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    private void ReturnToInitialPosition()
    {
        Vector2 direction = (initialPos - (Vector2)transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        if (Vector2.Distance(transform.position, initialPos) < 0.01f)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void ShootAtPlayer()
    {
        // Asegura que ambos vectores sean tratados como Vector2 para la resta.
        Vector2 targetDirection = (Vector2)(player.position - (Vector3)projectileSpawnPoint.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.velocity = targetDirection * projectileSpeed;
    }

}




