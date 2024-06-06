using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 movementVector;
    [SerializeField] float playerSpeed = 5f;
    [SerializeField] float playerVelocity = 0f;
    [SerializeField] float playerTurnRate = 5f;
    [SerializeField] float playerDeltaVelocity = 5f;
    [SerializeField] float maxVelocity = 35;
    GameObject Line;

    public Vector3 MooveWorld=new Vector3(0,0,0);
    public Quaternion RotateWorld=Quaternion.Euler(0,0,0);

    void Start()
    {
        Line= GameObject.Find("SpeedLine");
    }

    public void PlayerMovement(InputAction.CallbackContext ctx)
    {
        movementVector = ctx.ReadValue<Vector2>();
    }

    public void Exit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if (movementVector.y != 0&&playerVelocity<maxVelocity) { playerVelocity += movementVector.y * playerSpeed * Time.deltaTime; }
        if (playerVelocity > 0) 
        {
            playerVelocity -= playerDeltaVelocity*Time.deltaTime;
        }
        else if(playerVelocity < 0)
        {
            playerVelocity += playerDeltaVelocity * Time.deltaTime;
        }

        this.gameObject.transform.rotation*= Quaternion.Euler(0, movementVector.x * playerTurnRate * Time.deltaTime, 0);
        RotateWorld *= this.gameObject.transform.rotation;
        MooveWorld = this.gameObject.transform.rotation*Vector3.forward * playerVelocity * Time.deltaTime;
        Line.transform.rotation = Quaternion.Euler(0, 0, 120 - (playerVelocity/60*360));
    }
}
