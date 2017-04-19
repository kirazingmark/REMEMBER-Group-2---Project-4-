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
        GUI.Label(new Rect(Screen.width/3.25 - 75, Screen.height - 100, 1500, 500), "<color=white><size=45><i>That's a long way down! Better watch my step.</i></size></color>");
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