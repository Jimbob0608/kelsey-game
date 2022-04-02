using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


public class FollowPlayerMovement : MonoBehaviour {
    
    public Transform player;
    private Vector3 position;
    
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() {
        position = player.position;
        transform.position = new Vector3(position.x, position.y, -10);
    }
}