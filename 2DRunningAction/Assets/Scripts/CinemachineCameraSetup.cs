using Cinemachine;
using UnityEngine;

public class CinemachineCameraSetup : MonoBehaviour
{
    public string playerName = "Player";
    private CinemachineVirtualCamera cinemachineCamera;

    void Start()
    {
        cinemachineCamera = GetComponent<CinemachineVirtualCamera>();

        if (cinemachineCamera != null)
        {
            GameObject player = GameObject.Find(playerName);
            if (player != null)
            {
                cinemachineCamera.Follow = player.transform;
                cinemachineCamera.LookAt = player.transform;
            }
            else
            {
                Debug.LogError("Player 오브젝트를 찾을 수 없습니다. 이름이 올바른지 확인하세요.");
            }
        }
        else
        {
            Debug.LogError("Cinemachine Virtual Camera 컴포넌트를 찾을 수 없습니다.");
        }
    }
}
