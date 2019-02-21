using System;
using System.Collections.Generic;

public class Solution
{
    public static void Main(string[] args)
    {
        var line = Console.ReadLine();
        int T = int.Parse(line);
        for (int i = 1; i <= T; i++)
        {
            var splitParts = Console.ReadLine().Split(' ');
            int Hdinit = int.Parse(splitParts[0]);
            int Ad = int.Parse(splitParts[1]);
            int Hkinit = int.Parse(splitParts[2]);
            int Ak = int.Parse(splitParts[3]);
            int B = int.Parse(splitParts[4]);
            int D = int.Parse(splitParts[5]);

            int result = Solve(Hdinit, Ad, Hkinit, Ak, B, D);

            string prefixString = "Case #" + i + ": ";

            if (result == -1)
                Console.WriteLine(prefixString + "IMPOSSIBLE");
            else
                Console.WriteLine(prefixString + result);


        }
    }

    private static int Solve(int healthDragonInit, int attackDragon, int healtKnightInit, int attackKnight, int attackIncrease, int attackDecrease)
    {
        bool win;
        int result = SolveRecursiv(healthDragonInit, healtKnightInit,  healthDragonInit,  attackDragon,  healtKnightInit,  attackKnight,  attackIncrease,  attackDecrease,0, out win);

        return -1;
    }

    private static int SolveRecursiv(int healthDragon, int healthKnight, int healthDragonInit, int attackDragon, int healtKnightInit, int attackKnight, int attackIncrease, int attackDecrease, int attackCount, out bool win)
    {
        if (healthDragon == 0)
        {
            win = false;
            return attackCount;
        }

        if ( healthKnight == 0 )
        {
            win = true;
            return attackCount;
        }

        win = false;

        int attackCountBest = int.MaxValue;

        attackCount++;

        // Attack Action 
        bool currentWin;
        var newHealthKight = healthKnight - attackDragon;
        int attackCountCurrent   = SolveRecursiv(newHealthKight == 0 ? healthDragon : healthDragon -attackKnight, 
            newHealthKight, healthDragonInit, attackDragon, healtKnightInit,
            attackKnight, attackIncrease, attackDecrease, attackCount, out currentWin);

        if (currentWin)
        {
            if (attackCountBest > attackCountCurrent)
            {
                attackCountBest = attackCountCurrent;
                win = true;
            }
        }

        // Buff
        attackCountCurrent = SolveRecursiv(healthDragon-attackKnight, healthKnight, healthDragonInit, attackDragon + attackIncrease, healtKnightInit, attackKnight, attackIncrease, attackDecrease, attackCount, out currentWin);

        if (currentWin)
        {
            if (attackCountBest > attackCountCurrent)
            {
                attackCountBest = attackCountCurrent;
                win = true;
            }
        }

        // Cure
        attackCountCurrent = SolveRecursiv(healthDragonInit  - attackKnight, healthKnight, healthDragonInit, attackDragon , healtKnightInit, attackKnight, attackIncrease, attackDecrease, attackCount, out currentWin);

        if (currentWin)
        {
            if (attackCountBest > attackCountCurrent)
            {
                attackCountBest = attackCountCurrent;
                win = true;
            }
        }

        // Debuff
        attackKnight -= attackDecrease;
        attackCountCurrent = SolveRecursiv(healthDragonInit - attackKnight, healthKnight, healthDragonInit, attackDragon, healtKnightInit, attackKnight, attackIncrease, attackDecrease, attackCount, out currentWin);

        if (currentWin)
        {
            if (attackCountBest > attackCountCurrent)
            {
                attackCountBest = attackCountCurrent;
                win = true;
            }
        }

        return win ? attackCountBest : -1;
    }
}
