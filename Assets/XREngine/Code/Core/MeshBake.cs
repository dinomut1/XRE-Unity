using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalOpus.MB;
#if UNITY_EDITOR
using DigitalOpus.MB.MBEditor;

namespace XREngine
{
    public class MeshBake : MonoBehaviour
    {
        MeshRenderer renderer;
        MeshFilter filter;
        MB3_TextureBaker texBaker;
        MB3_MeshBaker meshBaker;
        public void Bake()
        {
            renderer = GetComponent<MeshRenderer>();
            filter = GetComponent<MeshFilter>();

            texBaker = gameObject.AddComponent<MB3_TextureBaker>();
            meshBaker = gameObject.AddComponent<MB3_MeshBaker>();

            MB3_TextureBakerEditorInternal.CreateCombinedMaterialAssets(texBaker, P)
        }
    }
}
#endif

