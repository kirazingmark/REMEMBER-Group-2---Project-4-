
// VARIABLES AND CONSTANTS.
private var enter : boolean;
private var played : float = 0;
public var customFont : Font;

// Start Function.
function Start() {
    
}

//Main Function.
function Update() {
    
}

function OnGUI() {
    
    if(enter){
		GUI.skin.font = customFont;
        GUI.Label(new Rect(Screen.width/5 - 0, Screen.height - 100, 1500, 500), "<color=white><size=45><i>EVEN AFTER THE FIRE, DOES IT SEE CLEARLY OR DARKLY?</i></size></color>");

    }
}

//Activate the Main function when player is near the Door.
function OnTriggerEnter (other : Collider) {
    
    if (other.gameObject.tag == "Player") {
		
		if (played == 0){
			
			enter = true;
			yield WaitForSeconds(5);
			enter = false;
			played = 1;
		}
		else {
			played = 1;
		}
    }
}

    //Deactivate the Main function when player is go away from Door.
    function OnTriggerExit (other : Collider) {
        
        // if (other.gameObject.tag == "Player") {
            // enter = false;
        // }
    }