using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ogrocode : MonoBehaviour
{
    [SerializeField] private float vida = 100;
    [SerializeField] private HealthBarController healthBar;
    [SerializeField] private GameObject NV;
    [SerializeField] private GameObject PL;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if (vida <= 0)
        {
            Destroy(this.gameObject);
            NV.GetComponent<NVControler>().NV = NV.GetComponent<NVControler>().NV + 1;
            PL.GetComponent<PlayerController>().Victory = PL.GetComponent<PlayerController>().Victory - 1;
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
}
