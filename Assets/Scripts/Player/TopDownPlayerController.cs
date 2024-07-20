using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerController : MonoBehaviour
{

    public Animator anim;
    public SpriteRenderer PlayerSprite;

    public Vector2 MovementSpeed = new Vector2(100.0f, 100.0f); 
    private Rigidbody2D _rigidbody2D;
    private Vector2 inputVector = new Vector2(0.0f, 0.0f);

    void Awake()
    {
        _rigidbody2D = gameObject.AddComponent<Rigidbody2D>();

        _rigidbody2D.angularDrag = 0.0f;
        _rigidbody2D.gravityScale = 0.0f;
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if(inputVector.x < 0)
        {
            PlayerSprite.flipX = true;
        }
        else if(inputVector.x > 0)
        {
            PlayerSprite.flipX = false;
        }

        if(inputVector.magnitude == 0)
        {
            anim.Play("Idle");
        }
        else
        {
            anim.Play("Run");
        }
    }

    void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + (inputVector * MovementSpeed * Time.fixedDeltaTime));
    }
}
