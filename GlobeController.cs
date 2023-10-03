using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeController : MonoBehaviour
{
    private bool isRotating = false;
    private float rotationSpeed = 10f;
    private float zoomSpeed = 0.5f;
    private Vector2 prevTouchPosition;

    void Update()
    {
        // Rotate the globe when user is dragging.
        if (isRotating && Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = touch.position - prevTouchPosition;
                transform.Rotate(Vector3.up, -delta.x * rotationSpeed * Time.deltaTime);
            }
            prevTouchPosition = touch.position;
        }

        // Zoom in/out using pinch-to-zoom gesture.
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;
            Vector2 touch2PrevPos = touch2.position - touch2.deltaPosition;

            float prevMagnitude = (touch1PrevPos - touch2PrevPos).magnitude;
            float currentMagnitude = (touch1.position - touch2.position).magnitude;

            float pinchAmount = (currentMagnitude - prevMagnitude) * zoomSpeed;
            Vector3 newScale = transform.localScale + new Vector3(pinchAmount, pinchAmount, pinchAmount);

            // Ensure that the scale doesn't become too small or too large.
            newScale = Vector3.ClampMagnitude(newScale, 0.5f);
            transform.localScale = newScale;
        }
    }

    // Handle touch events to enable/disable rotation.
    public void ToggleRotation()
    {
        isRotating = !isRotating;
    }

    // Handle tap events for displaying country information.
    public void OnTap(Vector3 tapPosition)
    {
        // Cast a ray from the touch position to the globe and handle country selection.
        Ray ray = Camera.main.ScreenPointToRay(tapPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // Perform actions based on the hit object (e.g., display country info).
        }
    }
}
