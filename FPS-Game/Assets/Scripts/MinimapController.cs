using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    private Transform player;
    public float yOffset = 20;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y += yOffset;
        transform.position = newPosition;
    }
}
