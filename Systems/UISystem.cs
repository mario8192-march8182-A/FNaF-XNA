using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace FNaF_XNA.Systems
{
    /// <summary>
    /// Manages user interface elements like buttons, menus, and HUD elements.
    /// </summary>
    public class UISystem
    {
        private SpriteBatch spriteBatch;
        private SpriteFont uiFont;
        private Dictionary<string, UIElement> uiElements;

        public UISystem()
        {
            uiElements = new Dictionary<string, UIElement>();
        }

        public void Initialize()
        {
            // Initialize UI system
        }

        public void LoadContent(ContentManager content)
        {
            try
            {
                // Load UI font
                uiFont = content.Load<SpriteFont>("Fonts/UIFont");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UI Font Load Error: {ex.Message}");
            }
        }

        public void SetSpriteBatch(SpriteBatch batch)
        {
            spriteBatch = batch;
        }

        public void AddButton(string name, Rectangle bounds, string text, Color textColor)
        {
            var button = new UIButton(name, bounds, text, textColor);
            uiElements.Add(name, button);
        }

        public void AddLabel(string name, Vector2 position, string text, Color textColor)
        {
            var label = new UILabel(name, position, text, textColor);
            uiElements.Add(name, label);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var element in uiElements.Values)
            {
                element.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (var element in uiElements.Values)
            {
                element.Draw(sb, uiFont);
            }
        }

        public UIElement GetElement(string name)
        {
            return uiElements.ContainsKey(name) ? uiElements[name] : null;
        }

        public SpriteFont UIFont => uiFont;
    }

    /// <summary>
    /// Base class for UI elements.
    /// </summary>
    public abstract class UIElement
    {
        protected string name;
        protected bool visible;

        public UIElement(string name)
        {
            this.name = name;
            this.visible = true;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch, SpriteFont font);

        public string Name => name;
        public bool Visible => visible;
    }

    /// <summary>
    /// UI Button element.
    /// </summary>
    public class UIButton : UIElement
    {
        private Rectangle bounds;
        private string text;
        private Color textColor;
        private bool isPressed;

        public UIButton(string name, Rectangle bounds, string text, Color textColor)
            : base(name)
        {
            this.bounds = bounds;
            this.text = text;
            this.textColor = textColor;
            this.isPressed = false;
        }

        public override void Update(GameTime gameTime)
        {
            // Update button state
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (!visible)
                return;

            // Draw button background
            spriteBatch.Draw(new Texture2D(spriteBatch.GraphicsDevice, 1, 1), 
                bounds, Color.Gray);

            // Draw button text
            var textSize = font.MeasureString(text);
            var textPosition = new Vector2(
                bounds.X + (bounds.Width - textSize.X) / 2,
                bounds.Y + (bounds.Height - textSize.Y) / 2
            );
            spriteBatch.DrawString(font, text, textPosition, textColor);
        }

        public Rectangle Bounds => bounds;
        public bool IsPressed => isPressed;
    }

    /// <summary>
    /// UI Label element.
    /// </summary>
    public class UILabel : UIElement
    {
        private Vector2 position;
        private string text;
        private Color textColor;

        public UILabel(string name, Vector2 position, string text, Color textColor)
            : base(name)
        {
            this.position = position;
            this.text = text;
            this.textColor = textColor;
        }

        public override void Update(GameTime gameTime)
        {
            // Labels don't need update
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (!visible)
                return;

            spriteBatch.DrawString(font, text, position, textColor);
        }

        public Vector2 Position => position;
        public string Text => text;
    }
}
