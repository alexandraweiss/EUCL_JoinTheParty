using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class Player : MonoBehaviour
{
    public float raisedDistance = 0.3f;
    public float aggressionStep = 0.001f;
    public NPCManager npcManager;
    public ActionBasedController leftController;
    public ActionBasedController rightController;
    public LineRenderer rightRay;
    public TrackedPoseDriver head;
    protected Vector3 rightPosition;
    protected Vector3 leftPosition;
    protected Vector3 headPosition;
    protected bool rightControllerActive = false;
    protected bool leftControllerActive = false;

    private void Start()
    {
        rightController.enableInputActions = true;
        leftController.enableInputActions = true;

        //head.positionAction.performed += OnHeadMoving;
        head.positionInput.action.performed += OnHeadMoving;
        rightController.positionAction.action.performed += OnRightControllerMoved;
        leftController.positionAction.action.performed += OnLeftControllerMoved;

        rightController.trackingStateAction.action.performed += OnRightControllerActivated;
        leftController.trackingStateAction.action.performed += OnLeftControllerActivated;

    }

    private void Update()
    {
        if (Time.time < 8)
        {
            return;
        }
        if (rightControllerActive)
        {
            //Debug.Log(string.Format("  {0}  {1} {2} ", Vector3.Distance(headPosition, rightPosition), rightPosition.y, headPosition.y));
            if ((rightPosition.y - headPosition.y) >= raisedDistance)
            { 
                OnRightHandRaised();
            }
            else if((rightPosition.y - headPosition.y) < 0)
            {
                OnRightHandLowered();
            }
        }

        if (leftControllerActive)
        {
            //Debug.Log(Vector3.Distance(headPosition, leftPosition));
            if ((leftPosition.y - headPosition.y) >= raisedDistance)
            {
                OnLeftHandRaised();
            }
            else if ((leftPosition.y - headPosition.y) < 0)
            {
                OnLeftHandLowered();
            }
        }
    }

    protected void OnRightHandRaised()
    {
        npcManager.AddAggression(aggressionStep);
    }

    protected void OnLeftHandRaised()
    {
        npcManager.AddAggression(aggressionStep);
    }

    protected void OnRightHandLowered()
    {
        npcManager.AddAggression(-aggressionStep);
    }

    protected void OnLeftHandLowered()
    {
        npcManager.AddAggression(-aggressionStep);
    }

    protected void OnHeadMoving(InputAction.CallbackContext c)
    {
        headPosition = c.ReadValue<Vector3>();
        headPosition.y -= 0.2f;
    }

    protected void OnRightControllerMoved(InputAction.CallbackContext c)
    {
        rightPosition = c.ReadValue<Vector3>();
    }

    protected void OnLeftControllerMoved(InputAction.CallbackContext c)
    {
        leftPosition = c.ReadValue<Vector3>();
    }

    protected void OnRightControllerActivated(InputAction.CallbackContext c)
    {
        Debug.LogWarning(".... activate right");
        rightControllerActive = c.ReadValue<int>() > 0;
    }

    protected void OnLeftControllerActivated(InputAction.CallbackContext c)
    {
        Debug.LogWarning(".... activate left");
        leftControllerActive = c.ReadValue<int>() > 0;
    }
}
