using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TextEffects : MonoBehaviour
{
    public TMP_Text textComponent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textComponent.ForceMeshUpdate();
        var textInfo = textComponent.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++) {
            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible) {
                continue;
            }
            var vertices = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;
            
        }
    }
}
