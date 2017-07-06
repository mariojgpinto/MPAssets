using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class DynamicGrid : MonoBehaviour {
	public int col,row;
	public bool square = true;

	private void OnRectTransformDimensionsChange() {
		AdjustCellSize();
	}

	public void AdjustCellSize() {
		RectTransform parent = gameObject.GetComponent<RectTransform>();
		GridLayoutGroup grid = gameObject.GetComponent<GridLayoutGroup>();
		
		float totalSpaceH = parent.rect.width - grid.spacing.x * (col - 1);
		float totalSpaceV = parent.rect.height - grid.spacing.y * (row - 1);

		float cellSizeH = totalSpaceH / col;
		float cellSizeV = totalSpaceV / row;

		if (square) {
			float cellSize = Mathf.Min(cellSizeH, cellSizeV);
			grid.cellSize = new Vector2(cellSize, cellSize);
		}
		else {
			grid.cellSize = new Vector2(cellSizeH, cellSizeV);
		}
	}
}
