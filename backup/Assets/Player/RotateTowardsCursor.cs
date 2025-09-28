using UnityEngine;

public class RotateTowardsCursor : MonoBehaviour
{
    Camera cam;
    bool canUseCamera = false;

    void Start()
    {
        if (Camera.main != null)
        {
            cam = Camera.main;
            canUseCamera = true;  // Enable usage of the camera
        }
    }

    void Update()
    {
        if (!canUseCamera) return;

        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector3 direction = mousePos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Subtract 90 degrees so up points to the cursor (instead of right)
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }
}