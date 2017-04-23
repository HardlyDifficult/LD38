using UnityEngine;

public class Team
{
    public int Id;
    public string TeamName;

    public Player[] TeamPlayers;

    public int TeamSize = 0;

    public float TeamHealth
    {
        get
        {
            float health = 0;
            for (int i = 0; i < TeamPlayers.Length; i++)
            {
                if (TeamPlayers[i] != null)
                {
                    health += TeamPlayers[i].Health;  
                }
            }

            float teamMaxHealth = TeamSize * 100.0f;

            return Mathf.Clamp01(health / teamMaxHealth);

        }
    }

    public bool TeamAlive
    {
        get { return TeamHealth > 0.0f; }
    }

    public Player CurrentPlayer
    {
        get { return TeamPlayers[_currentPlayerIndex]; }
    }

    private int _currentPlayerIndex = 0;
    private int _currentTeamSize = 0;

    public Team(int id, int teamSize, string teamName)
    {
        Id = id;
        TeamName = teamName;
        TeamSize = teamSize;

        TeamPlayers = new Player[TeamSize];

        TurnController.onTurnChange += OnTurnChange;
    }

    private void OnTurnChange()
    {
        if (TurnController.currentTeamId == Id)
        {
            CycleTeamMembers();
        }
    }

    private void CycleTeamMembers()
    {
        if (!TeamAlive) return;

        _currentPlayerIndex++;

        if (_currentPlayerIndex >= TeamPlayers.Length)
            _currentPlayerIndex = 0;

        if(TeamPlayers[_currentPlayerIndex] == null)
            CycleTeamMembers();

    }

    public void AddPlayer(Player player)
    {
        if (!ContainsPlayer(player))
        {
            if (_currentTeamSize == TeamSize) return;

            TeamPlayers[_currentTeamSize] = player;
            _currentTeamSize++;
        }
    }

    public void RemovePlayer(Player player)
    {
        if (ContainsPlayer(player))
        {
            for (int i = 0; i < TeamPlayers.Length; i++)
            {
                if (TeamPlayers[i] == player)
                {
                    TeamPlayers[i] = null;
                    _currentTeamSize--;
                }
            }
        }
    }

    public bool ContainsPlayer(Player p)
    {
        for (int i = 0; i < TeamPlayers.Length; i++)
        {
            if (TeamPlayers[i] == p)
                return true;
        }

        return false;
    }


    public bool GetTurn(Player player)
    {
        return CurrentPlayer == player;
    }


}
