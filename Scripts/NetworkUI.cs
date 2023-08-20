using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : NetworkBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private TextMeshProUGUI playersCountText;

    private NetworkVariable<int> playersNum = new NetworkVariable<int>(0,NetworkVariableReadPermission.Everyone);
    // Update is called once per frame
    private void Awake()
    {
        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });
        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });

    }
    private void Update(){
        playersCountText.text = "Players: " + playersNum.Value.ToString();
        if (!IsServer)  return;
        playersNum.Value = NetworkManager.Singleton.ConnectedClients.Count;
        }
}
