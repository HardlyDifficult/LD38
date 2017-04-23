
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject Planet;

    public void Awake()
    {
        for (int i = 0; i < TurnController.teamCount; i++)
        {
            string teamName = i%2 == 0 ? "Worm" : "Grub";

            Team team = new Team(i, 
                TurnController.playersPerTeam, 
                teamName + "s"
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
