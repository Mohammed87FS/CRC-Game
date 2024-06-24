using UnityEngine;
using System.IO;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] prefabTypes;
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
            if (prefabIndex >= 0 && prefabIndex < prefabTypes.Length)
            {
             
                Vector3 position = new Vector3(i * spacing, 0, j * spacing);
                GameObject block = Instantiate(prefabTypes[prefabIndex], position, Quaternion.identity);
                
              
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



   
}


