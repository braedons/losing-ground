using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour {
    public TextMeshProUGUI meltText;
    public TextMeshProUGUI freezeText;

    void Start() {
        
    }

    void Update() {
        
    }

    public void UpdateTurnsLeft(int meltTurns, int freezeTurns) {
        meltText.SetText("<b>Melt: </b> " + meltTurns);
        freezeText.SetText("<b>Freeze: </b> " + freezeTurns);
    }
}
