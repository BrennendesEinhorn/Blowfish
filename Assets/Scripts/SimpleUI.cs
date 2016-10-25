using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SimpleUI : MonoBehaviour {

    private Rigidbody rBody;

    public Text topLeftText;



    // Use this for initialization
    void Start () {
        rBody = GetComponent<Rigidbody>();


    }

    private void updateSpeedText()
    {
        topLeftText.text = "Speed: " + rBody.velocity.x+ "\nmoveXInput: " + Input.GetAxis("Horizontal");
    }


    // Update is called once per frame
    void Update () {
        updateSpeedText();
    }
}
