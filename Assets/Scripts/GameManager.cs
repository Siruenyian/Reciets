using UnityEngine;


public enum StateType
{
    DEFAULT,      //Fall-back state, should never happen
    WAITING,      //waiting for other player to finish his turn
    STARTTURN,    //Once, on start of each player's turn
    PLAYING,      //My turn
    BUYING,       //Buying something new
    TURNOVER,
    GAMEOVER,
    GAMESTART,
    LOBBY,        //Player is in the lobby
    MENU,         //Player is viewing in-game menu
    OPTIONS       //player is adjusting game options
};


public class GameManager : MonoBehaviour
{
    [SerializeField] private Bar bar;
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject singleton = new GameObject("GameManager");
                    _instance = singleton.AddComponent<GameManager>();
                }

                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}