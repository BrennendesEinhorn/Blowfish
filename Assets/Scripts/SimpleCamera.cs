using UnityEngine;
using System.Collections;

public class SimpleCamera : MonoBehaviour
{

    public GameObject player;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;

    }

    //Late: runs after every object has been updated
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform);

    }
}