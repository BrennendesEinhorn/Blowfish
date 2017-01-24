using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {

    public GameObject player;



    private Vector3 offset;
    private float pathLength;
    private Tween tw;


    /*new Vector3(-7.7f, -5.2f, 2f),
        new Vector3(-7.7f, -5.2f, 10f),
        new Vector3(1.86f, -5.2f, 47f),
        new Vector3(1.67f, -5.2f, 72.61f)*/

    public Transform target;
    private Vector3[] waypoints = new[] {
        new Vector3(252f, 45.69f, -1.68f),
        new Vector3(255.64f, 45.69f, 72.7f),
        new Vector3(256.6f,  45.69f, 213.3f),
        new Vector3(254.4f,  45.69f, 299.6f),

        new Vector3(253.42f,  45.69f, 363.7f),
        new Vector3(252f,  45.69f, 429f),
        new Vector3(251.84f,  45.69f, 501.7f),
        new Vector3(250.7f,  45.69f, 555.5f),
        new Vector3(248.8f,  45.69f, 627.4f),
        new Vector3(247.49f,  45.69f, 802.32f)
    };



    void Start()
    {
        //target = this.transform;


        // Create a path tween using the given pathType, Linear or CatmullRom (curved).
        // Use SetOptions to close the path
        // and SetLookAt to make the target orient to the path itself
        tw = target.DOPath(waypoints, 5, PathType.CatmullRom)
            .SetOptions(false)
            .SetLookAt(0.001f)
            .SetAutoKill(false);
        // Then set the ease to Linear and use infinite loops
        //t.SetEase(Ease.Linear);

        tw.ForceInit();

        pathLength = tw.PathLength();

        //Debug.Log("pathlength" + tw.PathLength());

      



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

        //Debug.Log(closestIndex + " " + secondClosestIndex);

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

        //Debug.Log(desiredDirection.x + " " + desiredDirection.y + " " + desiredDirection.z, gameObject);
        return desiredDirection;
    }


    Vector3 getDirection(float percentage)
    {
        Vector3 newPosition = tw.PathGetPoint(percentage);
        Vector3 newPositionFurther = tw.PathGetPoint(percentage + 0.01f);

        Vector3 dir = (new Vector3(newPositionFurther.x, newPositionFurther.y, newPositionFurther.z) - newPosition).normalized;

        return dir;
    }


    //Late: runs after every object has been updated
    void FixedUpdate()
    {
        Vector3 desiredDirection = getDesiredCamDirection();

        Vector3 rotatedOffset = new Vector3 (offset.magnitude * desiredDirection.normalized.x, offset.y, offset.magnitude  * desiredDirection.normalized.z);


        float percentagePath = GameManager.Instance.Player.distanceTraveled / pathLength;


        //Debug.Log("percentage: " + percentagePath);

        Vector3 newPosition = tw.PathGetPoint(percentagePath);

        transform.position = newPosition;

        //Debug.Log("new Position: " + newPosition);


        //transform.position = player.transform.position + rotatedOffset;


        //transform.LookAt(player.transform);

        Vector3 dir = getDirection(percentagePath);

        transform.rotation = Quaternion.LookRotation(dir);

        GameManager.Instance.Player.direction = dir;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("gameEnd"))
        {
           

            //Debug.Log("collision with camStop");

            StartCoroutine(EndTime());
        }
    }



    IEnumerator EndTime()
    {
        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(0);

    }
}
