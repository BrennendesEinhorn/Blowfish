using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;


public class CameraController : MonoBehaviour {

    public GameObject player;

    private Vector3 offset;
    public Transform target;
    public Vector3[] waypoints = new[] {
        new Vector3(-7.7f, -5.2f, 0f),
        new Vector3(-4.8f, -5.2f, 29f),
        new Vector3(1.86f, -5.2f, 47f),
        new Vector3(1.67f, -5.2f, 72.61f),
    };

    void Start()
    {
        target = this.transform;


        // Create a path tween using the given pathType, Linear or CatmullRom (curved).
        // Use SetOptions to close the path
        // and SetLookAt to make the target orient to the path itself
        Tween t = target.DOPath(waypoints, 50, PathType.CatmullRom)
            .SetOptions(true)
            .SetLookAt(0.001f);
        // Then set the ease to Linear and use infinite loops
        //t.SetEase(Ease.Linear);

        offset = transform.position - player.transform.position;


    }

    // Find closest 2 CameraDirection objects, returns null if there are less than 2 on map
    GameObject[] findClosest2CameraDirection()
    {
        

        List<float> distances = new List<float>();
        GameObject[] gos;
        Vector3 position = GameManager.Instance.Player.transform.position;

        gos = GameObject.FindGameObjectsWithTag("CameraDirection");

        //if we dont have at least 2 cameradirections, we signal this with a null result
        if(gos.Length < 2)
        {
            return null;
        }

        if (gos.Length == 2)
        {
            return gos;
        }



        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            distances.Add(diff.sqrMagnitude);
        }

        float closest = Mathf.Infinity;
        int closestIndex = 0;
        int secondClosestIndex = 1;
        for (int i = 0; i < distances.Count; i++)
        {
            if (distances[i] < closest)
            {
                closest = distances[i];
                secondClosestIndex = closestIndex;
                closestIndex = i;
                
            }
        }

        Debug.Log(closestIndex + " " + secondClosestIndex);

        GameObject[] result = { gos[closestIndex], gos[secondClosestIndex] };


        return result;
    }


    Vector3 getDesiredCamDirection()
    {


        GameObject[] closestCamDirections = findClosest2CameraDirection();

        // just go straight if there arent at least 2 cam directions set
        if(closestCamDirections == null)
        {
            return new Vector3(0, 0, 1.0f);
        }

        // we add a minus because the camera is in the opposite direction of the one that we want to face
        Vector3 firstDir = -closestCamDirections[0].transform.forward.normalized;
        Vector3 secondDir= -closestCamDirections[1].transform.forward.normalized;
        float firstDist = (closestCamDirections[0].transform.position - transform.position).sqrMagnitude;
        float secondDist = (closestCamDirections[1].transform.position - transform.position).sqrMagnitude;

        // from high distance to 0 because nearer ones matter more => bigger factor
        float firstFactor = Mathf.InverseLerp(firstDist + secondDist, 0, firstDist);
        float secondFactor = 1.0f - firstFactor;

        Vector3 desiredDirection = new Vector3(firstDir.x * firstFactor + secondDir.x * secondFactor, 0, firstDir.z * firstFactor + secondDir.z * secondFactor);

        Debug.Log(desiredDirection.x + " " + desiredDirection.y + " " + desiredDirection.z, gameObject);
        return desiredDirection;
    }


    //Late: runs after every object has been updated
    void FixedUpdate()
    {
        Vector3 desiredDirection = getDesiredCamDirection();

        Vector3 rotatedOffset = new Vector3 (offset.magnitude * desiredDirection.normalized.x, offset.y, offset.magnitude  * desiredDirection.normalized.z);

        //transform.position = player.transform.position + rotatedOffset;


        //transform.LookAt(player.transform);

        //GameManager.Instance.Player.direction = -desiredDirection;

    }
}
