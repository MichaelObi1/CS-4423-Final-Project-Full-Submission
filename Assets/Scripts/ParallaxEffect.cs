using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    //Starting position for the parallax game object.
    Vector2 startPos;

    //Starting z value for the parallax game object.
    float startingZ;

    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //When the target is moved, the parallax object 
        //is moved to the same distance times a mulitplier.
        Vector2 newPos = startPos + camMoveSinceStart * parallaxFactor;

        //The X/Y position changes, but the z value stays the same.
        transform.position = new Vector3(newPos.x, newPos.y, startingZ);
    }
}
