
using UnityEngine;
using FishNet.Object;

public class PassGo : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Get the Player component from the collided object.
            Player player = other.gameObject.GetComponent<Player>();

            if (player != null && player.IsOwner)
            {
                // Call the server RPC to update the player's balance.
                UpdateBalanceServerRpc(player);
            }
        }
    }

    [ServerRpc]
    private void UpdateBalanceServerRpc(Player player)
    {
        player.balance += 200;
        Debug.Log("Player passed Go. Collected $200.");
    }
}