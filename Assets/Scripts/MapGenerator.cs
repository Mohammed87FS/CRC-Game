using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] prefabTypes;
    public GameObject groundPrefab;
    public GameObject ballPrefab;
    public float spacing = 1f;
    private bool isLevelWon = false;
    private int[,] currentLevel;

    public delegate void MapGenerated();
    public event MapGenerated OnMapGenerated;
    private int currentLevelIndex = 1;
    private float levelTimer;
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

    private bool isGameStarted = false;

    private List<GameObject> allBlocks = new List<GameObject>();
    private List<GameObject> type2Blocks = new List<GameObject>();
    private List<GameObject> type3Blocks = new List<GameObject>();
    private List<GameObject> type4Blocks = new List<GameObject>();

    private void Start()
    {
        LoadLevel(LevelOne);
    }

    void LoadLevel(int[,] level)
    {
        currentLevel = level;
        GenerateMap();
        SetLevelTimer();
        OnMapGenerated?.Invoke();

        CameraController cameraController = FindObjectOfType<CameraController>();
        if (cameraController != null)
        {
            cameraController.AdjustCamera();
        }
    }

    void SetLevelTimer()
    {
        levelTimer = (currentLevelIndex == 1) ? 20f : 60f;
        UpdateUIManagerTimer();
    }

    void UpdateUIManagerTimer()
    {
        UIManager uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.SetLevelDuration(GetLevelDuration());
        }
    }

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

    bool IsLevelWon()
    {
        return isLevelWon;
    }

    void LoadNextLevel()
    {
        currentLevelIndex++;
        if (currentLevelIndex == 2)
        {
            LoadLevel(LevelTwo);
        }
    }

    public float GetLevelTimer()
    {
        return levelTimer;
    }

    public float GetLevelDuration()
    {
        return currentLevelIndex == 1 ? 20f : 60f;
    }

    public void StartGame()
    {
        isGameStarted = true;
    }

    public int GetMapWidth()
    {
        return currentLevel.GetLength(0);
    }

    public int GetMapHeight()
    {
        return currentLevel.GetLength(1);
    }
}