using System.Globalization;
using System.Data;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class DialogDataLoader : MonoBehaviour {
    
    private static string CharacterDataSetPath = "Assets/Datas/CharacterData.asset";
    private static string DialogDataSetPath = "Assets/Datas/DialogData.asset";
    private static string SelectionDataSetPath = "Assets/Datas/SelectionData.asset";

    [MenuItem("Data/Reload")]
    public static void ReloadData() {
        DataSet data = ExcelTools.LoadExcel("Assets/Editors/Data/GameData.xlsx");

        for (int i = 0; i < data.Tables.Count; ++i) {
            DataTable table = data.Tables[i];
            for (int j = 0; j < table.Columns.Count; ++j) {
                if (table.Rows[0][j].ToString() == "")
                    break;
                table.Columns[j].ColumnName = table.Rows[0][j].ToString();
            }
        }

        _loadCharacterData(data.Tables["character"]);
        _loadDialogData(data.Tables["dialog"]);
        _loadSelectionData(data.Tables["selection"]);
    }

    private static void _loadSelectionData(DataTable selectionTable) {
        Data.SelectionDataSet selectionDataSet = AssetDatabase.LoadAssetAtPath<Data.SelectionDataSet>(SelectionDataSetPath);
        selectionDataSet.selections.Clear();
        for (int i = 1; i < selectionTable.Rows.Count; ++i) {
            if (selectionTable.Rows[i]["id"].ToString() == "")
                break;
            Data.SelectionData temp = new Data.SelectionData() {
                ID = int.Parse(selectionTable.Rows[i]["id"].ToString()),
                DialogID = int.Parse(selectionTable.Rows[i]["dialogid"].ToString()),
            };
            var selections = selectionTable.Rows[i]["selection"].ToString().Split(';');
            foreach (var j in selections)
                temp.Options.Add(new System.Tuple<string, System.Action>(j, null));
        }
    }

    private static void _loadDialogData(DataTable dialogTable) {
        Data.DialogDataSet dialogDataSet = AssetDatabase.LoadAssetAtPath<Data.DialogDataSet>(DialogDataSetPath);
        dialogDataSet.dialogs.Clear();
        for (int i = 1; i < dialogTable.Rows.Count; ++i) {
            if (dialogTable.Rows[i]["id"].ToString() == "")
                break;
            Data.DialogData temp = new Data.DialogData() {
                ID = int.Parse(dialogTable.Rows[i]["id"].ToString()),
                PartID = int.Parse(dialogTable.Rows[i]["partid"].ToString()),
                SpeakerID = int.Parse(dialogTable.Rows[i]["speaker"].ToString()),
                Content = dialogTable.Rows[i]["content"].ToString(),
            };
            string audioPath = dialogTable.Rows[i]["audio"].ToString();
            if (audioPath != "") {
                temp.Audio = AssetDatabase.LoadAssetAtPath<AudioClip>(audioPath);
            }
            dialogDataSet.dialogs.Add(temp);
        }
    }

    private static void _loadCharacterData(DataTable characterTable) {
        Data.CharacterDataSet characterDataSet = AssetDatabase.LoadAssetAtPath<Data.CharacterDataSet>(CharacterDataSetPath);
        characterDataSet.Characters.Clear();
        for (int i = 1; i < characterTable.Rows.Count; ++i) {
            if (characterTable.Rows[i]["id"].ToString() == "")
                break;
            Data.CharacterData temp = new Data.CharacterData() {
                ID = int.Parse(characterTable.Rows[i]["id"].ToString()),
                Name = characterTable.Rows[i]["name"].ToString(),
            };
            string imgPath = characterTable.Rows[i]["drawing"].ToString();
            if (imgPath != "") {
                temp.Image = AssetDatabase.LoadAssetAtPath<Sprite>(imgPath);
            }
            characterDataSet.Characters.Add(temp);
        }
    }
}
