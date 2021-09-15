using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl3trigLokot : MonoBehaviour
{
    public bool lokot=false;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        lokot = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        lokot = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        lokot = false;
    }
}
