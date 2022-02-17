using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GLTF.Schema;
using Newtonsoft.Json.Linq;
using SeinJS;

namespace XREngine.RealityPack
{
    public class EXT_mesh_gpu_instancing : Extension
    {
        public JProperty Serialize()
        {
            return new JProperty
            (
                "EXT_mesh_gpu_instancing", new JObject
                (

                )
            );
        }
    }

}


