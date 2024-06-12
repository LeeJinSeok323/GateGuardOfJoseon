using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAi.Examples;

public class PlayerCtrl2 : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float lookSensitivity;

    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX;

    [SerializeField]
    private Camera theCamera;
    private Rigidbody myRigid;

    Vector3 position;

    LockMouseControl mouse;
    bool isNpcThere;

    void Start()
    {
        myRigid = GetComponent<Rigidbody>();  // private
        position = new Vector3(-1, 0, -1);
        mouse =GetComponent<LockMouseControl>();
    }

    void Update()  
    {
        Move();                 // 키보드 입력에 따라 이동
        if(!LockMouseControl.isMouseVisible)
        {
            CameraRotation();       // 마우스를 위아래(Y) 움직임에 따라 카메라 X 축 회전 
            CharacterRotation();    // 마우스 좌우(X) 움직임에 따라 캐릭터 Y 축 회전
        }
         

        if (Input.GetKeyDown(KeyCode.Space))
        {

            //NpcManager.Instance.Remove(0);
            //NpcManager.Instance.ChagneToWalk(0, position);
        }


    }

    void OnTriggerEnter(Collider col){
            Debug.Log($"옆에 npc 이름{col.gameObject.name}");
            isNpcThere = true;
            GptManager.Instance.Output = col.gameObject.GetComponent<NpcBehavior_Gate>().NpcChat;
        }
        
    void OnTriggerExit(Collider col){
        isNpcThere = false;
        Invoke("resetText", 5.0f);
    }
    void resetText(){
        if(!isNpcThere && GptManager.Instance.Output != null)
            GptManager.Instance.Output.text = null;
    }


    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");
        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    private void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;

        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    private void CharacterRotation()  // 좌우 캐릭터 회전
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY)); // 쿼터니언 * 쿼터니언
        // Debug.Log(myRigid.rotation);  // 쿼터니언
        // Debug.Log(myRigid.rotation.eulerAngles); // 벡터
    }
}