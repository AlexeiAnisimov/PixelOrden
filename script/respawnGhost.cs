using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnGhost : MonoBehaviour
{
    int hp;
    Transform playerTrans;
    public GameObject ghost;
    // Start is called before the first frame update
    void Start()
    {
        playerTrans = GetComponent<Transform>();
        hp = GetComponent<movement>().hp;
        StartCoroutine(W());
    }
    IEnumerator W()
    {
        yield return new WaitForSeconds(8f);
        StartCoroutine(ghostSpawn());
    }
    IEnumerator ghostSpawn()
    {
        if(hp>80)
            yield return new WaitForSeconds(hp/40);
        else yield return new WaitForSeconds(2);
        Debug.Log(GetComponent<movement>().typeZemli);
        if (GetComponent<movement>().typeZemli == "pesok")
        {
            Instantiate(ghost, new Vector3(playerTrans.position.x+(Mathf.Sign(Random.RandomRange(0f,1f)-0.5f)*Random.RandomRange(3,3)), playerTrans.position.y+0.5f, playerTrans.position.z), playerTrans.rotation);
        }
        StartCoroutine(ghostSpawn());
    }
}
