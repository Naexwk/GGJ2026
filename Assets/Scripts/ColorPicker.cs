using UnityEngine;

public class ColorPicker : MonoBehaviour
{
    void Start()
    {
        PickColor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PickColor();
        }
    }

    void PickColor()
    {
        SpriteRenderer[] sprites =
        GetComponentsInChildren<SpriteRenderer>();

        foreach(var item in sprites)
        {
            item.color = RandomColor.randomColor;
        }
    }
}