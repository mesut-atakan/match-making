using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 moveVector = new Vector2(horizontal, vertical).normalized;
        transform.Translate(moveVector * speed * Time.deltaTime);
    }
}
