using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace FNaF_XNA.Systems
{
    /// <summary>
    /// Manages animatronic AI, movement, and behavior.
    /// </summary>
    public class AnimatronicSystem
    {
        private List<Animatronic> animatronics;
        private bool gameOver;

        public AnimatronicSystem()
        {
            animatronics = new List<Animatronic>();
            gameOver = false;
        }

        public void Initialize()
        {
            // Create animatronics
            animatronics.Add(new Animatronic("Freddy", new Vector2(100, 100)));
            animatronics.Add(new Animatronic("Bonnie", new Vector2(300, 100)));
            animatronics.Add(new Animatronic("Chica", new Vector2(500, 100)));
            animatronics.Add(new Animatronic("Foxy", new Vector2(700, 100)));
        }

        public void LoadContent(ContentManager content)
        {
            foreach (var animatronic in animatronics)
            {
                animatronic.LoadContent(content);
            }
        }

        public void Update(GameTime gameTime, float nightProgress, float nightDuration)
        {
            foreach (var animatronic in animatronics)
            {
                animatronic.Update(gameTime, nightProgress, nightDuration);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var animatronic in animatronics)
            {
                animatronic.Draw(spriteBatch);
            }
        }

        public bool IsGameOver()
        {
            return gameOver;
        }

        public void Reset()
        {
            gameOver = false;
            foreach (var animatronic in animatronics)
            {
                animatronic.Reset();
            }
        }
    }

    /// <summary>
    /// Represents an individual animatronic character.
    /// </summary>
    public class Animatronic
    {
        private string name;
        private Vector2 position;
        private Texture2D texture;
        private float aggressiveness;
        private bool active;

        public Animatronic(string name, Vector2 startPosition)
        {
            this.name = name;
            this.position = startPosition;
            this.aggressiveness = 0f;
            this.active = false;
        }

        public void LoadContent(ContentManager content)
        {
            try
            {
                // Load animatronic textures
                // texture = content.Load<Texture2D>($"Animatronics/{name}");
            }
            catch
            {
                // Handle missing assets gracefully
            }
        }

        public void Update(GameTime gameTime, float nightProgress, float nightDuration)
        {
            // Increase aggressiveness as night progresses
            aggressiveness = nightProgress / nightDuration;

            // Activate animatronics based on aggressiveness
            if (aggressiveness > 0.3f && !active)
            {
                active = true;
            }

            // Update position based on AI behavior
            if (active)
            {
                // Simple AI movement
                position.X += (float)Math.Sin(nightProgress) * 2f;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
        }

        public void Reset()
        {
            aggressiveness = 0f;
            active = false;
        }

        public string Name => name;
        public bool IsActive => active;
        public float Aggressiveness => aggressiveness;
    }
}
