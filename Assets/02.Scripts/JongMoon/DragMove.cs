using UnityEngine;
using UnityEngine.EventSystems;

public class DragMove : MonoBehaviour, IDragHandler
{
    private RectTransform parentRectTransform;
    private RectTransform myRectTransform;

    void Start()
    {
        myRectTransform = GetComponent<RectTransform>();
        parentRectTransform = transform.parent.GetComponent<RectTransform>();

        if (parentRectTransform == null)
        {
            Debug.LogError("This object's parent does not have a RectTransform component.");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(parentRectTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            // Calculate the offset from the parent's corner
            float width = parentRectTransform.rect.width;
            float height = parentRectTransform.rect.height;
            Vector3 boundsMin = parentRectTransform.TransformPoint(new Vector3(-width / 2, -height / 2, 0));
            Vector3 boundsMax = parentRectTransform.TransformPoint(new Vector3(width / 2, height / 2, 0));

            // Adjust the global mouse position based on the calculated bounds
            float clampedX = Mathf.Clamp(globalMousePos.x, boundsMin.x, boundsMax.x);
            float clampedY = Mathf.Clamp(globalMousePos.y, boundsMin.y, boundsMax.y);

            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
}
/*
using UnityEngine;
using UnityEngine.EventSystems;

public class DragMove : MonoBehaviour, IDragHandler
{
    private RectTransform parentRectTransform;
    private RectTransform myRectTransform;

    void Start()
    {
        myRectTransform = GetComponent<RectTransform>();
        parentRectTransform = transform.parent.GetComponent<RectTransform>();

        if (parentRectTransform == null)
        {
            Debug.LogError("This object's parent does not have a RectTransform component.");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        // Convert the mouse position to local position within the parent RectTransform
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            // Consider the size of the object to prevent it from going out of bounds
            localPoint.x = Mathf.Clamp(localPoint.x, -parentRectTransform.rect.width / 2 + myRectTransform.rect.width / 2, parentRectTransform.rect.width / 2 - myRectTransform.rect.width / 2);
            localPoint.y = Mathf.Clamp(localPoint.y, -parentRectTransform.rect.height / 2 + myRectTransform.rect.height / 2, parentRectTransform.rect.height / 2 - myRectTransform.rect.height / 2);

            // Set the position using the clamped local point, converting it to world space
            myRectTransform.localPosition = new Vector3(localPoint.x, localPoint.y, myRectTransform.localPosition.z);
        }
    }
}

*/