using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPool : MonoBehaviour
{
	public static objectPool instance;

	public GameObject shadowPrefab;
	public int shaowCount;
	public Queue<GameObject> availabObjects = new Queue<GameObject>();

	void Awake()
	{
		instance = this;

		FillPool();
	}

	void FillPool()
	{
		for (int i = 0; i < shaowCount; i++)
		{
			GameObject go = Instantiate(shadowPrefab);
			go.transform.SetParent(transform);
			//go.SetActive(false);
			//availabObjects.Enqueue(go);
			returnPool(go);
		}
	}

	public void returnPool(GameObject go)
	{
		go.SetActive(false);
		availabObjects.Enqueue(go);
	}

	public GameObject GetFromPool()
	{
		if (availabObjects.Count == 0)
		{
			FillPool();
		}
		GameObject outPool = availabObjects.Dequeue();
		outPool.SetActive(true);
		return outPool;
	}
}
