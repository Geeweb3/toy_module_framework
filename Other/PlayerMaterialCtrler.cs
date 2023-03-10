using System;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;
using Logger = PFramework.Logger;

namespace GameMain
{
    public class PlayerMaterialCtrler : MonoBehaviour
    {
        private List<Material> materials;
        private int disolveId;
        [SerializeField]private ParticleCtrler pc;
        private bool isOpen;
        private float value;
        public float speed;

        private List<Color> mColMap;
        private static readonly int BodyBloomCol = Shader.PropertyToID("_Color");
        private static readonly int ParticleBloomCol = Shader.PropertyToID("_ColorBloom");

        public void OnInit()
        {
            materials = new List<Material>();
            mColMap = new List<Color>() {Color.blue, Color.green, Color.magenta, Color.yellow, Color.red, Color.white};
            var renderers = gameObject.GetComponentsInChildren<Renderer>();
            foreach (var r in renderers)
            {
                materials.AddRange(r.materials);
            }
            disolveId = Shader.PropertyToID("disolve");
            foreach (var mat in materials)
            {
                mat.SetFloat(disolveId, 0);
            }
            transform.Rotate(new Vector3(0, 345.539f, 0));
        }

        public void OnShow(int rank)
        {
            foreach (var mat in materials)
            {
                mat.SetFloat(disolveId, 0);
                // mat.SetColor(BloomCol, mColMap[rank]);
            }

            pc.GetComponent<Renderer>().material.SetColor(ParticleBloomCol, mColMap[rank]);
            value = 0;
        }

        public void OnFingerDown()
        {
            value = 0;
            isOpen = true;
        }

        private void FixedUpdate()
        {
            if (isOpen)
            {
                if (value < 1)
                {
                    value += speed;
                    for (int i = 0, len = materials.Count; i < len; ++i)
                    {
                        materials[i].SetFloat(disolveId, value);
                    }
                    pc.OnValueChange(value);
                }
                else
                    isOpen = false;
            }
        }
    }
}