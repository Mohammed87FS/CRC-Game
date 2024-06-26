using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class MapGenerator : MonoBehaviour
{
    // Public variables
    public GameObject[] prefabTypes;
    public GameObject groundPrefab;
    public GameObject ballPrefab;
    public float spacing = 1f;

    // Private variables
    private bool isLevelWon = false;
    private int[,] currentLevel;
    private int currentLevelIndex = 1;
    private float levelTimer;
    private bool isGameStarted = false;

    // Event for map generation
    public delegate void MapGenerated();
    public event MapGenerated OnMapGenerated;

    // Level data 
    private int[,] LevelOne ={


        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 0, 0, 0, 1, 2, 2, 2, 2, 2, 0, 0, 0, 0, 1},
        {1, 0, 1, 0, 1, 2, 1, 1, 1, 2, 0, 1, 1, 0, 1},
        {1, 0, 1, 0, 0, 0, 1, 2, 1, 0, 0, 1, 2, 0, 1},
        {1, 1, 1, 0, 1, 1, 1, 2, 1, 1, 0, 1, 2, 0, 1},
        {1, 2, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 2, 0, 1},
        {1, 2, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 2, 0, 1},
        {4, 2, 2, 2, 2, 2, 0, 2, 0, 0, 0, 0, 2, 0, 3},
        {1, 1, 1, 1, 1, 2, 1, 2, 1, 1, 1, 1, 2, 1, 1},
        {1, 0, 0, 0, 0, 2, 0, 2, 0, 2, 2, 2, 2, 0, 1},
        {1, 0, 1, 1, 0, 1, 0, 2, 0, 1, 1, 1, 1, 0, 1},
        {1, 0, 1, 2, 2, 2, 0, 2, 0, 1, 2, 2, 2, 0, 1},
        {1, 0, 1, 2, 1, 1, 0, 2, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 1, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}

};
    private int[,] LevelTwo = {
    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    {1, 0, 0, 0, 1, 2, 2, 2, 2, 2, 0, 0, 0, 0, 1, 1, 0, 0, 0, 2, 2, 2, 2, 2, 0, 0, 0, 0, 2, 1},
    {1, 0, 1, 0, 1, 2, 1, 1, 1, 2, 0, 1, 1, 0, 1, 0, 1, 0, 1, 2, 1, 1, 1, 2, 0, 1, 1, 0, 0, 1},
    {1, 0, 1, 0, 0, 0, 1, 2, 1, 0, 0, 1, 2, 0, 1, 0, 0, 0, 1, 2, 1, 0, 0, 1, 2, 0, 1, 0, 1, 1},
    {1, 1, 1, 0, 0, 1, 1, 2, 0, 1, 0, 1, 2, 0, 1, 1, 1, 0, 1, 2, 1, 1, 0, 1, 2, 0, 1, 1, 0, 1},
    {1, 2, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 2, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 2, 0, 1, 0, 0, 1},
    {1, 2, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 2, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 2, 0, 1, 1, 1, 1},
    {1, 2, 2, 2, 2, 2, 0, 2, 0, 0, 0, 0, 2, 0, 1, 2, 2, 2, 2, 2, 0, 2, 0, 0, 0, 0, 2, 0, 1, 1},
    {1, 1, 1, 1, 1, 2, 1, 2, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 2, 1, 2, 1, 1, 1, 1, 2, 1, 1, 1},
    {1, 0, 0, 0, 0, 2, 0, 2, 0, 2, 2, 2, 2, 0, 1, 0, 0, 0, 0, 2, 0, 2, 0, 2, 2, 2, 2, 0, 0, 1},
    {1, 0, 1, 1, 0, 1, 0, 2, 0, 1, 1, 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 2, 0, 1, 1, 1, 1, 0, 2, 1},
    {1, 0, 1, 2, 2, 2, 0, 2, 0, 1, 2, 2, 2, 0, 1, 0, 1, 2, 2, 2, 0, 2, 0, 1, 2, 2, 2, 0, 2, 1},
    {1, 0, 1, 2, 1, 1, 0, 2, 0, 0, 0, 0, 0, 0, 1, 0, 1, 2, 1, 1, 0, 2, 0, 0, 0, 0, 0, 0, 0, 1},
    {1, 0, 0, 0, 1, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 2, 2, 2, 2, 1, 1, 1, 1, 1, 0, 1},
    {4, 2, 2, 2, 2, 2, 2,2 , 2, 2, 2, 2, 2,2 , 2,2 , 2, 2, 2, 2, 2, 2, 2, 2, 0, 2, 2, 2, 0, 3},
    {1, 0, 0, 0, 1, 2, 2, 2, 2, 2, 0, 0, 0, 0, 1, 0, 0, 0, 1, 2, 2, 2, 2, 2, 0, 0, 0, 0, 2, 1},
    {1, 0, 1, 0, 1, 2, 1, 1, 1, 2, 0, 1, 1, 0, 1, 0, 1, 0, 1, 2, 1, 1, 1, 2, 0, 1, 1, 0, 2, 1},
    {1, 0, 1, 0, 0, 0, 1, 2, 1, 0, 0, 1, 2, 0, 0, 0, 0, 0, 1, 2, 1, 0, 0, 1, 2, 0, 1, 0, 1, 1},
    {1, 1, 1, 0, 1, 1, 1, 2, 1, 1, 0, 1, 2, 0, 0, 1, 1, 0, 1, 2, 1, 1, 0, 1, 2, 0, 1, 1, 1, 1},
    {1, 2, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 2, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 2, 0, 1, 0, 0, 1},
    {1, 2, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 2, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 2, 0, 1, 1, 1, 1},
    {1, 2, 2, 2, 2, 2, 0, 2, 0, 0, 0, 0, 2, 0, 1, 2, 2, 2, 2, 2, 0, 2, 0, 0, 0, 0, 2, 0, 1, 1},
    {1, 1, 1, 1, 1, 2, 1, 2, 1, 0, 1, 1, 2, 1, 0, 1, 1, 1, 1, 2, 1, 2, 1, 1, 1, 1, 2, 1, 1, 1},
    {1, 0, 0, 0, 0, 2, 0, 2, 0, 2, 2, 2, 2, 0, 1, 0, 0, 0, 0, 2, 0, 2, 0, 2, 2, 2, 2, 0, 0, 1},
    {1, 0, 1, 1, 0, 1, 0, 2, 0, 1, 1, 1, 1, 0, 1, 0, 1, 1, 0, 1, 0, 2, 0, 1, 1, 1, 1, 0, 1, 1},
    {1, 0, 1, 2, 2, 2, 0, 2, 0, 1, 2, 2, 2, 0, 1, 0, 1, 2, 2, 2, 0, 2, 0, 1, 2, 2, 2, 0, 1, 1},
    {1, 0, 1, 2, 1, 1, 0, 2, 0, 0, 0, 0, 0, 0, 1, 0, 1, 2, 1, 1, 0, 2, 0, 0, 0, 0, 0, 0, 0, 1},
    {1, 0, 0, 0, 1, 2, 2, 2, 2, 1, 1, 0, 1, 1, 1, 0, 0, 0, 1, 2, 2, 2, 2, 1, 1, 1, 1, 1, 0, 1},
    {1, 0, 1, 2, 0, 0, 2, 2, 2, 0, 0, 1, 0, 1, 1, 0, 2, 0, 0, 1, 0, 1, 2, 0, 0, 0, 1, 0, 2, 1},
    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
};

    // Lists to hold different types of blocks
    private List<GameObject> allBlocks = new List<GameObject>();
    private List<GameObject> type2Blocks = new List<GameObject>();
    private List<GameObject> type3Blocks = new List<GameObject>();
    private List<GameObject> type4Blocks = new List<GameObject>();



    private void Start()
    {
        LoadLevel(LevelOne);
    }



    // Load a level and generate the map
    void LoadLevel(int[,] level)
    {
        currentLevel = level;
        GenerateMap();
        SetLevelTimer();
        OnMapGenerated?.Invoke();

        // Adjust the camera after the map is generated
        CameraController cameraController = FindObjectOfType<CameraController>();
        if (cameraController != null)
        {
            cameraController.AdjustCamera();
        }
    }

    // Set the timer for the current level
    void SetLevelTimer()
    {
        levelTimer = (currentLevelIndex == 1) ? 20f : 60f;
        UpdateUIManagerTimer();
    }

    // Update the timer in the UI
    void UpdateUIManagerTimer()
    {
        UIManager uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.SetLevelDuration(GetLevelDuration());
        }
    }

    // Generate the map based on the current level data
    void GenerateMap()
    {
        ClearPreviousMap();
        float groundWidth = currentLevel.GetLength(0) * spacing;
        float groundHeight = currentLevel.GetLength(1) * spacing;
        float minY = float.MaxValue;

        for (int i = 0; i < currentLevel.GetLength(0); i++)
        {
            for (int j = 0; j < currentLevel.GetLength(1); j++)
            {
                int prefabIndex = currentLevel[i, j];
                if (prefabIndex != 0)
                {
                    Vector3 position = new Vector3(i * spacing, 0, j * spacing);
                    GameObject block = Instantiate(prefabTypes[prefabIndex - 1], position, Quaternion.identity);
                    block.AddComponent<Rigidbody>().isKinematic = true;
                    block.AddComponent<BoxCollider>();

                    allBlocks.Add(block);

                    // Categorize blocks based on their type
                    if (prefabIndex == 2)
                    {
                        type2Blocks.Add(block);
                    }
                    else if (prefabIndex == 3)
                    {
                        type3Blocks.Add(block);
                    }
                    else if (prefabIndex == 4)
                    {
                        type4Blocks.Add(block);
                    }

                    if (block.transform.position.y < minY)
                    {
                        minY = block.transform.position.y;
                    }
                }
            }
        }


        GameObject ground = Instantiate(groundPrefab, new Vector3(groundWidth / 2 - spacing / 2, minY - 1.0f, groundHeight / 2 - spacing / 2), Quaternion.identity);
        ground.transform.localScale = new Vector3(groundWidth, 1, groundHeight);
    }

    // Clear the previous map before generating a new one
    void ClearPreviousMap()
    {
        foreach (GameObject block in allBlocks)
        {
            Destroy(block);
        }
        allBlocks.Clear();
        type2Blocks.Clear();
        type3Blocks.Clear();
        type4Blocks.Clear();
    }


    private void Update()
    {
        if (!isGameStarted)
        {
            DialogSystem dialogSystem = FindObjectOfType<DialogSystem>();
            if (dialogSystem != null && dialogSystem.IsGameStarted())
            {
                isGameStarted = true;
            }
            else
            {
                return;
            }
        }

        MoveType2Blocks();
        UpdateLevelTimer();
        CheckWinCondition();
    }


    // Move type2 blocks based on input (using Linq)
    void MoveType2Blocks()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W))
        {
            moveDirection = Vector3.forward * spacing;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            moveDirection = Vector3.back * spacing;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            moveDirection = Vector3.left * spacing;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            moveDirection = Vector3.right * spacing;
        }

        if (moveDirection != Vector3.zero)
        {
            var occupiedPositions = new HashSet<Vector3>(allBlocks.Select(block => block.transform.position));

            var movableBlocks = type2Blocks
                .Where(block => !occupiedPositions.Contains(block.transform.position + moveDirection))
                .ToList();

            foreach (GameObject block in movableBlocks)
            {
                Rigidbody rb = block.GetComponent<Rigidbody>();
                rb.MovePosition(block.transform.position + moveDirection);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBall();
        }
    }

    // Shoot a ball from type4 blocks
    void ShootBall()
    {
        foreach (GameObject shooter in type4Blocks)
        {
            GameObject ball = Instantiate(ballPrefab, shooter.transform.position, Quaternion.identity);
            Rigidbody rb = ball.AddComponent<Rigidbody>();
            rb.useGravity = false;
            BallController ballController = ball.AddComponent<BallController>();
            ballController.SetTarget(type3Blocks, OnBallCollision, type2Blocks);
        }
    }

    // Handle ball collisions
    void OnBallCollision(GameObject ball, GameObject target)
    {
        if (type3Blocks.Contains(target))
        {
            Destroy(ball);
            isLevelWon = true;
            CheckWinCondition();
        }
        else if (type2Blocks.Contains(target))
        {
            Destroy(ball);
        }
    }

    // Update the level timer
    void UpdateLevelTimer()
    {
        if (levelTimer > 0)
        {
            levelTimer -= Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    // Check if the win condition is met
    void CheckWinCondition()
    {
        if (IsLevelWon() && currentLevelIndex == 1)
        {
            LoadNextLevel();
            isLevelWon = false;
        }
        else if (IsLevelWon() && currentLevelIndex == 2)
        {
            SceneManager.LoadScene("WinScene");
        }
    }

    // Check if the current level is won
    bool IsLevelWon()
    {
        return isLevelWon;
    }

    // Load the next level
    void LoadNextLevel()
    {
        currentLevelIndex++;
        if (currentLevelIndex == 2)
        {
            LoadLevel(LevelTwo);
        }
    }

    // Get the current level timer
    public float GetLevelTimer()
    {
        return levelTimer;
    }

    // Get the duration of the current level
    public float GetLevelDuration()
    {
        return currentLevelIndex == 1 ? 20f : 60f;
    }

    // Start the game
    public void StartGame()
    {
        isGameStarted = true;
    }

    // Get the width of the current map
    public int GetMapWidth()
    {
        return currentLevel.GetLength(0);
    }

    // Get the height of the current map
    public int GetMapHeight()
    {
        return currentLevel.GetLength(1);
    }
}
