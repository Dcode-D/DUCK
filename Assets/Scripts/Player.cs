using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{

    public static Player Instance { get; private set; } 

    [SerializeField]private float moveSpeed = 0;
    [SerializeField] private GameInput input;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform playerFollowTransform;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;
    private bool isWalking = false;
    Vector3 lastMoveDir = new Vector3(0,0,0);
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public void Awake()
    {
        if(Instance !=null)
        {
            Debug.LogError("Something wrong happened!");
        }
        Instance = this; 
    }
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    public void Start()
    {
        input.OnInputAction += Input_OnInputAction;
        input.OnInteractAlternate += Input_OnInteractAlternate;
    }

    private void Input_OnInteractAlternate(object sender, EventArgs e)
    {
        selectedCounter?.InteractAlternate(this);
    }

    private void Input_OnInputAction(object sender, System.EventArgs e)
    {
        selectedCounter?.Interact(this);
    }

    private void Update()
    {
        
        handleMovements();
        handleInteractions();
    }

    public bool getIsWalking()
    {
        return isWalking;
    }

    private void handleMovements()
    {
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;

        Vector2 inputvector = input.GetInputVectorNormalized();
        var moveDir = new Vector3(inputvector.x, 0, inputvector.y);
        if(moveDir!=Vector3.zero)
        {
            lastMoveDir = moveDir;
        }
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
            //check can move in x direction
            canMove = moveDirX!=Vector3.zero && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                //can move in x dir
                moveDir = moveDirX;
            }
            else
            {
                //else check can move in z direction
                Vector3 moveDirZ = new Vector3(0, 0, moveDirX.z);
                canMove = moveDirZ!=Vector3.zero && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    //can move in z direction
                    moveDir = moveDirZ;
                }
                else
                {
                    //cannot move in any dir
                    if (moveDir != Vector3.zero)
                    {
                        //make the charater face the moving dir
                        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * 10);
                    }
                }
            }
        }

        if (canMove)
        {
            isWalking = moveDir != Vector3.zero;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * 10);
            transform.position += moveDir * Time.deltaTime * moveSpeed;
        }
    }

    private void handleInteractions()
    {
        Vector2 inputvector = input.GetInputVectorNormalized();
        
        float instanceDistance = 2f;
        if( Physics.Raycast(transform.position, lastMoveDir, out RaycastHit raycastHit, instanceDistance, counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter clearCounter))
            {
                //has counter
                if(clearCounter != selectedCounter)
                {
                    changeCounter(clearCounter);
                }
                
            }
            else {
                // not a counter
                changeCounter(null);
            }
        }
        else
        {
            changeCounter(null );
        }
    }

    private void changeCounter(BaseCounter counter)
    {
        this.selectedCounter = counter;
        OnSelectedCounterChanged.Invoke(this,
                new OnSelectedCounterChangedEventArgs
                {
                   selectedCounter = selectedCounter,
                }
            );
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return playerFollowTransform;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
