using System;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button btnPlay;
    [SerializeField] private string ip = "20.153.150.46";
    [SerializeField] private string port = "9100";

    private void OnEnable()
    {
        btnPlay.onClick.AddListener(OnPlay);
    }

    private void OnDisable()
    {
        btnPlay.onClick.RemoveListener(OnPlay);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private async void OnPlay()
    {
        JoinServer();
        await Task.Delay(1000);
        //SceneManager.LoadScene(1);
    }

    public void JoinServer()
    {

        if (!ushort.TryParse(this.port, out ushort port))
        {
            Debug.LogWarning("Geçersiz port! Varsayýlan 7777 kullanýlýyor.");
            port = 7777;
        }

        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        transport.SetConnectionData(ip, port);

        Debug.Log($"Server'a baðlanýlýyor: {ip}:{port}");

        NetworkManager.Singleton.StartClient();
    }
}