using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; //Tilemap

public class TransparentPlatform : MonoBehaviour
{
    Tilemap tilemap;

    void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Only player hits the platforms
        if (collision.gameObject.name == "player")
            Appear();
    }

    void Appear()
    {
        //Set alpha as 1 
        tilemap.color = new Color(1, 1, 1, 1);
    }

}
