using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Snowy.MultiplayerBasics
{
    public class LeaderboardItem : MonoBehaviour
    {
        public TMP_Text posTxt;
        public TMP_Text nameTxt;
        public TMP_Text timeTakenTxt;

        public void Init(int pos, string playerName, string timeTaken)
        {
            this.posTxt.text = pos.ToString();
            this.nameTxt.text = playerName;
            this.timeTakenTxt.text = timeTaken;
        }
    }

}

