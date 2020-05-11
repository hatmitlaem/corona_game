using Superpow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Rank : MonoBehaviour
{
    public List<Text> scoreTextList;

    private void OnEnable()
    {
        scoreTextList[0].text = Utils.GetBestScore().ToString();
        scoreTextList[1].text = Utils.Get2ndScore().ToString();
        scoreTextList[2].text = Utils.Get3rdScore().ToString();
        scoreTextList[3].text = Utils.Get4thScore().ToString();
        scoreTextList[4].text = Utils.Get5thScore().ToString();
    }
}
