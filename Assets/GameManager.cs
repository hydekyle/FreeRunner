using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform player;
    public int velocity;
    public Transform scenario;

    private void Update ()
    {
        scenario.position = Vector3.MoveTowards (scenario.position, scenario.position + Vector3.left, Time.deltaTime * velocity);
    }
}
