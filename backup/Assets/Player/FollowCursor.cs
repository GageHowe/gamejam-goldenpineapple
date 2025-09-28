using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    Camera cam;
    bool canUseCamera = false;

    void Start()
    {
        if (Camera.main != null)
        {
            cam = Camera.main;
            canUseCamera = true;
        }
    }

    void Update()
    {
        if (!canUseCamera) return;

        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Keep sprite on the 2D plane

        transform.position = mousePos;
    }
}
