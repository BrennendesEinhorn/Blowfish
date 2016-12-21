using UnityEngine;
using System.Collections;

public class SimplePlayerMov : MonoBehaviour {





    private Rigidbody rBody;

    public float maxZAccelerationFractionPerSecond = 0.5f;
    public float maxXAccelerationFractionPerSecond = 1.2f;

    public float minSpeedZ = 15;
    public float maxSpeedZ = 17;
    public float maxZLimit = 30;
    public float minSpeedX = 0;
    public float pressTimeForMaxSpeedX = 1f;
    public float maxSpeedX = 20;


    public float speedUp = 2;
    public float slowDown = 1;

    public Vector3 direction = new Vector3();

    public float distanceTraveled = 0f;


    private bool grounded = true;





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

        

        if(!grounded)
        {
            rBody.velocity = new Vector3(rBody.velocity.x, rBody.velocity.y - 10 * Time.deltaTime, rBody.velocity.z);
        }

        if (Input.GetButton("Jump"))
        {
            playerJump();
        }

        //float mag = new Vector2(rBody.velocity.x, rBody.velocity.z).magnitude; 

        //rBody.velocity = new Vector3(direction.x * mag, rBody.velocity.y, direction.z * mag)  ;

        //rBody.transform.forward = new Vector3(direction.x, 0, direction.z);

        distanceTraveled += rBody.velocity.z * Time.fixedDeltaTime;

        Debug.Log("Distance: " + distanceTraveled);
         
        Vector3 rayOrigin = GetComponent<Collider>().bounds.center;

        float rayDistance = GetComponent<Collider>().bounds.extents.y + 0.1f;
        if (Physics.Raycast(rayOrigin, Vector3.down, rayDistance))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }


    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SpeedUp"))
        {
            other.gameObject.SetActive(false);
            if(maxSpeedZ <= maxZLimit)
            {
                maxSpeedZ += speedUp;
            }
        } else if (other.gameObject.CompareTag("SlowDown"))
        {
            if (maxSpeedZ > minSpeedZ)
            {
                maxSpeedZ -= slowDown; 
            }
            other.gameObject.SetActive(false);
            rBody.velocity = new Vector3(rBody.velocity.x, rBody.velocity.y, -5);

        } else if (other.gameObject.CompareTag("grube"))
        {
            transform.position = new Vector3(other.transform.position.x, 43.5f, transform.position.z - 40);
            distanceTraveled -= 40;

        }else if (other.gameObject.CompareTag("wall"))
        {
            rBody.AddForce(new Vector3(0f, -5f, 0));
        }

    }


    void playerJump()
    {

        const float JumpForce = 3.5f;

        if (grounded)
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
