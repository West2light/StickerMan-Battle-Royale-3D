using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeTeam : BaseMode
{
    public Transform spawnPoint;
    public List<Transform> spawnPoints;
    public PopupEndGame popupEndGame;
    public TMP_Text teamScoreText;
    /*public string attackingTeam;
    public string victimTeam;*/
    private readonly string[] teamTags = { "TeamA", "TeamB", "TeamC", "TeamD" };
    private Dictionary<string, int> teamScores = new Dictionary<string, int>();

    private void InitTeamScores()
    {
        teamScores["TeamA"] = 0;
        teamScores["TeamB"] = 0;
        teamScores["TeamC"] = 0;
        teamScores["TeamD"] = 0;

    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    private void Awake()
    {
        spawnPoints.Add(spawnPoint);
        spawnPoints.Add(CreateSpawn(new Vector3(spawnPoint.position.x + 10f, spawnPoint.position.y, spawnPoint.position.z)));
        spawnPoints.Add(CreateSpawn(new Vector3(spawnPoint.position.x + 10f, spawnPoint.position.y, spawnPoint.position.z + 10f)));
        spawnPoints.Add(CreateSpawn(new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z + 10f)));
        InitTeamScores();

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            SceneManager.LoadScene("GamePlay");
        }
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
        CalculateFinalScores();
    }
    public override void EndGame()
    {
        base.EndGame();
        popupEndGame.gameObject.SetActive(true);
    }

    public override void CreateEnemy(Transform spawn, string teamTag)
    {
        for (int i = 0; i < 2; i++)
        {
            Vector3 v;
            v.x = spawn.position.x + i * 1f;
            v.y = spawn.position.y + i * 1f;
            v.z = spawn.position.z + i * 1f;

            Vector3 point = spawn.position + v;

            Enemy enemy = Instantiate(gameController.enemy, point, Quaternion.identity);
            enemy.TeamTag = teamTag;
            enemy.gameObject.tag = teamTag;
            enemy.gameObject.name = string.Format("{0}-{1}", teamTag, i);
            gameController.enemies.Add(enemy);
        }
    }
    private void AssginPlayer()
    {
        int playerTeamIndex = Random.Range(0, teamTags.Length);

        string playerTeamTag = teamTags[playerTeamIndex];
        gameController.currentPlayer.tag = playerTeamTag;

        for (int i = 0; i < gameController.enemies.Count; i++)
        {
            Enemy enemy = gameController.enemies[i];
            if (enemy.tag == gameController.currentPlayer.tag)
            {
                // Debug.LogFormat("Playeyr move to {0}", enemy.gameObject.name);
                gameController.currentPlayer.transform.position = new Vector3(enemy.transform.position.x + Random.Range(-0.5f, 0.5f), 0f, enemy.transform.position.z);
                gameController.currentPlayer.transform.rotation = enemy.transform.rotation;
                gameController.currentPlayer.gameObject.name = string.Format("{0}-player", playerTeamTag);
                enemy.gameObject.SetActive(false);
                gameController.enemies.Remove(enemy);
                return;
            }
        }

    }

    public override void OnDeadCurrentPlayer(Player player)
    {
        base.OnDeadCurrentPlayer(player);
        if (teamTags.Length == 1)
        {
            for (int i = 0; i < gameController.enemies.Count; i++)
            {
                gameController.enemies[i].ChangeState(BehaviourState.Win);
            }

            EndGame();
        }
    }
    private void UpdateTeamScoresUI()
    {
        string scoreText = "";
        foreach (var team in teamScores)
        {
            if (team.Key == gameController.currentPlayer.tag)
            {
                scoreText += $"<color=blue>{team.Key}: {team.Value}</color>\n";
            }
            else
            {
                scoreText += $"{team.Key}: {team.Value}\n";
            }
        }

        teamScoreText.text = scoreText;

    }
    public void CalculateFinalScores()
    {
        foreach (var team in teamScores.Keys.ToList())
        {
            int kills = teamScores[team];
            float contributionFactor = 1.0f;
            teamScores[team] = Mathf.RoundToInt(kills * contributionFactor);
        }


        UpdateTeamScoresUI();
    }

    public override void OnDeadEnemy(Enemy enemy)
    {
        base.OnDeadEnemy(enemy);
        string attackingTeam = enemy.LastAttacker;
        if (attackingTeam != null && teamScores.ContainsKey(attackingTeam))
        {
            ++teamScores[attackingTeam];
        }
        gameController.enemies.Remove(enemy);
        UpdateTeamScoresUI();
    }

}
