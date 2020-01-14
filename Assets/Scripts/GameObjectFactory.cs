using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class GameObjectFactory : ScriptableObject
{

	Scene scene;

	protected T CreateGameObjectInstance<T>(T prefab) where T : MonoBehaviour
	{
		if (!scene.isLoaded)
		{
			if (Application.isPlaying)
			{
				scene = SceneManager.CreateScene(name);
			}
			else
			{
				Scene current = EditorSceneManager.GetActiveScene();
				scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive);
				scene.name = name;
				EditorSceneManager.SetActiveScene(current);
			}
		}
		T instance = Instantiate(prefab);
		SceneManager.MoveGameObjectToScene(instance.gameObject, scene);
		return instance;
	}
}
