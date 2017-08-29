using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    public LayerMask movementMask;
    public Interactable focus;

    PlayerMotor motor;
    Camera cam;

    private void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit,100, movementMask))
            {
                motor.Move2Point(hit.point);

                RemoveFocus();
            }
        }

		if (Input.GetMouseButtonDown(1))
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100))
			{
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
		}
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }

			focus = newFocus;
			motor.FollowTarget(newFocus);
        }
		newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }

		focus = null;

        motor.StopFollowingTarget();
    }
}
