using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] prefabTypes; // prefabs indexed from 1 to n, index 0 can be null if you prefer to keep it in the array
    public GameObject groundPrefab;
    public float spacing = 1.1f;

       private int[,] mapLayout = {
    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    {1, 0, 0, 0, 1, 2, 2, 2, 2, 2, 0, 0, 0, 0, 1},
    {1, 0, 1, 0, 1, 2, 1, 1, 1, 2, 0, 1, 1, 0, 1},
    {1, 0, 1, 0, 0, 0, 1, 2, 1, 0, 0, 1, 2, 0, 1},
    {1, 1, 1, 0, 1, 1, 1, 2, 1, 1, 0, 1, 2, 0, 1},
    {1, 2, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 2, 0, 1},
    {1, 2, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 2, 0, 1},
    {1, 2, 2, 2, 2, 2, 1, 2, 0, 0, 0, 1, 2, 0, 1},
    {1, 1, 1, 1, 1, 2, 1, 2, 1, 1, 1, 1, 2, 1, 1},
    {1, 0, 0, 0, 0, 2, 0, 2, 0, 2, 2, 2, 2, 0, 1},
    {1, 0, 1, 1, 0, 1, 0, 2, 0, 1, 1, 1, 1, 0, 1},
    {1, 0, 1, 2, 2, 2, 0, 2, 0, 1, 2, 2, 2, 0, 1},
    {1, 0, 1, 2, 1, 1, 0, 2, 0, 0, 0, 0, 0, 0, 1},
    {1, 0, 0, 0, 1, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1},
    {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
};

    private Dictionary<Vector3, int> occupiedPositions = new Dictionary<Vector3, int>();
    private List<GameObject> type2Blocks = new List<GameObject>();

    private void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        float groundWidth = mapLayout.GetLength(0) * spacing;
        float groundHeight = mapLayout.GetLength(1) * spacing;
        float minY = float.MaxValue;

        for (int i = 0; i < mapLayout.GetLength(0); i++)
        {
            for (int j = 0; j < mapLayout.GetLength(1); j++)
            {
                int prefabIndex = mapLayout[i, j];
                if (prefabIndex != 0) 
                {
                    Vector3 position = new Vector3(i * spacing, 0, j * spacing);
                    GameObject block = Instantiate(prefabTypes[prefabIndex - 1], position, Quaternion.identity);

                    occupiedPositions[position] = prefabIndex;

                    if (prefabIndex == 2)
                    {
                        type2Blocks.Add(block);
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

    private void Update()
    {
        MoveType2Blocks();
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

        foreach (GameObject block in type2Blocks)
        {
            Vector3 newPosition = block.transform.position + moveDirection;
            if (!occupiedPositions.ContainsKey(newPosition))
            {
                occupiedPositions.Remove(block.transform.position);
                occupiedPositions.Add(newPosition, 2);
                block.transform.position = newPosition;
            }
        }
    }
}
