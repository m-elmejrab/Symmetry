using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Rotatable : MonoBehaviour {

    public event Action<bool> statusChanged;

    public int correctAngle ;
    bool isCorrect;

	// Use this for initialization
	void Awake () {
        int x = Mathf.RoundToInt(gameObject.transform.rotation.eulerAngles.z);
        if (x == correctAngle || x == correctAngle + 180)
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
                gameObject.transform.Rotate(0, 0, 45);
                SoundManager.instance.PlayRotationSfx();
                //int x = (int)gameObject.transform.rotation.eulerAngles.z;
                int x = Mathf.RoundToInt(gameObject.transform.rotation.eulerAngles.z);
                

                if (x==correctAngle || x==correctAngle+180)
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

    public bool IsCorrect ()
    {
        return isCorrect;
    }

    void UpdateStatus (bool status)
    {
        isCorrect = status;
        statusChanged?.Invoke(status);
    }

}
