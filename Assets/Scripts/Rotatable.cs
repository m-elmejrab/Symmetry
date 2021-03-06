using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Rotatable : MonoBehaviour
{

    public event Action<bool> statusChanged;
    public int correctAngle;
    bool isCorrect;

    // Use this for initialization
    void Awake()
    {
        int x = Mathf.RoundToInt(gameObject.transform.rotation.eulerAngles.z);
        if (x == correctAngle || x == correctAngle + 180)
        {
            isCorrect = true;
        }
        else
        {
            isCorrect = false;
        }
    }

    void OnMouseDown()
    {

        Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        if (Physics.Raycast(raycast, out raycastHit))
        {
            if (raycastHit.collider.name == gameObject.name)
            {
                gameObject.transform.Rotate(0, 0, 45);
                SoundManager.instance.PlayRotationSfx();
                int x = Mathf.RoundToInt(gameObject.transform.rotation.eulerAngles.z);

                if (x == correctAngle || x == correctAngle + 180)
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

    /// <summary>
    /// Returns status of rotatable object
    /// </summary>
    public bool IsCorrect()
    {
        return isCorrect;
    }

    /// <summary>
    /// Updates object status after rotation and raises required event
    /// </summary>
    void UpdateStatus(bool status)
    {
        isCorrect = status;
        statusChanged?.Invoke(status);
    }

}
