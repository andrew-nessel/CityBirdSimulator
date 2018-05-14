using UnityEngine;
using System.Collections;
public class IntegrationMethods : MonoBehaviour
{
    public static void BackwardEuler(
        float h,
        Vector3 currentPosition,
        Vector3 currentVelocity,
        out Vector3 newPosition,
        out Vector3 newVelocity)
    {
        //Init acceleration
        //Gravity
        Vector3 acceleartionFactor = Physics.gravity;

        //Main algorithm
        newVelocity = currentVelocity + h * acceleartionFactor;

        newPosition = currentPosition + h * newVelocity;
    }
}