using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button exitFromPauseButton;
    [SerializeField] private Button goHomeFromGameUIButton;
    [SerializeField] private Button goHomeFromLooseUIButton;
    [SerializeField] private Button goHomeFromWinUIButton;
    
    [Space][Header("Views")]
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject game;
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject loose;
    
    
    [Space] [Header("TxtInfo")]
    [SerializeField] private TextMeshProUGUI menuCoin;
    [SerializeField] private TextMeshProUGUI gameCoin;
    [SerializeField] private TextMeshProUGUI score;
    
    [SerializeField] private GameObject world;
    
    private GameObject _worldInstance ;
    
    
    public static UIManager Instance => _instance;
    private static UIManager _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    
    
    void Start()
    {
        startGameButton.onClick.AddListener(StartGame);
        
        pauseButton.onClick.AddListener((() =>
        {
            Time.timeScale = 0f;
            pause.SetActive(true);
        }));
        
        exitFromPauseButton.onClick.AddListener((() =>
        {
            Time.timeScale = 1f;
            pause.SetActive(false);
        }));
        
        goHomeFromGameUIButton.onClick.AddListener((() =>
        {
            menu.SetActive(true);
            menuCoin.text = PlayerPrefs.GetInt("Coins", 0).ToString();
            game.SetActive(false);
            Destroy(_worldInstance);
        }));
        
        goHomeFromLooseUIButton.onClick.AddListener((() =>
        {
            menu.SetActive(true);
            menuCoin.text = PlayerPrefs.GetInt("Coins", 0).ToString();
            game.SetActive(false);
            loose.SetActive(false);
            Destroy(_worldInstance);
        }));
        
        goHomeFromWinUIButton.onClick.AddListener((() =>
        {
            AddCoins();
            menu.SetActive(true);
            menuCoin.text = PlayerPrefs.GetInt("Coins", 0).ToString();
            game.SetActive(false);
            win.SetActive(false);
            Destroy(_worldInstance);
        }));
    }

    
    void StartGame()
    {
        PlayerPrefs.SetInt("RoundCoin", 0);
        UpdateRoundCoinInfo();
        menu.SetActive(false);
        game.SetActive(true);
        _worldInstance= Instantiate(world);
    }

    void AddCoins()
    {
        PlayerPrefs.SetInt("Coins",PlayerPrefs.GetInt("Coins",0)+PlayerPrefs.GetInt("RoundCoin",0));
    }

    public void UpdateRoundCoinInfo()
    {
        gameCoin.text = PlayerPrefs.GetInt("RoundCoin", 0).ToString();
    }
    
    public void WinGame()
    {
        score.text = "Score: " + PlayerPrefs.GetInt("RoundCoin", 0);
        win.SetActive(true);
    }
    
    public void LooseLGame()
    {
        loose.SetActive(true);
    }
}
