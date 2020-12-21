using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mystat {
    public int gameCount;
    public int lossCount;
    public int winCount;
    public int grecjaCount;
    public int slavCount;
    public int nordCount;
    public int egipCount;
    public int grecjaEnemyCount;
    public int slavEnemyCount;
    public int nordEnemyCount;
    public int egipEnemyCount;

    public Mystat(int gameCount, int lossCount, int winCount, int grecjaCount, int slavCount, int nordCount, int egipCount, int grecjaEnemyCount, int slavEnemyCount, int nordEnemyCount, int egipEnemyCount)
    {
        this.gameCount = gameCount;
        this.lossCount = lossCount;
        this.winCount = winCount;
        this.grecjaCount = grecjaCount;
        this.slavCount = slavCount;
        this.nordCount = nordCount;
        this.egipCount = egipCount;
        this.grecjaEnemyCount = grecjaEnemyCount;
        this.slavEnemyCount = slavEnemyCount;
        this.nordEnemyCount = nordEnemyCount;
        this.egipEnemyCount = egipEnemyCount;
    }
}
   
