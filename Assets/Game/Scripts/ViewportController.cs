using UnityEngine;

[ExecuteInEditMode]
public class ViewportController : MonoBehaviour {
	private new Camera camera;
	private Vector2 size;

	private void Start() {
		camera = GetComponent<Camera>();
		if (camera == null) {
			throw new MissingComponentException("camera");
		}

		previousAspect = camera.aspect;
	}

	public float targetAspect = 16.0f / 9.0f;
	float previousAspect;

	private void Update() {
		if (size.x != Screen.width || size.y != Screen.height) {
			size = new Vector2(Screen.width, Screen.height);
			var windowAspect = size.x / size.y;
			if (windowAspect > targetAspect) {
				var margin = targetAspect / windowAspect;
				camera.rect = new Rect((1 - margin) / 2, 0, margin, 1);
			} else {
				var margin = windowAspect / targetAspect;
				camera.rect = new Rect(0, (1 - margin) / 2, 1, margin);
			}
		}
	}
}