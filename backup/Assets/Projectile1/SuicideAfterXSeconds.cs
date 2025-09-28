using UnityEngine;

public class SuicideAfterXSeconds : MonoBehaviour
{
    public float lifetime = 5f;

    void Start()
    {
        // Schedule destruction of this GameObject after 'lifetime' seconds
        Destroy(gameObject, lifetime);
    }
}
