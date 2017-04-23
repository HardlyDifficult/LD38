using UnityEngine;

public static class RandomHelper
{
    public static Vector3 RandomPointOnObject(GameObject centreObject, float distanceFromGround = 1.0f, int retry = 0)
    {
        //Find a random point in the world based off the centre object.
        Vector3 randomPoint = centreObject.transform.position +
            Random.insideUnitSphere.Times(centreObject.transform.localScale * 10);

        //Calculate the direction to that object.
        Vector3 directionToObject = centreObject.transform.position - randomPoint;

        //Return the hit point if we did hit the surface.
        RaycastHit hit;
        if (Physics.Raycast(randomPoint, directionToObject, out hit, Mathf.Infinity, 
            LayerMask.GetMask("Planet")))
        {
            //We return the hit point and add some offset to the from the negative direction of the centre object.
            //This will prevent worms clipping through the earth, bump up distanceFromGround if this occurs.
            return hit.point + (-directionToObject.normalized * distanceFromGround);
        }
        else
        {
            //The point in space was inside of the circle, lets retry.
            //Also capping the retries preventing a stackoverflow.
            if (retry > 10)
                return Vector3.zero;
            else
                retry++;

            return RandomPointOnObject(centreObject, retry);
        }

    }

}
