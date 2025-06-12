using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float detectionRange = 4f;
    private string zombieTag = "Zombie";
    private KeyCode detectionKey = KeyCode.E;
    private PlayerMovement playerMovement;
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !playerMovement.isAttack)
        {
            DetectNearbyZombies();
        }
    }

    void DetectNearbyZombies()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag(zombieTag);

        foreach (GameObject zombie in zombies)
        {
            float distance = Vector3.Distance(transform.position, zombie.transform.position);

            if (distance <= detectionRange)
            {
                Debug.Log("Zombie detected in range: " + zombie.name);
                // You can do other things here like attack, mark target, etc.
            }
        }
    }
}
