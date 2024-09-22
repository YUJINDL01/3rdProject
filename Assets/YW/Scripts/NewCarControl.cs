using UnityEngine;

public class NewCarControl : MonoBehaviour
{
    
    
    /// 차를 움직이는 코드를 만들고 버튼을 누르면 실행되도록 온클릭이벤트를 버튼에 연결하기
    /// 앞으로 가는 코드, 멈추는 코드, 뒤로 가는 코드 를 각각 D / P, N / R 버튼에 연결
    /// 차가 자동으로 좀 굴러가니까
    
    
        /// 자동차 이동   
        public float speed = 5f; // 자동차의 기본 이동 속도
        public float accelSpeed = 10f;  // 자동차 가속 시 속도
        private bool isMovingForward = false;
        private bool isMovingBackward = false;
        private bool isMovingFastForward = false;
        
        
        /// 자동차 회전
        public float rotationSpeed = 75f; // 회전 속도
        public float maxRotationAngle = 900f; // 최대 회전 각도 (승용차는 3회전이 최대, 스티어링  2.5바퀴)
        
        private float currentRotation = 0f; // 현재 회전 각도
        
        
        /// 자동차 악셀, 브레이크
        /// 악셀은 클릭한 시간에 비례해서 길게 누르면 서서히 빨라지고, 
        /// 브레이크는 클릭한 시간에 비례해서 길게 누르면 서서히 느려짐
    
        void Update()
        {
            // 자동차 이동
            if (isMovingForward)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            else if (isMovingBackward)
            {
                transform.Translate(Vector3.back * speed * Time.deltaTime);
            }
            else if (isMovingFastForward)
            {
                transform.Translate(Vector3.forward * accelSpeed * Time.deltaTime); // D나 R로 놔둔 상태에서 가속할 수 있으니까 현재 속도에 가속하는 속도를 더해야 움직이면서도 더 가속될 듯
            }
            
            
            /// 자동차 회전  -> 좌우 방향키로 회전
            float horizontalInput = Input.GetAxis("Horizontal");
        
            //회전 방향 계산
            float rotationAmount = horizontalInput * rotationSpeed * Time.deltaTime;
        
            // 현재 회전에 새로운 회전량을 더해서 각도를 계산
            float newRotation = currentRotation + rotationAmount;
        
            // 새로운 회전 각도가 허용 범위 안에 있는지 확인
            if(newRotation > -maxRotationAngle && newRotation < maxRotationAngle)
            {
                transform.Rotate(0, rotationAmount, 0);
                currentRotation = newRotation; // 회전 각도 업데이트
            }
            
            
            
            
        }
    
        public void MoveForward()  // 앞으로 가는 코드 D
        {
            isMovingForward = true;
        }
        
        public void MoveBackward() // 뒤로 가는 코드 R
        {
            isMovingBackward = true;
        }
    
        public void MoveStop() // 멈추는 코드 P N
        {
            isMovingForward = false;
            isMovingBackward = false;
            isMovingFastForward = false;
        }

        public void MoveFastForward()
        {
            isMovingFastForward = true;
        }
        
    
        
}
