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
        topLeftText.text = "Speed: " + System.Math.Round(rBody.velocity.z, 2) + "\nTarget Speed: " + GameManager.Instance.Player.maxSpeedZ ; 
    }


    // Update is called once per frame
    void Update () {
        updateSpeedText();
    }
}
