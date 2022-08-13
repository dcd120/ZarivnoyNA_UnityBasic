using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraRotateAround : MonoBehaviour
{
	// ��������� �� ��������
	[SerializeField] public CinemachineVirtualCamera heroCamera;

	public Vector3 offset;

	[SerializeField] public float sensitivity = 3f;	// ���������������� �����
	[SerializeField] public float limit = 80f;		// ����������� �������� �� Y
	[SerializeField] public float zoom = 0.25f;		// ���������������� ��� ����������, ��������� �����
	[SerializeField] public float zoomMax = 10f;		// ����. ����������
	[SerializeField] public float zoomMin = 3f;		// ���. ����������

	private float X, Y;

	void Start()
	{
		limit = Mathf.Abs(limit);
		if (limit > 90) limit = 90;
	}

	void Update()
	{
		if (Input.GetAxis("Mouse ScrollWheel") > 0) offset.x += zoom;
		else if (Input.GetAxis("Mouse ScrollWheel") < 0) offset.z -= zoom;

		offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));

		X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
		Y += Input.GetAxis("Mouse Y") * sensitivity;
		Y = Mathf.Clamp(Y, -limit, limit);
		transform.localEulerAngles = new Vector3(-Y, X, 0);
		//transform.position = transform.localRotation * offset + target.position;
	}
}
