
// VARIABLES AND CONSTANTS.
var smooth = 2.0;
var DoorOpenAngle = 90.0;
private var open : boolean;
private var enter : boolean;
private var defaultRot : Vector3;
private var openRot : Vector3;

// Start Function.
function Start() {
    
    defaultRot = transform.eulerAngles;
    openRot = new Vector3 (defaultRot.x, defaultRot.y - DoorOpenAngle, defaultRot.z);
}

//Main Function.
function Update() {
    
    if(open) {
        
        // Open Door.
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, openRot, Time.deltaTime * smooth);
    }
    else {
        
        // Close Door.
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, defaultRot, Time.deltaTime * smooth);
    }

    if(Input.GetKeyDown("e") && enter) {
        
        open = !open;
    }
}

function OnGUI() {
    
    if(enter){
        
        GUI.Label(new Rect(Screen.width/2 - 75, Screen.height - 200, 350, 80), "<color=white><size=35>Open/Close - 'E'</size></color>");
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