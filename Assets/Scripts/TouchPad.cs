using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RectTransform touchpad_rect = GetComponent<RectTransform>();
        RectTransform handle_rect = transform.GetChild(0).GetComponent<RectTransform>();

        float size = (float)Screen.width / 6f;

        touchpad_rect.sizeDelta = new Vector2(size, size);
        transform.position = new Vector2(3f * size / 5f, 3 * size / 5f);
        handle_rect.sizeDelta = new Vector2(size / 5f, size / 5f);
    }
}