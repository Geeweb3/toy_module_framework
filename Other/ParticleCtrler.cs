using UnityEngine;

namespace GameMain
{
    public class ParticleCtrler : MonoBehaviour
    {
        [SerializeField] private ParticleSystem ptc;

        private ParticleSystem.EmissionModule emission;
        private ParticleSystem.MainModule ptcMain;

        private void Start()
        {
            emission = ptc.emission;
            ptcMain = ptc.main;
            emission.rateOverTime = 0;
        }

        public void OnValueChange(float v)
        {

            if (v > 0.7f)
            {
                emission.rateOverTime = 0;
            }
            else if (v < 0.35f)
            {
                emission.rateOverTime = (int) (v * 1000);
                ptcMain.startSize = 0.07f * v;
            }
            else
            {
                emission.rateOverTime = (int) ((0.7f - v) * 2000);
                ptcMain.startSize = 0.07f * v;
            }
        }
    }
}