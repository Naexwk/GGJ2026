using UnityEngine;

public class Observer : MonoBehaviour
{
    PlayerController player;
    float maxDetectionValue = 100f;
    float currentDetectionValue = 0;
    bool playerDetected = false;

    // time it takes to go from alert to running/chasing
    // rn is 5 seconds (math)
    [SerializeField] LayerMask obstacleLayerMask;
    [SerializeField] float detectionRate = 4f; 
    [SerializeField] float undetectionRate = 2f; 
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        currentDetectionValue = 0f;
    }
    
    void FixedUpdate()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (player.isSus)
            if (!Physics.Raycast(player.transform.position, transform.position, distance, obstacleLayerMask))
            currentDetectionValue += detectionRate * Time.deltaTime;
        else
            currentDetectionValue -= undetectionRate * Time.deltaTime;

        if (currentDetectionValue >= 100) playerDetected = true;
        if(currentDetectionValue < 0) currentDetectionValue = 0;
    }
}