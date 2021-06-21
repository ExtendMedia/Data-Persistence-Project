using System;

public class Player : IComparable<Player>
{
    public string name;
    public int score;

    public Player(string playerName, int playerScore)
    {
        name = playerName;
        score = playerScore;
    }

    public int CompareTo(Player other)
    {
        if (other == null)
            return 1;

        else
            return this.score.CompareTo(other.score);
    }
}
