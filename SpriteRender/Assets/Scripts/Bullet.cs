using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public float damage = 10f;

    private Player player;

    private void Start()
    {
        // Configurar o Rigidbody2D
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Adicionar e configurar o Collider2D
        Collider2D collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<BoxCollider2D>();
        }
        collider.isTrigger = true;
    }

    public void SetDirection(Vector2 direction)
    {
        rb.velocity = direction * bulletSpeed;
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Lógica para aplicar dano ao inimigo
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            enemy.TriggerDamageAnimation();
            DestroyBullet();
        }
        else if (collision.CompareTag("Colisores"))
        {
            // Destruir a bala ao colidir com objetos com a tag "Colisores"
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        if (player != null)
        {
            player.ResetShootAnimation();
        }
        Destroy(gameObject);
    }
}
