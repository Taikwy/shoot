using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrapInfo : MonoBehaviour
{
    public TextMeshProUGUI numScrap;

    public void SetText(float scrapCount){
        numScrap.text = scrapCount.ToString();
    }

    public void UpdateText(TextMeshProUGUI textMesh, string value){
        textMesh.text = value;
    }
}
