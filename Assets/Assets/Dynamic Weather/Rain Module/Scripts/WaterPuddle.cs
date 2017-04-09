using UnityEngine;
using System.Collections;

namespace DynamicWeather {
    public class WaterPuddle : MonoBehaviour {

        bool isRaining = false;

        Material mat;   //material assigned at start that needs to change
        Coroutine lastUsedRoutine;    //variable to stop the most recent coroutine

        public float timeTillWet = 6;   //the time it takes for the waterpuddle to fill
        public float timeTillDry = 6;   //the time it takes for the waterpuddle to dry up
        public float maxOpacity = 1;    //the maximum amount of opacity, 1 is fully opaque
        public bool StayWet = false;    //set true if you don't want the water puddle to dissappear

        // Use this for initialization
        void Start() {
            if (WeatherManager.singleton != null) {
                mat = GetComponent<Renderer>().material;

                isRaining = WeatherManager.singleton.isRaining;
                ActivatePuddles(isRaining);
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

                    CheckState();
                }
            }
        }

        //lerp the puddles material to the correct value according to the rain
        void CheckState() {
            if (isRaining) {
                if (timeTillWet == 0) {
                    //if time is 0 switch material instantly
                    ActivatePuddles(isRaining);
                }
                else {
                    if (lastUsedRoutine != null) {
                        StopCoroutine(lastUsedRoutine);
                    }

                    lastUsedRoutine = StartCoroutine(FadeTo(maxOpacity, timeTillWet));
                }
            }
            else {
                if (!StayWet) {
                    if (timeTillDry == 0) {
                        //if time is 0 switch material instantly
                        ActivatePuddles(isRaining);
                    }
                    else {
                        if (lastUsedRoutine != null) {
                            StopCoroutine(lastUsedRoutine);
                        }
                        lastUsedRoutine = StartCoroutine(FadeTo(0, timeTillDry));
                    }
                }
            }
        }

        //lerp the texture's opacity from 0 to 1 (not 255) over x seconds
        IEnumerator FadeTo(float aValue, float duration) {
            float alpha = mat.color.a;

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / duration) {
                Color newColor = mat.color;
                newColor.a = Mathf.Lerp(alpha, aValue, t);
                mat.color = newColor;
                yield return null;
            }
        }

        void ActivatePuddles(bool isRaining) {
            if (isRaining) {
                Color newColor = mat.color;
                newColor.a = maxOpacity;
                mat.color = newColor;
            }
            else {
                Color newColor = mat.color;
                newColor.a = 0;
                mat.color = newColor;
            }
        }
    }
}