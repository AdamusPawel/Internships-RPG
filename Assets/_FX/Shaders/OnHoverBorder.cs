using UnityEngine;

public class OnHoverBorder : MonoBehaviour
{
	public Material border;
	public Material nonBorder;

	void OnMouseOver()
	{
		GetComponent<Renderer>().material = border;
	}

	void OnMouseExit()
	{
		GetComponent<Renderer>().material = nonBorder;
	}
}
