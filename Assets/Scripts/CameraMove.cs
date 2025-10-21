using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float speed;
    public Transform player;

    private void Update()
    {
        var lerpX = Mathf.Lerp(transform.position.x, player.position.x, speed * Time.deltaTime);
        transform.position = new Vector3(lerpX, transform.position.y, transform.position.z);
    }
}