using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DigitalOpus.MB;
using UnityEditor;
using DigitalOpus.MB.Core;
#if UNITY_EDITOR
using DigitalOpus.MB.MBEditor;

namespace XREngine
{
    [System.Serializable]
    public class MeshBakeResult
    {
        public GameObject[] originals;
        public GameObject[] combined;
    }

    public class MeshBake : MonoBehaviour
    {

        public enum Mode
        {
            BAKE_CHILDREN
        }

        public Mode mode;

        public MeshBakeResult Bake()
        {
            var targets = new List<GameObject>();

            switch (mode)
            {
                case Mode.BAKE_CHILDREN:
                    var frontier = new Queue<Transform>();
                    frontier.Enqueue(transform);
                    while(frontier.Count > 0)
                    {
                        var child = frontier.Dequeue();
                        var rend = child.GetComponent<MeshRenderer>(); 
                        if (rend != null)
                        {
                            targets.Add(child.gameObject);
                        }
                        var nuChildren = (Enumerable.Range(0, child.childCount).Select((i) =>
                        {
                            return child.GetChild(i);
                        }));
                        foreach(var nuChild in nuChildren)
                        {
                            frontier.Enqueue(nuChild);
                        }
                    }
                    break;
            }

            if(targets.Count > 0)
            {
                GameObject go = new GameObject("MeshBake-" + gameObject.name);
                
                GameObject goChild = new GameObject("child", new[]
                {
                    typeof(MeshRenderer),
                    typeof(MeshFilter)
                });

                goChild.transform.SetParent(go.transform);

                var renderer = goChild.GetComponent<MeshRenderer>();
                var filter = goChild.GetComponent<MeshFilter>();

                var texBaker = go.AddComponent<MB3_TextureBaker>();
                var meshBaker = go.AddComponent<MB3_MeshBaker>();
                texBaker.fixOutOfBoundsUVs = true;
                texBaker.objsToMesh = targets;
                
                meshBaker.objsToMesh = targets;
                meshBaker.meshCombiner.lightmapOption = MB2_LightmapOptions.preserve_current_lightmapping;
                string matPath = PipelineSettings.PipelineAssetsFolder.Replace(Application.dataPath, "Assets") + gameObject.name + "_MeshBaker.asset";

                MB3_TextureBakerEditorInternal.CreateCombinedMaterialAssets(texBaker, matPath);
                texBaker.CreateAtlases(null, true, new MB3_EditorMethods());
                EditorUtility.ClearProgressBar();
                if (texBaker.textureBakeResults != null) EditorUtility.SetDirty(texBaker.textureBakeResults);

                meshBaker.textureBakeResults = AssetDatabase.LoadAssetAtPath<MB2_TextureBakeResults>(matPath);
                meshBaker.meshCombiner.resultSceneObject = go;
                
                MB3_MeshBakerEditorInternal.bake(meshBaker);
                goChild.isStatic = true;

                return new MeshBakeResult
                {
                    originals = targets.ToArray(),
                    combined = new[] { go }
                };
            }

            return null;
        }
    }
}
#endif

