using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class rain : MonoBehaviour
{
    public TilemapRenderer[] rainFrame;
    int i = 0;
    int numanim=0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(w());
    }

    IEnumerator w()
    {
        yield return new WaitForSeconds(0.12f);
        rainFrame[numanim].sortingOrder=-15 ;
        numanim++;
        if (numanim == 4) numanim = 0;
        rainFrame[numanim].sortingOrder = -5 ;
        StartCoroutine(w());
    }
}
