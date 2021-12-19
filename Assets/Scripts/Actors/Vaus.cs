using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Vaus : MonoBehaviour
{
    public float horizontalSpeed = 150f;
    public List<Sprite> rackSprites;
    private Rigidbody2D rb;
    private SpriteRenderer sRender;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sRender = GetComponent<SpriteRenderer>();

        if (GameManager.Instance.playerObject == null)
        {
            GameManager.Instance.playerObject = gameObject;
        }
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        rb.velocity = Vector2.right * moveX * horizontalSpeed;
    }

    private void Update()
    {
        // Testing of other rack types
        if (Input.GetKeyDown(KeyCode.Alpha1)) { ChangeSprite(0); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { ChangeSprite(1); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { ChangeSprite(2); }
    }

    private void ChangeSprite(int id)
    {
        if (rackSprites[id] != null)
        {
            sRender.sprite = rackSprites[id];

            var _collider = GetComponent<BoxCollider2D>();

            _collider.size = new Vector2(sRender.bounds.size.x, sRender.bounds.size.y);
        }
    }
}
