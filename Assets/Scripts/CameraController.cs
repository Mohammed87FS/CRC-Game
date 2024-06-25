using UnityEngine;

public class CameraController : MonoBehaviour
{
    public MapGenerator mapGenerator;
    public float padding = 2f;
    public float cameraHeight = 40f; 
    public float cameraDepth = -10f; 

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogError("Camera component is not attached to the CameraController GameObject.");
        }
    }

    private void Start()
    {
        if (mapGenerator == null)
        {
            Debug.LogError("MapGenerator is not assigned.");
        }
        else
        {
            mapGenerator.OnMapGenerated += AdjustCamera; 
            AdjustCamera();
        }
    }

    public void AdjustCamera()
    {
        if (mapGenerator != null)
        {
            int mapWidth = mapGenerator.GetMapWidth();
            int mapHeight = mapGenerator.GetMapHeight();

            Vector3 mapCenter = new Vector3(mapWidth / 2f, cameraHeight, mapHeight / 2f); 
            transform.position = mapCenter;

        
            transform.rotation = Quaternion.Euler(90f, 0f, 0f);

            float mapAspectRatio = (float)mapWidth / mapHeight;
            float screenAspectRatio = (float)Screen.width / Screen.height;

            if (screenAspectRatio >= mapAspectRatio)
            {
                cam.orthographicSize = mapHeight / 2f + padding;
            }
            else
            {
                float differenceInSize = mapAspectRatio / screenAspectRatio;
                cam.orthographicSize = (mapHeight / 2f + padding) * differenceInSize;
            }
        }
        else
        {
            Debug.LogError("MapGenerator is not assigned.");
        }
    }
}
