using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static readonly Utils Instance = new Utils();

    public float CalculateAngle(Transform myTransform, Vector3 targetPosition)
    {
        Vector3 myPosition = myTransform.position;
        myPosition.y = 0f;

        targetPosition.y = 0f;

        Vector3 targetDir = (targetPosition - myPosition).normalized;

        float dot = Mathf.Clamp(Vector3.Dot(targetDir, myTransform.forward), -1f, 1f);

        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

        Vector3 cross = Vector3.Cross(myTransform.forward, targetDir);

        if (0f > cross.y)
        {
            angle *= -1;
        }

        return angle;
    }
}
