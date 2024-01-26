using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TextEffects : MonoBehaviour
{
    public TMP_Text textComponent;
    public bool rainbowText;
    public bool wobblyText;
    public bool shiftyText;

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
            for (int j = 0; j < 4; j++) {
                var origin = vertices[charInfo.vertexIndex + j];
                vertices[charInfo.vertexIndex + j] = origin + new Vector3(0, Mathf.Sin(Time.time * 5f + origin.x*0.01f)*10f,0);

            }
        }
        for (int i = 0; i < textInfo.meshInfo.Length; i++) {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}
