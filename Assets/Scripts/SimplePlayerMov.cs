using UnityEngine;
using System.Collections;

public class SimplePlayerMov : MonoBehaviour {





    private Rigidbody rBody;

    public float maxZAccelerationFractionPerSecond = 0.5f;
    public float maxXAccelerationFractionPerSecond = 1.2f;

    public float minSpeedZ = 10;
    public float maxSpeedZ = 15;
    public float minSpeedX = 0;
    public float pressTimeForMaxSpeedX = 1f;
    public float maxSpeedX = 20;


    public float speedUp = 3;
    public float slowDown = 2;









    // Use this for initialization
    void Start () {
        rBody = GetComponent<Rigidbody>();

    }



    void FixedUpdate()
    {

        

        float inX = Input.GetAxis("Horizontal");
        if (inX != 0)
        {
            float moveXRaw = Mathf.Sign(inX) * maxSpeedX * Time.deltaTime * maxXAccelerationFractionPerSecond + rBody.velocity.x ;
            float moveX = Mathf.Clamp(moveXRaw, -maxSpeedX, maxSpeedX);

            rBody.velocity = new Vector3( moveX , rBody.velocity.y, rBody.velocity.z);
        }

        if (rBody.velocity.z < maxSpeedZ)
        {
            rBody.velocity = new Vector3(rBody.velocity.x, rBody.velocity.y, rBody.velocity.z + maxSpeedZ * Time.deltaTime * maxZAccelerationFractionPerSecond);
        }

        //if(rBody.velocity.y < -0.5f && rBody.velocity.y > -10)
        //{
        //    rBody.velocity = new Vector3(rBody.velocity.x, rBody.velocity.y - 100 * Time.deltaTime, rBody.velocity.z);
        //}

        if (Input.GetButton("Jump"))
        {
            playerJump();
        }

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SpeedUp"))
        {
            other.gameObject.SetActive(false);
            maxSpeedZ += speedUp;
        }else if(other.gameObject.CompareTag("SlowDown"))
        {
            if(maxSpeedZ > minSpeedZ)
            {
                maxSpeedZ -= slowDown;
            }
        }
    }



    void playerJump()
    {

        const float JumpForce = 3.5f;

        Vector3 rayOrigin = GetComponent<Collider>().bounds.center;

        float rayDistance = GetComponent<Collider>().bounds.extents.y + 0.1f;
        if (Physics.Raycast(rayOrigin, Vector3.down, rayDistance))
        {
            rBody.AddForce(Vector3.up * JumpForce, ForceMode.VelocityChange);
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
