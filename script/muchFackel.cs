using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class muchFackel : MonoBehaviour
{
    public GameObject fakel;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 120; i++)
        {
            GameObject k=Instantiate(fakel, new Vector2(-120 + 2 * i, 0.4f), new Quaternion(0, 0, 0, 0));
            k.transform.parent=transform;
        }
    }

}
