using System;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class NewCarControl : MonoBehaviour
{
    
    
    /// 차를 움직이는 코드를 만들고 버튼을 누르면 실행되도록 온클릭이벤트를 버튼에 연결하기
    /// 앞으로 가는 코드, 멈추는 코드, 뒤로 가는 코드 를 각각 D / P, N / R 버튼에 연결
    /// 차가 자동으로 좀 굴러가니까
    
    
        /// 자동차 이동   
        public float speed = 1.5f; // 자동차의 기본 이동 속도
        public float acceleration = 1f;  // 가속도
        public float maxSpeed = 5f; // 최대 속도
        public float deceleration = 2f; // 감속도
        public float currentSpeed = 0f; // 현재 속도
        
        
        private bool isMovingForward = false;
        private bool isMovingBackward = false;
        private bool isMovingFastForward = false;
        private bool isBreaking = false;
        
        
        /// 자동차 회전
        public float rotationSpeed = 75f; // 회전 속도
        public float maxRotationAngle = 900f; // 최대 회전 각도 (승용차는 3회전이 최대, 스티어링  2.5바퀴)
        

        public Transform handleTransform;
        
        private float currentRotation = 0f; // 현재 회전 각도


        public TMP_Text speedText;
        
        /// 자동차 악셀, 브레이크
        /// 악셀은 클릭한 시간에 비례해서 길게 누르면 서서히 빨라지고, 
        /// 브레이크는 클릭한 시간에 비례해서 길게 누르면 서서히 느려짐


        /*
        public Vector3 newGravity = new Vector3(0, 50f, 0);

        private void Start()
        {
            Physics.gravity = newGravity;
        }*/

        void Update()
        {
            // 정수로 나오게 하고 싶어서 int 로 바꿔줌
            float speedInKmh = Mathf.FloorToInt(currentSpeed * 5); // 기존 유니티 속도는 m/s라 km/h 로 바꿔서 표현한다 생각 *5는 그냥 내가 임의로 지정한 거임

            if (speedInKmh >= 0)
            {
                speedText.text = $"{speedInKmh} km/h";
            }
            else
            {
                speedText.text = "0km/h";
            }
            
            
            
            
            if (isMovingForward)
            {
                if (!isBreaking)
                {
                    if (currentSpeed < speed)
                    {
                        currentSpeed += deceleration * Time.deltaTime;
                    }
                }

                transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
            }
            else if (isMovingBackward)
            {
                if (!isBreaking)
                {
                    if (currentSpeed > -speed)
                    {
                        currentSpeed -= deceleration * Time.deltaTime;
                    }
                }

                transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
            }

            
            /// 자동차 회전  -> 좌우 방향키로 회전
            float horizontalInput = Input.GetAxis("Horizontal");
            
            if (isMovingBackward)
            {
                horizontalInput = -horizontalInput;
            }
        
            //회전 방향 계산
            float rotationAmount = horizontalInput * rotationSpeed * Time.deltaTime;
        
            // 현재 회전에 새로운 회전량을 더해서 각도를 계산
            float newRotation = currentRotation + rotationAmount;
        
            // 새로운 회전 각도가 허용 범위 안에 있는지 확인
            // if(newRotation > -maxRotationAngle && newRotation < maxRotationAngle)
            {
                
                
                
            transform.Rotate(0, rotationAmount, 0);
            currentRotation = newRotation; // 회전 각도 업데이트
            }
            
            // 0으로 바꿔줘야 됨
            
            

          
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene("Fail");
            }
        }

        public void MoveForward()  // 앞으로 가는 코드 D
        {
            isMovingForward = true;
            isMovingBackward = false;
        }
        
        public void MoveBackward() // 뒤로 가는 코드 R
        {
            isMovingForward = false;
            isMovingBackward = true;
        }
    
        public void MoveStop() // 멈추는 코드 P N
        {
            isMovingForward = false;
            isMovingBackward = false;
            isMovingFastForward = false;
        }

        public void Accel(float value)
        {
            if (isMovingBackward)
                return;
            
            currentSpeed += value * acceleration * Time.deltaTime; // 시간에 따라 속도 증가
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed); // 최대 속도 제한
        }

        public void Break(float value)
        {
            if (value < 0.5f)
                isBreaking = true;
            else if (value > 0.7f)
                isBreaking = false;
            
            currentSpeed -= deceleration * Time.deltaTime; // 시간에 따라 속도 감소
            currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed); // 최소 속도는 0
        }


        // -1부터 +1까지의 값들을 -120부터 120까지 바꿔주기
        
        public void Handle(float value)
        {
            handleTransform.localRotation = Quaternion.Euler(26.078f, -value * maxRotationAngle, 0);
        }
    
        
}
