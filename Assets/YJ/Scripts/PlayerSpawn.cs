using Fusion;
using UnityEngine;

public class PlayerSpawn : SimulationBehaviour, IPlayerJoined
{
    public GameObject playerPrefab; 
    public Sprite[] playerImages; // 플레이어 이미지를 저장할 배열
    private GameObject parentObject;
    private Vector3 parentTransform;
    
    public void PlayerJoined(PlayerRef player)
    {
        parentObject = GameObject.Find("Players");
        parentTransform = parentObject.transform.position;
       
        Debug.Log("플레이서 스폰 할라고");
       
        if (player == Runner.LocalPlayer)
        {
            // 플레이어가 생성되어 있으면 안되게 하기 
            Debug.Log("플레이어 스폰");
            var playerOp = Runner.Spawn(playerPrefab, parentTransform, Quaternion.identity);
            //playerOp.transform.SetParent(parentObject.transform);
            //PlayerControll.Instance.RpcSetNickname(LoginManager.Value);
        }

    }
}
    /*
    private void ChangePlayerImage(NetworkObject playerObject, int playerOrder)
    {
        var playerImage = playerObject.GetComponent<Image>(); // 자식 오브젝트의 Image 컴포넌트를 찾기

        if (playerOrder >= 0 && playerOrder < playerImages.Length) // 배열의 범위 내인지 확인
        {
            playerImage.sprite = playerImages[playerOrder]; // 이미지 변경
            Debug.Log($"플레이어 이미지 변경됨: {playerImages[playerOrder].name}");
            //RpcChangePlayerImage(playerOrder); // 모든 클라이언트에게 이미지 변경 요청
        }
        else
        {
            Debug.LogError("유효하지 않은 플레이어 순서입니다.");
        }
    }

    /*
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RpcChangePlayerImage(int playerOrder)
    {
        var playerObject = Runner.GetPlayerObject(Runner.LocalPlayer); // 자신의 플레이어 오브젝트 가져오기
        var playerImage = playerObject.GetComponent<Image>(); // 자식 오브젝트의 Image 컴포넌트를 찾기

        if (playerOrder >= 0 && playerOrder < playerImages.Length) // 배열의 범위 내인지 확인
        {
            playerImage.sprite = playerImages[playerOrder]; // 이미지 변경
            Debug.Log($"모든 플레이어에게 이미지 변경됨: {playerImages[playerOrder].name}");
        }
    }
    */
    
