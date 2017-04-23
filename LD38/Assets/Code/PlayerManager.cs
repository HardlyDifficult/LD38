
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject Planet;

    public void Awake()
    {
        string[] teamNames =
        {
            "Grub",
            "Worm",
            "Caterpillar",
            "Snake",
            "Ladybug"
        };

        for (int i = 0; i < TurnController.teamCount; i++)
        {
            string teamName = teamNames[i];

            Team team = new Team(i, 
                TurnController.playersPerTeam, 
                "Team " + teamName + "s"
            );

            TurnController.AddTeam(team);

            for (int j = 0; j < TurnController.playersPerTeam; j++)
            {
                Player player = SpawnPlayer();
                player.PlayerName = teamName + " " + (j + 1);

                TurnController.AddPlayer(team, player);
            }
        }
    }

    public Player SpawnPlayer()
    {
        Vector3 randomSpawnPoint = RandomHelper.RandomPointOnObject(Planet);

        GameObject player = Instantiate(PlayerPrefab, randomSpawnPoint, Quaternion.identity);

        Player playerComponent = player.GetComponent<Player>();

        if (playerComponent != null)
            return playerComponent;

        Destroy(player);
        return null;
    }

}
