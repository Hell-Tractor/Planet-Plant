using UnityEngine;
namespace Utils {
public interface ISaveLoad {
    public void Save(ScriptableObject originData);
    public void Load(ScriptableObject originData);
}
}