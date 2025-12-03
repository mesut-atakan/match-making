using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float speed = 5.0f;

    private void Update()
    {
        // Sadece local player input alabilir
        if (!IsOwner)
            return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 moveVector = new Vector2(horizontal, vertical).normalized;

        // Hareket server tarafýndan yapýlmalý
        MoveServerRpc(moveVector);
    }

    [ServerRpc]
    private void MoveServerRpc(Vector2 movement)
    {
        // Server authoritative movement
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
