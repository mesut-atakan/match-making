using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Core;
using Unity.Services.Multiplay;
using UnityEngine;

public class MultiplayManager : MonoBehaviour
{
    private IServerQueryHandler serverQueryHandler;
    private float heartbeatTimer;

    private async void Start()
    {
        if (Application.platform != RuntimePlatform.LinuxServer)
            return;

        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;

        await UnityServices.InitializeAsync();

        var serverConfig = MultiplayService.Instance.ServerConfig;

        serverQueryHandler = await MultiplayService.Instance.StartServerQueryHandlerAsync(
            10, "MyServer", "MyGame", "MyBuild", "MyMap"
        );

        ushort port = 7777;

        if (!string.IsNullOrEmpty(serverConfig.AllocationId))
        {
            port = serverConfig.Port;
            Debug.Log($"[SERVER] Allocation detected. Using allocated port {port}");
        }
        else
        {
            Debug.LogWarning("[SERVER] No allocation detected. Using manual port 7777");
        }

        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        transport.SetConnectionData("0.0.0.0", port);

        NetworkManager.Singleton.StartServer();

        await MultiplayService.Instance.ReadyServerForPlayersAsync();
        Debug.Log("[SERVER] Ready for players!");

        NetworkManager.Singleton.SceneManager.LoadScene("GameScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
        Debug.Log("[SERVER] Game scene loading...");
    }

    private void Update()
    {
        if (Application.platform != RuntimePlatform.LinuxServer)
            return;

        heartbeatTimer += Time.deltaTime;

        if (heartbeatTimer >= 1f)
        {
            heartbeatTimer = 0f;

            serverQueryHandler.CurrentPlayers = (ushort)NetworkManager.Singleton.ConnectedClientsIds.Count;
            serverQueryHandler.UpdateServerCheck();
        }
    }
}