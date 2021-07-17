using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableBend : MonoBehaviour {

    public int correctAngle ;
    bool isCorrect;

	// Use this for initialization
	void Awake () {
        int x = (int)gameObject.transform.rotation.eulerAngles.z;
        if (x == correctAngle)
        {
            isCorrect=true;
        }
        else
        {
            isCorrect=false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {

        Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit raycastHit;
        if (Physics.Raycast(raycast, out raycastHit))
        {
            if (raycastHit.collider.name == gameObject.name)
            {
                /*
                Quaternion newRotation = transform.rotation;
                newRotation.eulerAngles.Set(newRotation.eulerAngles.x, newRotation.eulerAngles.y, newRotation.eulerAngles.z + 45);
                transform.rotation = Quaternion.Lerp(transform.rotation,  newRotation, 1f * Time.deltaTime);
                */
                gameObject.transform.Rotate(0, 0, 90);
                GameManager.instance.PlayRotateSound();
                //int x = (int)gameObject.transform.rotation.eulerAngles.z;
                int x = Mathf.RoundToInt(gameObject.transform.rotation.eulerAngles.z);
                if (x==correctAngle )
                {
                    UpdateStatus(true);
                }
                else if (isCorrect)
                {
                    UpdateStatus(false);
                }


            }
        }

    }

    public bool CheckStatus ()
    {
        return isCorrect;
    }

    void UpdateStatus (bool status)
    {
        isCorrect = status;
        GameManager.instance.ObjectChanged(CheckStatus());
    }

}
