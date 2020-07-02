using UnityEngine;
using System.Collections;
using System;

public class CameraFollow : MonoBehaviour 
{
	[SerializeField] private float smooth = 2f;

	[Header("Camera Blocks")]
	[SerializeField] private Transform topLeft;
	[SerializeField] private Transform bottomRight;
	
	private Transform player;
	private float camWidth;
	private float camHeight;
	private Resolution resolution;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		ChangeResolution();
	}

	void LateUpdate ()
	{
		if (!Screen.currentResolution.Equals(resolution)) {
			ChangeResolution();
		}
		Vector3 newPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
		if (newPosition.x - camWidth/2 < topLeft.position.x) 
			newPosition.x =  topLeft.position.x + camWidth/2;

		if (newPosition.y + camHeight/2 > topLeft.position.y)
			newPosition.y = topLeft.position.y - camHeight/2;

		if (newPosition.x + camWidth/2 > bottomRight.position.x) 
			newPosition.x =  bottomRight.position.x - camWidth/2;

		if (newPosition.y - camHeight/2 < bottomRight.position.y)
		 	newPosition.y = bottomRight.position.y + camHeight/2;
		transform.position = Vector3.Lerp(transform.position, newPosition, smooth);
	}

    private void ChangeResolution()
    {
		camHeight = 2*Camera.main.orthographicSize;
  		camWidth = camHeight*Camera.main.aspect;
        resolution = Screen.currentResolution;
    }
}
