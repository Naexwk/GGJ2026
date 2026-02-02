using UnityEngine;

public class RandomColor : MonoBehaviour
{
    static public Color randomColor;

    void Awake()
    {
        GetColor();
    }

    float RandNumber ()
    {
        return 0.5f + Random.Range(0f, 0.5f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetColor();
        }
    }

    void GetColor()
    {
        randomColor = new Color(RandNumber(), RandNumber(), RandNumber());
    }
}