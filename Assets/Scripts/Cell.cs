using System;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Renderer borderRenderer;
    public Transform anchor;
    public Material regular;
    public Material selected;

    [Space(20)]
    public Ball ball;
    public int x;
    public int y;

    // событие принимает 2 аргумента-инта
    public event Action<int, int> Click;

    void OnMouseDown()
    {
        if (Click != null)
            Click(x, y);
    }

    public void SetSelected(bool chosen)
    {
        if (chosen)
        {
            borderRenderer.material = selected;
        }
        else
        {
            borderRenderer.material = regular;
        }
    }
}
