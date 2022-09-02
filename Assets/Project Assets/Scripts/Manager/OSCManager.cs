using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCManager : MonoBehaviour
{
    [SerializeField] OSC QLabOSC;
    [SerializeField] OSC TouchDesignerOSC;

    public static OSCManager instance;

    private void Start(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(this.gameObject);
        }
    } 

    public void StartFx(int index){
        OscMessage message = new OscMessage();
        message.address = "/cue/fx"+index+"/start";
        QLabOSC.Send(message);
    }

    public void StartSfx(int index){
        OscMessage message = new OscMessage();
        message.address = "/cue/start_sfx"+index+"/start";
        QLabOSC.Send(message);
    }

    public void StopSfx(int index){
        OscMessage message = new OscMessage();
        message.address = "/cue/stop_sfx"+index+"/start";
        QLabOSC.Send(message);
    }

    public void SendPosition(Vector3 position){
        OscMessage message = new OscMessage();
        message.address = "/phone/position";
        message.values.Add(position.x);
        message.values.Add(position.y);
        message.values.Add(position.z);
        TouchDesignerOSC.Send(message);
    }
}
