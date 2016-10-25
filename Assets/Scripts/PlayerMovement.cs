using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private Rigidbody rBody;

    public float maxSpeedZ = 2;
    public float minSpeed = 0;
    public float pressTimeForMaxSpeed = 1f;
    public float maxSpeed = 1000;


    private float lastPressTime;
    private float lastReleaseTime;

    private bool isPressed;

    // Use this for initialization
    void Start () {
		rBody = GetComponent<Rigidbody> ();

        
    }

 

	void FixedUpdate () {

        if(rBody.velocity.y < maxSpeedZ)
        {

        }



        float moveXInput = Input.GetAxis("Horizontal"); 
        if(Mathf.Abs(moveXInput) == 1) //movementInput
        {
            if(!isPressed) //if the button wasnt held last update
            {
                isPressed = true;
                lastPressTime = Time.time;
            }

            StopCoroutine(SlowdownX());

            //get the % of the time pressed in relation to our max needed time to reach full speed
            float t = Mathf.InverseLerp(0, pressTimeForMaxSpeed, Time.time - lastPressTime);

            //Calculate new x velocity depending on t and deltatime
            float moveX = moveXInput * Time.deltaTime * Mathf.Lerp(minSpeed, maxSpeed, t);

            
            
            rBody.velocity = new Vector3(moveX, rBody.velocity.y, rBody.velocity.z);
            
        }
        else
        {
            if(isPressed)
            {
                isPressed = false;
                lastReleaseTime = Time.time;
            }


           
            StartCoroutine(SlowdownX());
        }
        

    }
	
    private IEnumerator SlowdownX()
    {
        float moveXInput = Mathf.Sign(rBody.velocity.x);

        //get the % of the time pressed in relation to our max needed time to reach 0 speed
        float t = Mathf.InverseLerp(pressTimeForMaxSpeed, 0, Time.time - lastReleaseTime);

        //Calculate new x velocity depending on t and deltatime
        float moveX = moveXInput * Time.deltaTime * Mathf.Lerp(minSpeed, maxSpeed, t);

        if (Mathf.Abs(moveX) < 0.001)
        {
            moveX = 0;
        }


        rBody.velocity = new Vector3(moveX, rBody.velocity.y, rBody.velocity.z);

        yield return new WaitForEndOfFrame();

    }

   

    // Update is called once per frame
    void Update () {
	
	}
}
