using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SeinJS;
using GLTF.Schema;
using Newtonsoft.Json.Linq;
using System;

namespace XREngine.RealityPack
{
    public class EXT_mesh_gpu_instancing_Factory : SeinExtensionFactory
    {

        public override string GetExtensionName()
        {
            return "EXT_mesh_gpu_instancing";
        }

        public override List<System.Type> GetBindedComponents()
        {
            return new List<Type> { };
        }

        public override List<EExtensionType> GetExtensionTypes()
        {
            return new List<EExtensionType> { EExtensionType.Node };
        }

        public override void Serialize(ExporterEntry entry, Dictionary<string, Extension> extensions, UnityEngine.Object component = null, object options = null)
        {

            var extension = new EXT_mesh_gpu_instancing();


            AddExtension(extensions, extension);
        }
        public override Extension Deserialize(GLTFRoot root, JProperty extensionToken)
        {
            throw new System.NotImplementedException();
        }
    }

}
