using UnityEngine;
using UnityEngine.InputSystem;

public class CarControl : MonoBehaviour
{
    ///// 실제 운전처럼 구현하기 위해
    //위쪽 방향키 = 악셀
    // D = 기어 D
    // R = 기어 R
    // 스페이스 = 브레이크
    
    // 좌 우 방향키 = 회전 방향만 바뀜

    // ⇒ D + 위쪽 방향키 = 전진
    // ⇒ R + 위쪽 방향키 = 후진
    
    
    // 위쪽 화살키: 악셀이라 하고 D 눌렀을 때 앞으로 R 눌렀을 때 뒤로
    // 악셀 밟는 힘의 크기 생각 + 길게 눌렀을 때라 생각하니까 그럼 앞으로 쭉 가다가 속도가 계속 빨라짐 그래서
    // 위쪽 화살표가 악셀이니 누르면 가속할 수 있도록
    
    public float moveSpeed = 5f; // 이동 속도
    public float rotationSpeed = 75f; // 회전 속도
    public float maxRotationAngle = 900f; // 최대 회전 각도 (승용차는 3회전이 최대, 스티어링  2.5바퀴)

    private float currentRotation = 0f; // 현재 회전 각도

    public float accelerationRate = 0.1f; // 가속도 비율
    public float decelerationRate = 0.1f; // 감속도 비율
    public float maxSpeed = 10f; // 최대 속도
    public float minSpeed = 0f; // 최소 속도
    
    //속도 방향 실제 이동 세 개로 분리해서 코드 정리
    
    
    void Update()
    {
        /// ?? 안 나아가고 중점에서 동그랗게 회전함
        /// 회전되는 값을 지정하고 일정 범위 안에서만 가능하게 하고
        /// 누르면 회전한 방향으로 앞으로 나아가야 함
        
        /// D를 누르면 방향키를 안 눌러도 오른쪽으로 회전함
        /// D가 wasd 할 때 입력돼서 그런 거 같음 (그렇다면 a 도 그럴 것)
        /// 방향키만 가능하게 하고 ad 의 방향은 안 받고 싶음
        /// 그냥 wasd가 앞으로 가고 옆으로 가고 이런 기능을 끄고 방향키만 가능하게 하자
        /// Unity 기본 설정이 그렇게 돼있으니까 이걸 고치기
        
        
        // 좌우 입력 처리
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
        
        // 위 화살표 키를 누르고 있는지 확인
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.UpArrow))
        {
            // 속도를 증가시킵니다
            moveSpeed = Mathf.Min(moveSpeed + accelerationRate * Time.deltaTime, maxSpeed);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
           
        }
        else if(Input.GetKey(KeyCode.Space))
        {
            // 속도를 감소시킵니다
            moveSpeed = Mathf.Max(moveSpeed - decelerationRate * Time.deltaTime, minSpeed);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        
        
        
        
        
        
        // 이동 벡터 초기화
        Vector3 moveDirection = Vector3.zero;

        // 위쪽 방향키와 D를 동시에 누를 때
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection = Vector3.forward;
        }
        // 위쪽 방향키와 R을 동시에 누를 때
        else if (Input.GetKey(KeyCode.R))
        {
            moveDirection = Vector3.back;
        }

        // 스페이스바를 누를 때 이동 중지
        if (Input.GetKey(KeyCode.Space))
        {
            moveDirection = Vector3.zero;
        }

        
        // 이동 처리
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}
