using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeTeam : BaseMode
{
    public Transform spawnPoint;
    private Transform spawn1;
    private Transform spawn2;
    private Transform spawn3;
    private Transform spawn4;

    private void Awake()
    {
        spawn1 = spawnPoint;
        spawn2.position = new Vector3(spawn1.position.x + 180, spawn1.position.y, spawn1.position.z);
        spawn3.position = new Vector3(spawn2.position.x, spawn2.position.y, spawn2.position.z + 180);
        spawn4.position = new Vector3(spawn3.position.x, spawn3.position.y, spawn1.position.z);
    }
    public override void BeginGame()
    {
        base.BeginGame();
    }
    public override void EndGame()
    {
        base.EndGame();
    }

    public virtual void CreateTeam()
    {

    }
}
