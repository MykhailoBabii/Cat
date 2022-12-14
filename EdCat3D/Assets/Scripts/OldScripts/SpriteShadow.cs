using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteShadow : MonoBehaviour {
	public Vector2 offset = new Vector2(-0.17f, -0.06f);

	private SpriteRenderer sprRndCaster;
	private SpriteRenderer sprRndShadow;

	private Transform transCaster;
	private Transform transShadow;

	public Material shadowMaterial;
	public Color shadowColor;

	void Start()
	{
		transCaster = transform;
		transShadow = new GameObject().transform;
		transShadow.parent = transCaster;
		transShadow.gameObject.name = "shadow";
		transShadow.localRotation = Quaternion.identity;

		sprRndCaster = GetComponent<SpriteRenderer>();
		sprRndShadow = transShadow.gameObject.AddComponent<SpriteRenderer>();

		sprRndShadow.material = shadowMaterial;
		sprRndShadow.color = shadowColor;
		sprRndShadow.sortingLayerName = sprRndCaster.sortingLayerName;
		sprRndShadow.sortingOrder = sprRndCaster.sortingOrder - 1;
	}

	void LateUpdate()
	{
		//transShadow.position = new Vector2 (transCaster.position.x + offset.x, transCaster.position.y + offset.y);
		transShadow.position = new Vector3 (transCaster.position.x - 0.05f, transCaster.position.y - 0.06f, transCaster.position.z);

		sprRndShadow.sprite = sprRndCaster.sprite;
	}
}
