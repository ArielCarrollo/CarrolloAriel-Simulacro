using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private float x_Movement;
    [SerializeField] private float y_Movement;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private HealthBarController healthBar;
    [SerializeField] public int Victory = 3;
    [SerializeField] private float vida = 100;

    private void Update() {
        
        myRBD2.velocity = new Vector2(x_Movement * velocityModifier, y_Movement * velocityModifier);

        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);

        Vector2 mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        CheckFlip(mouseInput.x);
    
        Debug.DrawRay(transform.position, mouseInput.normalized * rayDistance, Color.red);

        if(Input.GetMouseButtonDown(0)){
            ShootProjectile();
            Debug.Log("Right Click");
        }else if(Input.GetMouseButtonDown(1)){
            Debug.Log("Left Click");
        }
        if(Victory <= 0)
        {
            SceneManager.LoadScene("ganaste");
        }
        else if (vida <= 0)
        {
            SceneManager.LoadScene("perdiste");
        }
    }

    private void CheckFlip(float x_Position){
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        x_Movement = context.ReadValue<Vector2>().x;
        y_Movement = context.ReadValue<Vector2>().y;
    }
    private void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)projectileSpawnPoint.position;
        projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * velocityModifier;
    }
    private void ReceiveDamage(int damageAmount)
    {
        healthBar.UpdateHealth(-damageAmount);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemybullet"))
        {
            ReceiveDamage(10);
            vida = vida - 10;
        }
        else if (other.CompareTag("enemy"))
        {
            ReceiveDamage(10);
            vida = vida - 10;
        }
        else if (other.CompareTag("enemy2"))
        {
            ReceiveDamage(10);
            vida = vida - 10;
        }
    }
}
