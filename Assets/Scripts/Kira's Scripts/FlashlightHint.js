// VARIABLES AND CONSTANTS.
private var enter : boolean;

// Start Function.
function Start() {
    
}

//Main Function.
function Update() {
    
}

function OnGUI() {
    
    if(enter){
        GUI.Label(new Rect(Screen.width/2 - 75, Screen.height - 200, 500, 500), "<color=white><size=25><i>Hint: Press 'F' to use Flashlight</i></size></color>");
    }
}

//Activate the Main function when player is near the Door.
function OnTriggerEnter (other : Collider) {
    
    if (other.gameObject.tag == "Player") {
        enter = true;
    }
}

    //Deactivate the Main function when player is go away from Door.
    function OnTriggerExit (other : Collider) {
        
        if (other.gameObject.tag == "Player") {
            enter = false;
        }
    }