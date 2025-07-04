using UnityEngine;

public class BossSizeInitialize : MonoBehaviour
{

    [Header("Target Scale (XYZ)")]
     float sizeX = -1.5f;
     float sizeY = 1.5f;
     float sizeZ = 1.5f;
    public GameObject bossPrefab; // Reference to the boss prefab
    private void Start()
    {

        bossPrefab.transform.localScale = new Vector3(sizeX, sizeY, sizeZ);
        bossPrefab.transform.rotation = Quaternion.Euler(0, 180, 0); // Reset rotation to avoid any unwanted rotations
    }
    private void Awake()
    {
        bossPrefab.transform.localScale = new Vector3(sizeX, sizeY, sizeZ);
        bossPrefab.transform.rotation = Quaternion.Euler(0, 180, 0); // Reset rotation to avoid any unwanted rotations

    }
    private void Update()
    {

    }
}
