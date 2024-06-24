using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] prefabTypes;
    public GameObject groundPrefab;
    public float spacing = 1f;

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

    private List<GameObject> allBlocks = new List<GameObject>();
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
                    block.AddComponent<Rigidbody>().isKinematic = true; 
                    block.AddComponent<BoxCollider>(); 

                    allBlocks.Add(block);

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

        if (moveDirection != Vector3.zero)
        {
            HashSet<Vector3> occupiedPositions = new HashSet<Vector3>();

           
            foreach (GameObject block in allBlocks)
            {
                occupiedPositions.Add(block.transform.position);
            }

            List<GameObject> movableBlocks = new List<GameObject>();

            foreach (GameObject block in type2Blocks)
            {
                Vector3 newPosition = block.transform.position + moveDirection;

                if (!occupiedPositions.Contains(newPosition))
                {
                    movableBlocks.Add(block);
                    occupiedPositions.Add(newPosition); 
                }
            }

       
            foreach (GameObject block in movableBlocks)
            {
                Rigidbody rb = block.GetComponent<Rigidbody>();
                rb.MovePosition(block.transform.position + moveDirection);
            }
        }
    }
}
