using System;
using System.IO;
using System.Drawing.Imaging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace MaskTest1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Effect maskEffect;
        Texture2D mask;
        Texture2D light;
        Vector2 lightPosition;
        Vector2 maskPosition;
        Vector2 lightOffset;
        float lightScale = 2f;
        float maskScale = 0.5f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        { base.Initialize(); }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            maskEffect = Content.Load<Effect>("Mask");
            mask = Content.Load<Texture2D>("masktex");
            light = Content.Load<Texture2D>("light");
            maskPosition = new Vector2(200, 100);
            lightOffset = new Vector2(light.Width / 2, light.Height / 2);

            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            { Exit(); }
            lightPosition = Mouse.GetState().Position.ToVector2();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // this is just here to show you where the mask is
            spriteBatch.Begin(blendState: BlendState.AlphaBlend);
            spriteBatch.Draw(mask, maskPosition, null, new Color(Color.Black, 0.3f), 0, Vector2.Zero, maskScale, SpriteEffects.None, 0f);
            spriteBatch.End();


            // remember to cast to a float if you are passing an int!
            maskEffect.Parameters["MaskLocationX"].SetValue(maskPosition.X);
            maskEffect.Parameters["MaskLocationY"].SetValue(maskPosition.Y);
            maskEffect.Parameters["MaskWidth"].SetValue((float)(mask.Width));
            maskEffect.Parameters["MaskHeight"].SetValue((float)(mask.Height));
            maskEffect.Parameters["MaskScale"].SetValue(maskScale);
            maskEffect.Parameters["MaskTexture"].SetValue(mask);

            spriteBatch.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointWrap, effect: maskEffect);
            
            // pass this information for EACH texture you are drawing
            maskEffect.Parameters["TextureLocationX"].SetValue(lightPosition.X - lightOffset.X * lightScale);
            maskEffect.Parameters["TextureLocationY"].SetValue(lightPosition.Y - lightOffset.Y * lightScale);
            maskEffect.Parameters["TextureWidth"].SetValue((float)(light.Width));
            maskEffect.Parameters["TextureHeight"].SetValue((float)(light.Height));
            maskEffect.Parameters["TextureScale"].SetValue(lightScale);
            spriteBatch.Draw(light, lightPosition, null, Color.White, 0f, lightOffset, lightScale, SpriteEffects.None, 0f);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
