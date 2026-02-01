using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private PlayerController playerController;
    private void Awake()
    {
        // Implement singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController not found in the scene.");
        }
        

    }

    public void StartGame()
    {
        Debug.Log("Game Started!");
        // Additional game start logic here
        //AudioManager.Instance.PlayMusic(1); // Play background music
    }

    public void EndGame()
    {
        Debug.Log("Game Ended!");
        // Additional game end logic here
        playerController.gameObject.SetActive(false); // Deactivate player
        //AudioManager.Instance.PlayMusic(1); // Play game over sound

    }

    private void StartMusicBasedOnscene() 
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0) 
        {
            AudioManager.Instance.PlayMusic(0); // Play menu music
        }
        else 
        {
            AudioManager.Instance.PlayMusic(1); // Play game music
        }
    }
}
