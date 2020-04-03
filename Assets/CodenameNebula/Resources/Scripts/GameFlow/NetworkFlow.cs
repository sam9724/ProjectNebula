using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;



using BeardedManStudios.Forge.Networking.Generated;

public class NetworkFlow : NetworkFlowBehavior
{
    // Start is called before the first frame update
    private bool _networkReady;
    //private PlayersInfo playersInfo;                // Added DKE

    private readonly Dictionary<uint, PilotBehavior> _playerObjects = new Dictionary<uint, PilotBehavior>();
    private readonly Dictionary<uint, GunnerBehavior> _gunObjects = new Dictionary<uint, GunnerBehavior>();
    void Start()
    {
        
    }
    protected override void NetworkStart()
    {
        base.NetworkStart();

        if (NetworkManager.Instance.IsServer)
        {
            PilotBehavior c = NetworkManager.Instance.InstantiatePilot(0, new Vector3(0, 0.55f, 0));    // Changed DKE to include index and start position
            c.networkObject.ownerNetworkId = networkObject.MyPlayerId;
            _playerObjects.Add(networkObject.MyPlayerId, c); //p.NetworkId
            c.transform.Find("PilotCamera").gameObject.SetActive(true);
            //c.transform.Find("PilotCamera").gameObject.tag = "MainCamera";

            NetworkManager.Instance.Networker.playerDisconnected += (player, sender) =>
            {
                // Remove the player from the list of players and destroy it
                PilotBehavior cc = _playerObjects[player.NetworkId];
                _playerObjects.Remove(player.NetworkId);
                cc.networkObject.Destroy();
            };
        }
        else
        {
            GunnerBehavior r = NetworkManager.Instance.InstantiateGunner(0, PlayerManager.Instance.pilot.gunnerSpawnPos.position);    //, new Vector3(0, 0, 2) Changed DKE to include index and start position
            r.networkObject.ownerNetworkId = networkObject.MyPlayerId;
            _gunObjects.Add(networkObject.MyPlayerId, r);
            r.transform.Find("GunnerCamera").gameObject.SetActive(true);
            //r.transform.Find("GunnerCamera").gameObject.tag = "MainCamera";


            NetworkManager.Instance.Networker.playerDisconnected += (player, sender) =>
            {
                // Remove the player from the list of players and destroy it
                GunnerBehavior rr = _gunObjects[player.NetworkId];
                _gunObjects.Remove(player.NetworkId);
                rr.networkObject.Destroy();
            }; 
        }

        _networkReady = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!_networkReady && networkObject != null)
        {
            NetworkStart();
        }

    }
}
