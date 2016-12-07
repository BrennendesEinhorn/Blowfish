using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SimpleCamera : MonoBehaviour
{

    public GameObject player;

    private bool camMove = true;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;

    }

    //Late: runs after every object has been updated
    void LateUpdate()
    {
        if(camMove)
        {
            transform.position = player.transform.position + offset;
            transform.position = new Vector3(252f, 46.869f, transform.position.z);
            transform.LookAt(player.transform);
        }

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("gameEnd"))
        {
            camMove = false;

            Debug.Log("collision with camStop");

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }
}