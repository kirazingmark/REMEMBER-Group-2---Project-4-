using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace DynamicWeather {
    public class MeshWet : MonoBehaviour {

        Coroutine lastUsedRoutine;    //variable to stop the most recent coroutine

        Material[] matsOriginal;    //list of the original materials

        bool isRaining = false; //temp variable to compare to singleton
        public float SmoothRate = 0.2f; //the rate at which the material goes from dry to wet, lower means smoother but costs more performance

        public float WetValue = 0.8f;   //the max wet value of the mesh, should be lower then 1 and higher then the dryvalue
        public float DryValue = 0.3f;   //the min wet value of the mesh, should be lower then WetValue

        public float timeTillWet = 3f; //the time it takes untill the object is wet
        public float timeTillDry = 6f; //the time it takes untill the object is dry
        public bool StayWet = false;    //don't change back to the dry values
        public bool HasRainDetail = false;  //if the material has the rain detail normal map, lerp it

        Dictionary<int, Texture> specGlossDict; //dictionary to save specgloss texture with the index of the original material array

        // Use this for initialization
        void Start() {
            if (WeatherManager.singleton != null) {
                if (GetComponent<Renderer>()) {
                    matsOriginal = GetComponent<Renderer>().materials;
                    SaveTextures();
                    isRaining = WeatherManager.singleton.isRaining;
                    ChangeState(isRaining);
                }
                else {
                    Debug.Log("The MeshWet script on " + gameObject.name + " needs to be assigned to a object with a meshRenderer");
                    GetComponent<MeshWet>().enabled = false;
                }

                if (SmoothRate == 0) {
                    Debug.Log(gameObject.name + "'s Smoothrate can't be 0");
                    GetComponent<MeshWet>().enabled = false;
                }
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

                    ChangeMaterialSmoothness(isRaining);
                }
            }
        }

        //if it's raining, adjust the material's gloss and detail normal to wetvalue
        //if it's not raining, reset reset everything to dry value/original specGloss
        void ChangeMaterialSmoothness(bool isRaining) {
            if (isRaining) {
                if (timeTillWet == 0) {
                    ChangeState(isRaining);
                }
                else {
                    if (lastUsedRoutine != null) {
                        StopCoroutine(lastUsedRoutine);
                    }
                    lastUsedRoutine = StartCoroutine(LerpMaterials(WetValue, 1, timeTillWet, false));
                }
            }
            else {
                if (!StayWet) {
                    if (timeTillDry == 0) {
                        ChangeState(isRaining);
                    }
                    else {
                        if (lastUsedRoutine != null) {
                            StopCoroutine(lastUsedRoutine);
                        }

                        lastUsedRoutine = StartCoroutine(LerpMaterials(DryValue, 0, timeTillDry, true));
                    }
                }
            }
        }

        //saves to original spec gloss textures in a dictionary to use them later
        void SaveTextures() {
            specGlossDict = new Dictionary<int, Texture>();

            foreach (var mat in matsOriginal) {
                if (mat.HasProperty("_MetallicGlossMap") && mat.GetTexture("_MetallicGlossMap") != null) {
                    int index = Array.IndexOf(matsOriginal, mat);
                    specGlossDict.Add(index, mat.GetTexture("_MetallicGlossMap"));
                }

                if (mat.HasProperty("_SpecGlossMap") && mat.GetTexture("_SpecGlossMap") != null) {
                    int index = Array.IndexOf(matsOriginal, mat);
                    specGlossDict.Add(index, mat.GetTexture("_SpecGlossMap"));
                }
            }
        }

        //instantly change materials
        void ChangeState(bool isRaining) {
            if (isRaining) {
                SetMaterials(WetValue, 1, false);
            }
            else {
                SetMaterials(DryValue, 0, true);
            }
        }

        //lerp the texture's gloss/detail normal from 0 to 1 (not 255) over x seconds
        IEnumerator LerpMaterials(float glossValue, float normalValue, float duration, bool ResetMats) {
            foreach (var mat in matsOriginal) {
                if (mat.HasProperty("_MetallicGlossMap")) {
                    mat.SetTexture("_MetallicGlossMap", null);
                    mat.DisableKeyword("_MetallicGlossMap");
                }

                if (mat.HasProperty("_SpecGlossMap")) {
                    mat.SetTexture("_SpecGlossMap", null);
                    mat.DisableKeyword("_SPECGLOSSMAP");
                }

                if (mat.HasProperty("_Glossiness")) {
                    float time = duration / SmoothRate;
                    float increment = 1 / time;
                    float gloss = mat.GetFloat("_Glossiness");

                    for (float t = 0.0f; t <= 1.0f; t += increment) {
                        if (mat.HasProperty("_DetailNormalMap") && HasRainDetail) {
                            float detailNRM = mat.GetFloat("_DetailNormalMapScale");
                            mat.SetFloat("_DetailNormalMapScale", Mathf.Lerp(detailNRM, normalValue, t));
                        }
                        mat.SetFloat("_Glossiness", Mathf.Lerp(gloss, glossValue, t));
                        yield return new WaitForSeconds(SmoothRate);
                    }
                }
            }

            if (ResetMats) {
                ResetSpecGlossMaps();
            }


            yield return null;
        }

        //resets the specgloss textures if there was one filled in originally
        void ResetSpecGlossMaps() {
            foreach (KeyValuePair<int, Texture> texture in specGlossDict) {
                if (matsOriginal[texture.Key].HasProperty("_MetallicGlossMap")) {
                    matsOriginal[texture.Key].EnableKeyword("_MetallicGlossMap");
                    matsOriginal[texture.Key].SetTexture("_MetallicGlossMap", specGlossDict[texture.Key]);
                }

                if (matsOriginal[texture.Key].HasProperty("_SpecGlossMap")) {
                    matsOriginal[texture.Key].EnableKeyword("_SPECGLOSSMAP");
                    matsOriginal[texture.Key].SetTexture("_SpecGlossMap", specGlossDict[texture.Key]);
                }
            }
        }



        //sets the materials values and specgloss maps
        void SetMaterials(float glossValue, float normalValue, bool ResetMats) {
            foreach (var mat in matsOriginal) {
                if (mat.HasProperty("_MetallicGlossMap")) {
                    mat.SetTexture("_MetallicGlossMap", null);
                    mat.DisableKeyword("_MetallicGlossMap");
                }

                if (mat.HasProperty("_SpecGlossMap")) {
                    mat.SetTexture("_SpecGlossMap", null);
                    mat.DisableKeyword("_SPECGLOSSMAP");
                }

                if (mat.HasProperty("_Glossiness")) {
                    if (mat.HasProperty("_DetailNormalMap") && HasRainDetail) {
                        mat.SetFloat("_DetailNormalMapScale", normalValue);
                    }
                    mat.SetFloat("_Glossiness", glossValue);

                }
            }

            if (ResetMats) {
                ResetSpecGlossMaps();
            }
        }
    }
}