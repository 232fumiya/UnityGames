using UnityEngine;
using System.Collections;

public class objPos :Photon.MonoBehaviour{
	Vector3 SyncVec;
	// Use this for initialization
	void Start () {
	
	}
	// Update is called once per frame
	void Update () {
		if (!photonView.isMine) {       // photonViewが自分自身ではない場合、位置と回転を反映.
			transform.position =Vector3.Slerp (this.transform.position,SyncVec,Time.deltaTime * 5);
		}
	}

	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			// 自分のプレイヤー情報を送信.
			stream.SendNext(transform.position);
		} else {
			// 他のプレイヤー情報を受信.
			SyncVec = (Vector3)stream.ReceiveNext();
		}
	}
}
