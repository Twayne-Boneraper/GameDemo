using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadow : MonoBehaviour
{
	private Transform shadowTransform;
	private SpriteRenderer shadowSprite;

	private Transform playerTransform;
	private SpriteRenderer playerSprite;

	public float activeTime;
	public float startTime;

	[Header("不透明度控制")]
	private float alpha;
	public float alphaSet;
	public float alphaMutiplier;


	void OnEnable()
	{
		playerSprite = GameObject.Find("Player").GetComponent<SpriteRenderer>();
		playerTransform = GameObject.Find("Player").transform;
		shadowSprite = GetComponent<SpriteRenderer>();

		transform.position = playerTransform.position;
		transform.localScale = playerTransform.localScale;
		transform.localRotation = playerTransform.localRotation;
		shadowSprite.sprite = playerSprite.sprite;

		alpha = alphaSet;

		startTime = Time.time;
	}

	// Update is called once per frame
	void Update()
	{

		alpha *= alphaMutiplier;

		Color color = new Color(1, 1, 1, alpha);

		shadowSprite.color = color;

		if (Time.time >= startTime + activeTime)
		{
			//返回对象池
			objectPool.instance.returnPool(this.gameObject);
		}
	}
}
