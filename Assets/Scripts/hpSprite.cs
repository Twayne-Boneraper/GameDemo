using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpSprite : MonoBehaviour
{
    public GameObject hpSpritePrefab;
    public List<GameObject> hpPrefabList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            hpPrefabList.Add(Instantiate(hpSpritePrefab,transform));
        }
        //hpPrefabList[2].SetActive(false);
    }

}
