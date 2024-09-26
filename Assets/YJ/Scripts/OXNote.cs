using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OXNote : MonoBehaviour
{
    private List<string> oxList = new List<string>();
    public GameObject oxPrefs;
    private Transform parent;

    private void Start()
    {
        oxList = ScoreManager.Instance.answerOX; // 이거 저장시켜 
        Debug.Log(string.Join(", ", oxList));

        parent = GameObject.Find("Canvas/Result/ErrorNote/OX").transform;

        SetOXNote();
    }

    private void SetOXNote()
    {
        for (int i = 0; i < oxList.Count; i++)
        {
            // Instantiate and set parent
            GameObject spawnedOX = Instantiate(oxPrefs);
            spawnedOX.transform.SetParent(parent, false); // 로컬 스케일 유지

            // Set order text
            TMP_Text orderText = spawnedOX.GetComponentInChildren<TMP_Text>();
            orderText.text = $"{i + 1}"; // 순서 텍스트 설정

            // Find the result text transform
            //TMP_Text resultText = orderText.GetComponentInChildren<TMP_Text>();
            Transform resultTransform = orderText.transform.Find("Result");
            TMP_Text resultText = resultTransform.GetComponent<TMP_Text>();
            
            
            //Transform resultTransform = spawnedOX.transform.Find("OX/OX/Order/Result");
            //TMP_Text resultText = resultTransform.GetComponent<TMP_Text>();

            // Set the result text based on oxList value
            if (resultText != null)
            {
                resultText.text = oxList[i]; // O 또는 X로 설정
            }
            else
            {
                Debug.LogError("Result text component not found in spawnedOX.");
            }
        }
    }
}