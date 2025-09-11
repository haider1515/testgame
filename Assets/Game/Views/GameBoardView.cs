using UnityEngine;
using UnityEngine.UI;
using Game.ViewModels;
using Game.Config;
using System.Collections.Generic;
using Zenject;

namespace Game.Views
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public class GameBoardView : MonoBehaviour
    {
        public Vector2 spacing = new Vector2(10, 10);
        public Vector2 padding = new Vector2(20, 20);


        public Transform gridParent;
        private GridLayoutGroup grid;
        private RectTransform rect;

        private IConfig config;


        private int rows;
        private int cols;

        [Inject]
        public void Construct(IConfig config)
        {
            rows = config.Rows;
            cols = config.Columns;
        }

        void Awake()
        {
            if (gridParent == null)
                gridParent = transform;

            grid = GetComponent<GridLayoutGroup>();
            rect = GetComponent<RectTransform>();
            UpdateLayout();
        }

        void OnRectTransformDimensionsChange()
        {
            UpdateLayout();
        }

        public void UpdateLayout()
        {
            if (rows <= 0 || cols <= 0) return;

            float width = rect.rect.width - padding.x - spacing.x * (cols - 1);
            float height = rect.rect.height - padding.y - spacing.y * (rows - 1);

            float cellW = width / cols;
            float cellH = height / rows;

            float size = Mathf.Min(cellW, cellH);

            grid.cellSize = new Vector2(size, size);
            grid.spacing = spacing;
            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            grid.constraintCount = cols;
        }

        public void Populate(IGameBoardViewModel board, CardView prefab, Dictionary<string, Sprite> spriteMap)
        {
            foreach (Transform child in gridParent)
                Destroy(child.gameObject);

            foreach (var cardVM in board.Cards)
            {
                var view = Instantiate(prefab, gridParent);
                spriteMap.TryGetValue(cardVM.SpriteKey, out Sprite sprite);
                view.Bind(cardVM, sprite);
            }

            UpdateLayout();
        }
    }
}
