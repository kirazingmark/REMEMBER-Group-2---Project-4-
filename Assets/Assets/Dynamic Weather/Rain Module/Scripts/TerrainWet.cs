using UnityEngine;
using System.Collections;
using System;

namespace DynamicWeather {
    public class TerrainWet : MonoBehaviour {

        Terrain thisTerrain;    //terrain reference
        SplatPrototype[] prototypes; //array of terrain textures
        Coroutine lastUsedRoutine;    //variable to stop the most recent coroutine

        bool isRaining = false; //temp variable to prevent looping

        public float WetValue = 0.7f;   //value should be lower then 1 and higher then the dryvalue
        public float DryValue = 0.3f;   //value should be higher then 0 and lower then the WetValue

        public float timeTillWet = 3f; //the time it takes untill the object is wet
        public float timeTillDry = 6f; //the time it takes untill the object is dry
        public float terrainSmoothRate = 0.2f; //a lower value means the terrain transitions smoother from wet to dry, this also means more performance
        public bool stayWet = false;    //if you want the terrain to stay wet

        // Use this for initialization
        void Start() {
            if (WeatherManager.singleton != null) {
                thisTerrain = GetComponent<Terrain>();
                prototypes = thisTerrain.terrainData.splatPrototypes;

                if (terrainSmoothRate == 0) {
                    Debug.Log(gameObject.name + "'s Smoothrate can't be 0");
                }


                isRaining = WeatherManager.singleton.isRaining;
                ChangeState(isRaining);
            }
            else {
                Debug.Log("Drag the weatherManager prefab in the scene");
            }
        }

        // Update is called once per frame
        void Update() {
            if (WeatherManager.singleton != null) {
                if (isRaining != WeatherManager.singleton.isRaining) {
                    isRaining = WeatherManager.singleton.isRaining;

                    ChangeTerrainSmoothness(isRaining);
                }
            }
        }

        //get all the terrain materials and lerp the gloss to wetvalue/dry value and update the terrain
        //maybe include gloss texture when dry and remove it etc, doable?
        void ChangeTerrainSmoothness(bool isRaining) {

            if (isRaining) {
                //if time is 0 swap instantly
                if (timeTillWet == 0) {
                    ChangeState(isRaining);
                }
                else {
                    if (lastUsedRoutine != null) {
                        StopCoroutine(lastUsedRoutine);
                    }

                    lastUsedRoutine = StartCoroutine(UpdateTerrain(timeTillWet, false));
                }
            }
            else {
                if (!stayWet) {
                    //if time is 0 swap instantly
                    if (timeTillDry == 0) {
                        ChangeState(isRaining);
                    }
                    else {
                        if (lastUsedRoutine != null) {
                            StopCoroutine(lastUsedRoutine);
                        }

                        lastUsedRoutine = StartCoroutine(UpdateTerrain(timeTillDry, true));
                    }
                }
            }
        }

        //updates the splatarray every second for x seconds
        //for a smoother transition lower the smoothing value, but this will slow down framerate
        IEnumerator UpdateTerrain(float duration, bool negative) {
            float time = duration / terrainSmoothRate;

            float increment = (WetValue - DryValue) / time;

            for (int i = 0; i < time; i++) {
                foreach (var splat in prototypes) {
                    float gloss = splat.smoothness;

                    if (negative) {
                        if (gloss >= DryValue) {
                            splat.smoothness = gloss - increment;
                        }
                    }
                    else {
                        if (gloss <= WetValue) {
                            splat.smoothness = gloss + increment;
                        }
                    }
                }

                yield return new WaitForSeconds(terrainSmoothRate);
                thisTerrain.terrainData.splatPrototypes = prototypes;
            }
        }

        //enables the correct state instantly
        void ChangeState(bool isRaining) {
            if (isRaining) {
                foreach (var splat in prototypes) {
                    splat.smoothness = WetValue;
                }

                thisTerrain.terrainData.splatPrototypes = prototypes;
            }
            else {
                foreach (var splat in prototypes) {
                    splat.smoothness = DryValue;
                }

                thisTerrain.terrainData.splatPrototypes = prototypes;
            }
        }
    }
}