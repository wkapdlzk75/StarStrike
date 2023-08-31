using UnityEngine;

public class Item : MonoBehaviour
{
    public string type;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * 0.5f;
    }
}
