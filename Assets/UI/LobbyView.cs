using FishNet;
using FishNet.Object.Synchronizing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class LobbyView : View
{
    [SerializeField]
    private TMP_Text playerList;

    [SerializeField]
    private Button readyButton;

    [SerializeField]
    private TMP_Text readyButtonText;

    [SerializeField]
    private Button startButton;

    [SerializeField]
    private Button exitButton;

    [SerializeField]
    public TMP_InputField usernameInput;

    [SerializeField]
    public TMP_Text usernameText;

    [SerializeField]
    private Button submitButton;

    [SyncVar]
    public string playername;


    public override void Initialize()
    {
        submitButton.onClick.AddListener(() => 
        {
            if (usernameInput.text != "") 
            {   
                playername = usernameInput.text;
                usernameText.text = $"Welcome, {usernameInput.text}!\n<size=20>\nPlease click Ready and then Start</size>";
            }
        });
         
       


        readyButton.onClick.AddListener(() => Player.Instance.IsReady = !Player.Instance.IsReady);

        if (InstanceFinder.IsServer)
        {
            startButton.onClick.AddListener(() => GameManager.Instance.StartGame());
        }
        else
        {
            startButton.gameObject.SetActive(false);
        }

        base.Initialize();
    }

    private void Update()
    {
        if (!IsInitialized) return;

        string playerListText = "";

        int playerCount = GameManager.Instance.Players.Count;

        for (int i = 0; i < GameManager.Instance.Players.Count; i++)
        {
            Player player = GameManager.Instance.Players[i];

            playerListText += $"Player {player.OwnerId} (IS Ready: {player.IsReady})";

            if (i < playerCount - 1)
            {
                playerListText += "\r\n";
            }
        }

        playerList.text = playerListText;

        exitButton.onClick.AddListener(Application.Quit);

        readyButtonText.color = Player.Instance.IsReady ? Color.green : Color.red;

        startButton.interactable = GameManager.Instance.CanStart;
    }
}
