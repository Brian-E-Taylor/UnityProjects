using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
	public float smooth = 1.5f;

	private Transform lookPosition;
	private Vector3 relCameraPos;
	private float relCameraPosMag;
	private Vector3 newPos;

	void Start()
	{
		lookPosition = GameObject.FindGameObjectWithTag(Tags.player1).transform;

		relCameraPos = transform.position - lookPosition.position;
		relCameraPosMag = relCameraPos.magnitude - 0.5f;
	}

	void FixedUpdate()
	{
		Vector3 standardPos = lookPosition.position + relCameraPos;

		Vector3 abovePos = lookPosition.position + Vector3.up * relCameraPosMag;

		Vector3[] checkPoints = new Vector3[5];

		checkPoints[0] = standardPos;

		checkPoints[1] = Vector3.Lerp(standardPos, abovePos, 0.25f);
		checkPoints[2] = Vector3.Lerp(standardPos, abovePos, 0.5f);
		checkPoints[3] = Vector3.Lerp(standardPos, abovePos, 0.75f);

		checkPoints[4] = abovePos;

		for (int i = 0; i < checkPoints.Length; i++)
		{
			if (ViewingPosCheck(checkPoints[i]))
				break;
		}

		transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime);

		SmoothLookAt();
	}

	public void SetViewPosition(Transform lookPos)
	{
		lookPosition = lookPos;
	}

	bool ViewingPosCheck(Vector3 checkPos)
	{
		RaycastHit hit;

		if (Physics.Raycast(checkPos, lookPosition.position - checkPos, out hit, relCameraPosMag))
			if (hit.transform != lookPosition)
				return false;

		newPos = checkPos;
		return true;
	}

	void SmoothLookAt()
	{
		Vector3 relPosition = lookPosition.position - transform.position;

		Quaternion lookAtRotation = Quaternion.LookRotation(relPosition, Vector3.up);

		transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, smooth * Time.deltaTime);
	}
}
