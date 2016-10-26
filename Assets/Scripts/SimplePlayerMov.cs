using UnityEngine;
using System.Collections;

public class SimplePlayerMov : MonoBehaviour {





    private Rigidbody rBody;

    public float maxZAccelerationFractionPerSecond = 0.5f;
    public float minSpeedZ = 2;
    public float maxSpeedZ = 20;
    public float minSpeedX = 0;
    public float pressTimeForMaxSpeedX = 1f;
    public float maxSpeedX = 20;



   





    // Use this for initialization
    void Start () {
        rBody = GetComponent<Rigidbody>();

    }



    void FixedUpdate()
    {

        

        float inX = Input.GetAxis("Horizontal");
        if (inX != 0)
        {
            float moveXRaw = Mathf.Sign(inX) * maxSpeedX * Time.deltaTime + rBody.velocity.x;
            float moveX = Mathf.Clamp(moveXRaw, -maxSpeedX, maxSpeedX);

            rBody.velocity = new Vector3( moveX , rBody.velocity.y, rBody.velocity.z);
        }

        if (rBody.velocity.z < maxSpeedZ)
        {
            rBody.velocity = new Vector3(rBody.velocity.x, rBody.velocity.y, rBody.velocity.z + maxSpeedZ * Time.deltaTime * maxZAccelerationFractionPerSecond);
        }

        if(rBody.velocity.y < -0.5f && rBody.velocity.y > -10)
        {
            rBody.velocity = new Vector3(rBody.velocity.x, rBody.velocity.y - 80 * Time.deltaTime, rBody.velocity.z);
        }



    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SpeedUp"))
        {
            other.gameObject.SetActive(false);
            maxSpeedZ += 3;
        }
    }

    private IEnumerator SlowdownX()
    {

        rBody.velocity = new Vector3(0, rBody.velocity.y, rBody.velocity.z);
        yield return new WaitForEndOfFrame();

    }


    // Update is called once per frame
    void Update () {
	
	}
}
