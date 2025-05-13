using UnityEngine;

public class DestroyOverLifeTime : MonoBehaviour
{
     public float time;
    void Start()
    {
        Destroy(gameObject, time);
    }

 
}
