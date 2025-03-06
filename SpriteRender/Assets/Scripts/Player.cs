using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private Rigidbody2D rb;

    private Vector2 movement;

    private void Update()
    {
        // Captura a entrada do jogador
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        movement = new Vector2(moveX, moveY).normalized;
    }

    private void FixedUpdate()
    {
        // Move o jogador com base na entrada capturada
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
