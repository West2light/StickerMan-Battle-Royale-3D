using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameModeTeam : BaseMode
{
    public Transform spawnPoint;
    public List<Transform> spawnPoints;

    private readonly string[] teamTags = { "TeamA", "TeamB", "TeamC", "TeamD" };

    private void Awake()
    {
        spawnPoints.Add(spawnPoint);
        spawnPoints.Add(CreateSpawn(new Vector3(spawnPoint.position.x + 20, spawnPoint.position.y, spawnPoint.position.z)));
        spawnPoints.Add(CreateSpawn(new Vector3(spawnPoint.position.x + 20, spawnPoint.position.y, spawnPoint.position.z + 20)));
        spawnPoints.Add(CreateSpawn(new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z + 20)));
    }
    private Transform CreateSpawn(Vector3 position)
    {
        GameObject spawnObject = new GameObject("SpawnPoint");
        spawnObject.transform.position = position;
        return spawnObject.transform;
    }

    public override void BeginGame()
    {
        base.BeginGame();

        var spawnWithTeams = spawnPoints
            .Zip(teamTags, (spawn, teamTag) => new { Spawn = spawn, TeamTag = teamTag });

        foreach (var entry in spawnWithTeams)
        {
            CreateEnemy(entry.Spawn, entry.TeamTag);
        }
        AssginPlayer();
    }
    public override void EndGame()
    {
        base.EndGame();

    }

    public override void CreateEnemy(Transform spawn, string teamTag)
    {

        for (int i = 0; i < 2; i++)
        {
            Enemy enemy = Instantiate(gameController.enemy, spawn.position + new Vector3(spawn.position.x + i * 1f, spawn.position.y + i * 1f, spawn.position.z + i * 1f), Quaternion.identity);
            enemy.TeamTag = teamTag;
            enemy.gameObject.tag = teamTag;
            gameController.enemies.Add(enemy);
        }
    }
    private void AssginPlayer()
    {
        int playerTeamIndex = Random.Range(0, teamTags.Length);
        string playerTeamTag = teamTags[playerTeamIndex];
        gameController.currentPlayer.tag = playerTeamTag;
        Enemy enemy = new Enemy();
        for (int i = 0; i < gameController.enemies.Count; i++)
        {
            if (gameController.enemies[i].tag == gameController.currentPlayer.tag)
            {

                enemy = gameController.enemies[i];
                enemy.gameObject.SetActive(false);
                gameController.enemies.Remove(enemy);
                break;
            }
        }
        gameController.mode.playerSpawn = enemy.transform;

    }


}
