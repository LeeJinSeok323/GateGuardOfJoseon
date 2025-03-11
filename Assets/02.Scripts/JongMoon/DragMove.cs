using UnityEngine;
using UnityEngine.EventSystems;

public class DragMove : MonoBehaviour, IDragHandler
{
    private RectTransform parentRectTransform;
    private RectTransform myRectTransform;
    public float blankX;
    public float blankY;

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
            Vector3 boundsMin = parentRectTransform.TransformPoint(new Vector3((-width / 2) + blankX, (-height / 2) + blankY, 0));
            Vector3 boundsMax = parentRectTransform.TransformPoint(new Vector3((width / 2) - blankX, (height / 2) - blankY, 0));

            // Adjust the global mouse position based on the calculated bounds
            float clampedX = Mathf.Clamp(globalMousePos.x, boundsMin.x, boundsMax.x);
            float clampedY = Mathf.Clamp(globalMousePos.y, boundsMin.y, boundsMax.y);

            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
}
