using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    //player
    Score eggScore;
    // Animator anim;
    HealthManager Health;
    GameObject weapon;
    Transform weaponOrigin;
    string playersWeapon;
    private float walkSpeed, runSpeed;
    int keyCounter;
    private Vector3 moveDir, weaponDir;
    private Quaternion playersCurrentRot;
    private bool canPlayerMove, isPlayerMoving, isPlayerCrouching, canShoot;
    List<string> inventory;
    // private GameObject pickupItem, rightHand, leftHand, flashlight;

    //cam
    public Camera firstPersonCam;
    private Vector3 camOffset;
    private float camMaxDownRot, camMaxUpRot, mouseSensitivityX, mouseSensitivityY;

   /* private enum GameStates
    {
        //cutscene - no controller, playerPanic - rotation only(freeze in fear),
        //playerNormal- all controllers
        cutScene,
        playerPanic,
        playerNormal
    }
    private GameStates currentState;
    */

    // Start is called before the first frame update
    void Start()
     {
    //     playersPocket = gameObject.GetComponent<Inventory>();
    //     eggScore = GameObject.Find("PlayerUI").GetComponent<Score>();
        // anim = GetComponent<Animator>();
        //player
        // currentState = GameStates.playerNormal;
        Health = new HealthManager(50f);
        walkSpeed = 2.0f;
        runSpeed = 3.5f;
        canPlayerMove = true;
        inventory = new List<string>();
        keyCounter = 0;
        //weapons
        weaponOrigin = this.transform.Find("WeaponSpawner");
        playersWeapon = "axe"; //<------------------ NEED TO BE DEPENDING ON THE CHARACTER SELECTED
        //grab flashlight
        // GrabItem(rightHand, flashlight);

        //cam
        camMaxDownRot = 50f;
        camMaxUpRot = 335f;
        mouseSensitivityX = 5f;
        mouseSensitivityY = 3.5f;
        //set cam position and orientation at beggining
        firstPersonCam.transform.position = this.transform.position;
        // firstPersonCam.transform.rotation = this.transform.rotation;
         SetCam();
    }

    // Update is called once per frame
    void Update()
    {
         SetCam();
         GameStateManager();

         Debug.Log("player Health: "+Health.checkHealth);
    }

    void SetCam()
    {
        firstPersonCam.transform.rotation = this.transform.rotation;
        camOffset = new Vector3(0, 0.3572625f, -0.001f);
        firstPersonCam.transform.position = this.transform.position + camOffset;
    }

    void GameStateManager()
    {
        // switch(currentState)
        // {
        //     case GameStates.cutScene:
        //         break;
        //     case GameStates.playerNormal:
                PlayerControllers(walkSpeed);
        //         break;
        //     case GameStates.playerPanic:
        //         PlayerControllers(panicSpeed);
        //         break;
        //     default:
        //         break;

        // }
    }

    void PlayerControllers(float speed)
    {
        canShoot = false;

        #region WASD
        //run
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }

        if(Input.GetKey(KeyCode.W))
        {
            MovePlayer(Vector3.forward, speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            MovePlayer(Vector3.back, speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            MovePlayer(Vector3.left, speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            MovePlayer(Vector3.right, speed);
        }
        #endregion

        if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            isPlayerMoving = false;
        }

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            weapon = Instantiate(Resources.Load("weapons/" + playersWeapon, typeof(GameObject)), weaponOrigin.position, this.transform.rotation) as GameObject;

            // weaponDir = new Vector3(Mathf.Sin(this.transform.rotation.x), 0, -Mathf.Cos(this.transform.rotation.z));
            weapon.transform.Rotate(moveDir);
            //canShoot = true;
        }

        //interact OR AUTOPICK UP???      <---------------
        if (Input.GetKey(KeyCode.E))
        {
            // if(canPlayerGetEgg)
            // {
            //     playersPocket.inventoryList.Add(pickupItem);
            //     Destroy(pickupItem);
            //     eggScore.AddPoints(1);
            //     canPlayerGetEgg = false;
            // }
        }

        RotatePlayer();
        WalkAnimate();
    }

    private void MovePlayer(Vector3 direction, float speed)
    {
        if(canPlayerMove)
        {
            this.transform.Translate(direction * speed * Time.deltaTime);
            isPlayerMoving = true;
        }
    }

    private void WalkAnimate()
    {
        //anim.SetBool("playerWalking", isPlayerMoving);
        //anim.SetLayerWeight(2, 0.6f);
    }

    private void CrouchAnimate(bool isCrouching)
    {
        //anim.SetBool("playerCrouch", isCrouching);
        if(isCrouching)
        {
            isPlayerMoving = false;
        }
        canPlayerMove = !isCrouching;
    }

    private void RotatePlayer()
    {
        moveDir = new Vector3(0, Input.GetAxis("Mouse X") * mouseSensitivityX, 0);
        this.transform.Rotate(moveDir);
        CamRotation(this.transform.rotation);
    }

    private void CamRotation(Quaternion playerRot)
    {
        //Cam side rotation is with player rotation (cam needs to be players child)

        //camera "up & down" movement
        playerRot.x += Input.GetAxis("Mouse Y") * mouseSensitivityY;
        firstPersonCam.transform.Rotate(new Vector3(-playerRot.x, 0, 0));

        //limit cam rotation on x (up and down)
        Vector3 camAngle = firstPersonCam.transform.eulerAngles;

        if (playerRot.x > 0 && (camAngle.x <= camMaxUpRot && camAngle.x >= camMaxDownRot + 20))
        {
            // going up
            camAngle.x = camMaxUpRot;
        }

        if (playerRot.x < 0 && (camAngle.x >= camMaxDownRot && !(camAngle.x >= camMaxDownRot + 30) ))
        {
            //going down
            camAngle.x = camMaxDownRot;
        }
        firstPersonCam.transform.rotation = Quaternion.Euler(camAngle);

    }

    private void TakeDamage(float damage)
    {
        Health.TakeDamage(damage);
            if(!Health.StillAlive())
                Debug.Log("U ded mate :C");
    }

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("collision enter");
        if(col.transform.tag == "Enemy")
        {
           TakeDamage(col.transform.GetComponent<Enemies>().DamagePower);
        }


    }

    private void OnTriggerEnter(Collider col)
    {
        string objName = col.transform.name;
        
        if(col.transform.tag == "Pickable")
        {
            switch(objName)
            {
                case "key":
                    keyCounter++;
                    Debug.Log("key");
                    // inventory.Add(); 
                    break;
                case "chest":
                    Debug.Log("chest");
                    // GetGold(ammount);
                    break;
                default:
                    Debug.Log("idk");
                    break;
            }
                Destroy(col.gameObject);
        }
        else if(col.transform.tag == "door")
        {
            OpenDoor(col.gameObject);
        }
    }

    private void OnCollisionStay(Collision col)
    {
        if(col.transform.tag == "Enemy")
        {
            /* TODO <-----------------------------(fdjsgkf-------
             * after a hit take a "cool down" for it to happen again (ex: 1.5 sec)
             *after it if still touchign then damage again
            */
            //TakeDamage(col.transform.GetComponent<Enemies>().DamagePower);
        }
    }

    void GrabItem(GameObject hand, GameObject item)
    {
        item.transform.parent = hand.transform;
        item.transform.position = this.transform.position;
    }

    private void OpenDoor(GameObject door)
    {
        if(keyCounter >= 1)
        {
            keyCounter--;
            //TODO <--------------------2345----_r4rw4r----
            //play door opening animation 
            //destroy doorafter it
            Destroy(door);
        }
    }

}
