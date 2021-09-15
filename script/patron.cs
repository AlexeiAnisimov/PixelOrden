using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patron : MonoBehaviour
{
    public string hoziain;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "patron";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name!=hoziain)
            Destroy(gameObject);
    }
}
