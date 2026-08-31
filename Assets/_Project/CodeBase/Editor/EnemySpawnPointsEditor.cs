using System.Linq;
using _Project.CodeBase.Gameplay.Enemy;
using UnityEditor;
using UnityEngine;

namespace _Project.CodeBase.Editor
{
    [CustomEditor(typeof(EnemySpawnPoints))]
    public class EnemySpawnPointsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            // Рисуем стандартный инспектор (чтобы видеть сам массив Points)
            DrawDefaultInspector();

            EnemySpawnPoints spawnPointsScript = (EnemySpawnPoints)target;

            GUILayout.Space(10); // Небольшой отступ для визуального разделения

            // Создаем кнопку в инспекторе
            if (GUILayout.Button("Find and Collect All Spawn Points", GUILayout.Height(30)))
            {
                // Находим все SpawnPoint на сцене (включая неактивные, если нужно)
                SpawnPoint[] foundPoints =
                    Object.FindObjectsByType<SpawnPoint>(FindObjectsInactive.Include, FindObjectsSortMode.None);

                SpawnPoint[] sortedPoints = foundPoints
                    .OrderBy(p => p.transform.GetSiblingIndex())
                    .ToArray();

                // Регистрируем изменения для системы Undo (отмены действий)
                Undo.RecordObject(spawnPointsScript, "Collect Spawn Points");

                // Записываем найденные точки в свойство скрипта
                // Используем рефлексию, так как у вашего свойства приватный сеттер
                var property = typeof(EnemySpawnPoints).GetProperty("Points");

                property.SetValue(spawnPointsScript, sortedPoints);

                // Маркируем сцену как "измененную", чтобы Unity предложила сохранить её
                EditorUtility.SetDirty(spawnPointsScript);

                Debug.Log($"<color=green>Успешно собрано точек спавна: {sortedPoints.Length}</color>",
                    spawnPointsScript);
            }
        }
    }
}