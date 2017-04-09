using UnityEngine;
using System.Collections;

namespace DynamicWeather {

    public class WeatherManager : MonoBehaviour {

        public bool isRaining = false;

        public GameObject RainParticle; //drop your rain particle here

        public static WeatherManager singleton = null;

        bool rainActive = false;    //bool to compare so rain can change state

        void Awake() {
            //Check if instance already exists
            if (singleton == null) {
                //if not, set instance to this
                singleton = this;

                DontDestroyOnLoad(this.gameObject);
            }

            //If instance already exists and it's not this:
            else if (singleton != this) {
                //    //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);
            }
        }

        // Use this for initialization
        void Start() {
            if (RainParticle == null) {
                Debug.Log("Assign a rain particle to the weathermanager");
            }
            else {
                rainActive = isRaining;
                ActivateRain(rainActive);
            }
        }

        // Update is called once per frame
        void Update() {

            //Code the logic for you rain particle here by using the isRaining boolean

            //on keydown flips the boolean enables/disables the rain particle
            if (Input.GetKeyDown(KeyCode.R)) {
                Debug.Log("guicontrols: Input.GetKeyDown(KeyCode.R)");
                isRaining = !isRaining;
            }

            if (rainActive != isRaining) {
                rainActive = isRaining;
                ActivateRain(rainActive);
            }
        }

        //checks rain state and enables or disables the particle
        void ActivateRain(bool isRaining) {

            if (RainParticle != null) {
                RainParticle.SetActive(isRaining);
            }
        }
    }
}
