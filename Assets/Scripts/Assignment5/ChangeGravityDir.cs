using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeGravityDir : MonoBehaviour

{
	private ConstantForce cForce;
	private Vector3 forceDirection;

	[Header("Input Actions")]
	public InputActionProperty rightHandGrab;
	public InputActionProperty leftHandGrab;

	void Start()
	{
		cForce = GetComponent<ConstantForce>();
		forceDirection = new Vector3(0, -10, 0);
		cForce.force = forceDirection;
	}

	void Update()
	{
		if (rightHandGrab.action.IsPressed() || leftHandGrab.action.IsPressed())
		{
			forceDirection = forceDirection * -1;
			cForce.force = forceDirection;
		}
	}
}
