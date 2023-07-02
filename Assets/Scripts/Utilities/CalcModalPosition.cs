using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcModalPosition
{
    public static void CalculateModalPosition (GameObject referenceObject, Canvas canvas, GameObject modal, Vector2 offsetAxisY) 
    {
        Vector3 objectWorldPosition = referenceObject.transform.position;
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(objectWorldPosition);

        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
        RectTransform modalRectTransform = modal.GetComponent<RectTransform>();

        Vector2 canvasBounds = new Vector2(canvasRectTransform.rect.width, canvasRectTransform.rect.height);
        Vector2 modalBoxSize = modalRectTransform.rect.size;

        // Calculate the position relative to the canvas
        Vector2 canvasDialoguePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPosition, canvas.worldCamera, out canvasDialoguePosition);

        // Set the dialogue box position based on the canvasDialoguePosition
        modalRectTransform.anchoredPosition = canvasDialoguePosition;

        // Check if the dialogue box exceeds the canvas bounds
        Vector2 modalPosition = modalRectTransform.anchoredPosition;
        float leftBound = -canvasBounds.x * 0.5f + modalBoxSize.x * 0.5f;
        float rightBound = canvasBounds.x * 0.5f - modalBoxSize.x * 0.5f;
        float bottomBound = -canvasBounds.y * 0.5f + modalBoxSize.y * 0.5f;
        float topBound = canvasBounds.y * 0.5f - modalBoxSize.y * 0.5f;

        if (modalPosition.x < leftBound)
            modalPosition.x = leftBound;
        else if (modalPosition.x > rightBound)
            modalPosition.x = rightBound;

        if (modalPosition.y < bottomBound)
            modalPosition.y = bottomBound;
        else if (modalPosition.y > topBound)
            modalPosition.y = topBound;

        // Set the final dialogue box position within the canvas bounds
        modalRectTransform.anchoredPosition = modalPosition + offsetAxisY;
    }

}
