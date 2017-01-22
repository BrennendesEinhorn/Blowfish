using UnityEngine;
using System.Collections;

public class SimplePlayerMov : MonoBehaviour {





    private Rigidbody rBody;

    public float maxZAccelerationFractionPerSecond = 0.5f;
    public float maxXAccelerationFractionPerSecond = 0.9f;

    public float minSpeedZ = 18;
    public float maxSpeedZ = 18;
    public float maxZLimit = 30;
    public float minSpeedX = 0;
    public float pressTimeForMaxSpeedX = 1f;
    public float maxSpeedX = 20;


    public float speedUp = 2;
    public float slowDown = 1;


	//TODO starting direction must equal directionOld, in this case (0,0,1)
	public Vector3 directionOld;
    public Vector3 direction;
	public bool changedDir = true;

    public float distanceTraveled;


    private bool grounded = true;


	/*doesnt work at some point
	 * private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
	{
		Vector2 difference = vec2 - vec1;
		float sign = (vec2.y < vec1.y)? -1.0f : 1.0f;
		float value = Vector2.Angle (vec2, vec1) * sign;
		Debug.Log ("calculated AngleBetween Vector2:" + value);
		return value;

	}*/
		
	private float AngleBetweenVector2Second(Vector2 vec1, Vector2 vec2)
	{
		float value = Mathf.DeltaAngle(Mathf.Atan2(vec1.y, vec1.x) * Mathf.Rad2Deg,
			Mathf.Atan2(vec2.y, vec2.x) * Mathf.Rad2Deg);
		Debug.Log ("calculated AngleBetween Vector2:" + value);

		return value;
	}


    // Use this for initialization
    void Start () {
        rBody = GetComponent<Rigidbody>();
		direction = new Vector3(0,0,1);
		directionOld = new Vector3 (0, 0, 1);
		distanceTraveled = 0.01f;

    }



    void FixedUpdate()
    {
		Vector2 zDir = new Vector2 (0, 1);
		Vector2 viewDir = new Vector2 (direction.x, direction.z);

		float angle = AngleBetweenVector2Second (viewDir, zDir);

		Vector3 rotVel = Quaternion.Euler (0, -angle, 0) * rBody.velocity;

		float leftRightVel = rotVel.x;


        

        float inX = Input.GetAxis("Horizontal");
        if (inX != 0)
        {
			float moveXRaw = Mathf.Sign(inX) * maxSpeedX * Time.deltaTime * maxXAccelerationFractionPerSecond + leftRightVel ;
            float moveX = Mathf.Clamp(moveXRaw, -maxSpeedX, maxSpeedX);

			Vector3 newVelRot = new Vector3 (moveX, rotVel.y, rotVel.z);
			Vector3 newVel = Quaternion.Euler (0, angle, 0) * newVelRot;


			rBody.velocity = newVel;
        }

		/*float inX = Input.GetAxis("Horizontal");
		if (inX != 0)
		{
			float moveXRaw = Mathf.Sign(inX) * maxSpeedX * Time.deltaTime * maxXAccelerationFractionPerSecond + rBody.velocity.x ;
			float moveX = Mathf.Clamp(moveXRaw, -maxSpeedX, maxSpeedX);

			rBody.velocity = new Vector3( moveX , rBody.velocity.y, rBody.velocity.z);
		}*/




        if (rotVel.z < maxSpeedZ)
        {

			Vector3 rotVelforZ = Quaternion.Euler (0, -angle, 0) * rBody.velocity;

			Vector3 newVelRot = new Vector3(rotVelforZ.x, rotVelforZ.y, rotVelforZ.z + maxSpeedZ * Time.deltaTime * maxZAccelerationFractionPerSecond);
			Vector3 newVel = Quaternion.Euler (0, angle, 0) * newVelRot;


			rBody.velocity = newVel;
        }


        if(!grounded)
        {
            rBody.velocity = new Vector3(rBody.velocity.x, rBody.velocity.y - 10 * Time.deltaTime, rBody.velocity.z);
        }

        if (Input.GetButton("Jump"))
        {
            playerJump();
        }


		//transform velocity to new viewDir
		if (changedDir == true) 
		{
			float angleDirs = AngleBetweenVector2Second(new Vector2(direction.x, direction.z),new Vector2 (directionOld.x, directionOld.z));
			Debug.Log("direction: " + direction);
			Debug.Log("directionOld: " + directionOld);

			Debug.Log("angleDirs: " + angleDirs);

			
			rBody.velocity = Quaternion.Euler (0, angleDirs, 0) * rBody.velocity;

			changedDir = false;
		
		}

		//float mag = new Vector2(rBody.velocity.x, rBody.velocity.z).magnitude; 

        //rBody.velocity = new Vector3(direction.x * mag, rBody.velocity.y, direction.z * mag)  ;
         
        //rBody.transform.forward = new Vector3(direction.x, 0, direction.z);
		Vector3 rotVelforDistance = Quaternion.Euler (0, -angle, 0) * rBody.velocity;

		if(rotVelforDistance.z > 1 || rotVelforDistance.z < 0)
        {
			distanceTraveled += (rotVelforDistance.z - 0.1f) * Time.fixedDeltaTime;
        }

        Debug.Log("z velocity: " + rBody.velocity.z);

        Debug.Log("Distance: " + distanceTraveled);
         


		//test if player is still on ground
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
