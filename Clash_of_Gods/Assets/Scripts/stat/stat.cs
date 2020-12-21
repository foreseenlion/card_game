using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class stat : MonoBehaviour
{
    public GameObject text_gameCount;
    public GameObject text_winCount;
    public GameObject text_lossCount;
    public GameObject text_grecjaCount;
    public GameObject text_egipCount;
    public GameObject text_nordCount;
    public GameObject text_slavCount;
    public GameObject text_grecjaEnemyCount;
    public GameObject text_egipEnemyCount;
    public GameObject text_nordEnemyCount;
    public GameObject text_slavEnemyCount;

    private Mystat mystat;
    // Start is called before the first frame update
    void Start()
    {
        mystat = JsonUtility.FromJson<Mystat>(ReadStat());
        //text.GetComponent<Text>().text = createText();

        //Put data from "mystat" to text in Statistics_Menu Scene
        text_gameCount.GetComponent<Text>().text = mystat.gameCount.ToString();
        text_winCount.GetComponent<Text>().text = mystat.winCount.ToString();
        text_lossCount.GetComponent<Text>().text = mystat.lossCount.ToString();
        text_grecjaCount.GetComponent<Text>().text = mystat.grecjaCount.ToString();
        text_egipCount.GetComponent<Text>().text = mystat.egipCount.ToString();
        text_nordCount.GetComponent<Text>().text = mystat.nordCount.ToString();
        text_slavCount.GetComponent<Text>().text = mystat.slavCount.ToString();
        text_grecjaEnemyCount.GetComponent<Text>().text = mystat.grecjaEnemyCount.ToString();
        text_egipEnemyCount.GetComponent<Text>().text = mystat.egipEnemyCount.ToString();
        text_nordEnemyCount.GetComponent<Text>().text = mystat.nordEnemyCount.ToString();
        text_slavEnemyCount.GetComponent<Text>().text = mystat.slavEnemyCount.ToString();
    }



    public string createText()
    {
        string result = "";
        result += createLine("Game count", mystat.gameCount);
        result += createLine("Win count", mystat.winCount);
        result += createLine("Loss count", mystat.lossCount);
        result += createLine("Greece games", mystat.grecjaCount);
        result += createLine("Egypt games", mystat.egipCount);
        result += createLine("Nordic games", mystat.nordCount);
        result += createLine("Slavic games", mystat.slavCount);
        result += createLine("Games against Greece", mystat.grecjaEnemyCount);
        result += createLine("Games against Egypt", mystat.egipEnemyCount);
        result += createLine("Games against Nordic", mystat.nordEnemyCount);
        result += createLine("Games against Slavic", mystat.slavEnemyCount);

        return result;
    }

    string createLine(string what, int count)
    {
        return what + " " + count + "\n";
    }

    public static string ReadStat()
    {
        string result = "";
        string path = "Assets/Resources/stat.json";

        // Open the stream and read it back.
        using (FileStream fs = File.OpenRead(path))
        {
            byte[] b = new byte[1024];
            System.Text.UTF8Encoding temp = new System.Text.UTF8Encoding(true);

            while (fs.Read(b, 0, b.Length) > 0)
            {
                result += temp.GetString(b);
            }
        }
        return result;
    }

    public static void SaveStat(Mystat mystat)
    {
        Mystat result = JsonUtility.FromJson<Mystat>(ReadStat());

        string path = "Assets/Resources/stat.json";

        string json = JsonUtility.ToJson(sumStat(mystat,result));

        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(json);
            }
        }
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif

    }

    static Mystat sumStat(Mystat mystat, Mystat oldMyStat)
    {
        return new Mystat(
            mystat.gameCount + oldMyStat.gameCount,
            mystat.lossCount + oldMyStat.lossCount,
            mystat.winCount + oldMyStat.winCount,
            mystat.grecjaCount + oldMyStat.grecjaCount,
            mystat.slavCount + oldMyStat.slavCount,
            mystat.nordCount + oldMyStat.nordCount,
            mystat.egipCount + oldMyStat.egipCount,
            mystat.grecjaEnemyCount + oldMyStat.grecjaEnemyCount,
            mystat.slavEnemyCount + oldMyStat.slavEnemyCount,
            mystat.nordEnemyCount + oldMyStat.nordEnemyCount,
            mystat.egipEnemyCount + oldMyStat.egipEnemyCount
        );
    }

}
