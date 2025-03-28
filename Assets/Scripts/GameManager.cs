using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance.
    public static GameManager instance;

    // List of characters in the game.
    public Character[] characters = new Character[1];

    // Make sure this is the only instance of this and it persists from scene to scene.
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
