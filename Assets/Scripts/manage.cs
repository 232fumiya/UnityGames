using UnityEngine;
using System.Collections;

public class manage : Photon.MonoBehaviour {
	private bool keyLock;
	private GameObject mySyncObj;
	// Use this for initialization
	void Start () {
		keyLock = false;
		PhotonNetwork.ConnectUsingSettings (null);
	}
	void OnJoinedLobby(){
		Debug.Log ("test");
		PhotonNetwork.JoinRandomRoom ();
	}
	void OnJoinedRoom(){
		Debug.Log ("success!!");
		keyLock = true;
		float x = Random.Range (-5.0f, 5.0f);
		float y = Random.Range (-5.0f, 5.0f);
		float z = Random.Range (0.0f, 5.0f);
		mySyncObj =	PhotonNetwork.Instantiate("Cube",new Vector3(x,y,z),Quaternion.identity,0);
		if (mySyncObj == null)
			return;
	}
	void OnPhotonRandomJoinFailed(){
		Debug.Log ("Room is Not Found");
		PhotonNetwork.CreateRoom("test");
	}
	void FixedUpdate(){
		if (mySyncObj != null) {
			Vector3 SyncVec = mySyncObj.transform.position;
			SyncVec.y += Input.GetAxis ("Vertical")/2;
			SyncVec.x += Input.GetAxis ("Horizontal")/2;
			mySyncObj.transform.position = SyncVec;
		}
	}

}
