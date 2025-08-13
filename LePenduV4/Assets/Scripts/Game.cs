using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Game
{
    public List<string> usedLetters = new List<string>();
    public string word;
    public int remainingLife;
    public Game(string word, int lifeCount)
    {
        this.word = word;
        word = word.ToUpper();

        remainingLife = lifeCount;
    }

    public bool HasLetterPlayed(string letter)
    {
        return usedLetters.Contains(letter);
    }

    public bool WordContainsLetter(string letter)
    {
        return word.Contains(letter);
    }
    public void RemoveLife()
    {
        remainingLife--;
        if (remainingLife < 0)

            remainingLife = 0;
    }
    public bool IsWordGuess()
    {
        foreach (char value in word)
        {
            if (!usedLetters.Contains(value.ToString()))
            {
                return false;
            }
        }
        return true;
    }

}
