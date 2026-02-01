using UnityEngine;

public class Observer : MonoBehaviour
{
    PlayerController player;
    float maxDetectionValue = 10f;
    float currentDetectionValue = 0;
    [SerializeField] LayerMask obstacleLayerMask;
    public bool isAlert = false;
    public bool detectedPlayer = false;
    [SerializeField] float runTime = 5f;

    // time it takes to go from alert to running/chasing
    // rn is 5 seconds (math)
    [SerializeField] float alertTime = 2f;

    // on top of alert time
    [SerializeField] float detectionTime = 3f;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        currentDetectionValue = 0f;
    }

    void FixedUpdate()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (player.isSus && !Physics.Raycast(player.transform.position, transform.position, distance, obstacleLayerMask))
            currentDetectionValue += Time.deltaTime;
        else
            currentDetectionValue -= Time.deltaTime / 2f;

        Mathf.Clamp(currentDetectionValue, 0f, maxDetectionValue);

        if (currentDetectionValue > detectionTime)
        {
            isAlert = false;
            detectedPlayer = true;
            return;
        }

        if (currentDetectionValue > alertTime)
        {
            detectedPlayer = false;
            isAlert = true;
            return;
        }

        detectedPlayer = false;
        isAlert = false;
    }
}