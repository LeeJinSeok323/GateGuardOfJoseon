using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragMove : MonoBehaviour, IDragHandler
{
    private Vector3 offset;
    private float minX, maxX, minY, maxY;

    void Start()
    {
        // Set these values based on your game's requirements
        // For example, you can use the camera's viewport to set these boundaries
        Camera cam = Camera.main;
        minX = 0; // Left boundary
        maxX = Screen.width; // Right boundary
        minY = 0; // Bottom boundary
        maxY = Screen.height; // Top boundary
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPosition = eventData.position;

        // Clamp the position within the defined bounds
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = newPosition;
    }
}
