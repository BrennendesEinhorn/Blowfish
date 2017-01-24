using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gesundheitszustand : MonoBehaviour {


    public Image Gesundheitszustand1;
    public Image Gesundheitszustand2;
    public Image Gesundheitszustand3;
    void Start () {
        Gesundheitszustand1.gameObject.SetActive(false);
        Gesundheitszustand2.gameObject.SetActive(false);
        Gesundheitszustand3.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        //hab gelogen kann das doch nicht :(
        // Du Pflunze!!!!!!!!!!
        // harsch :(
        // lies das ohne das h


        float momentanGesch = GameManager.Instance.Player.rBody.velocity.z;
        float höchstGeschw = GameManager.Instance.Player.maxZLimit;

        Debug.Log("momentan Geschw:" + momentanGesch);



        if (momentanGesch < 32 && momentanGesch > 25)
        {
            Gesundheitszustand1.gameObject.SetActive(true);
            Gesundheitszustand2.gameObject.SetActive(false);
            Gesundheitszustand3.gameObject.SetActive(false);
        }
        else if (momentanGesch < 25 && momentanGesch > 10) {

            Gesundheitszustand2.gameObject.SetActive(true);
            Gesundheitszustand1.gameObject.SetActive(false);
            Gesundheitszustand3.gameObject.SetActive(false);

        } else if (momentanGesch < 10 && momentanGesch > -5) {

            Gesundheitszustand3.gameObject.SetActive(true);
            Gesundheitszustand1.gameObject.SetActive(false);
            Gesundheitszustand2.gameObject.SetActive(false);
        }


	}
}
