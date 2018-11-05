# XNA/Monogame PixelShader Mask  
A simple 2D mask written for Monogame. This repository contains the [shader itself](../master/PixelShader%20Mask/Content/Mask.fx), plus the full code for an example game to help people like me figure out how to use shaders and get it up and running quickly and easily.  

## Important Files  
[The mask file itself](../master/PixelShader%20Mask/Content/Mask.fx)  

### Examples To Help You Figure Out How Shaders Work  
[The main game script](../master/PixelShader%20Mask/Game1.cs)  

## If You've Done This Kind Of Thing Before  
The script works by first passing information about the mask, then passing information about each texture before it is drawn.  
For example:  
```
GraphicsDevice.Clear(Color.CornflowerBlue);

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
```

## Special Thanks  
Most of the shader code came from this [this answer](https://gamedev.stackexchange.com/questions/38118/best-way-to-mask-2d-sprites-in-xna/38135#38135) on gamedev stackexchange.  


## Final Note  
I learned all this for use in my game: Irbis. You can check it out at [irbisgame.com](https://irbisgame.com), or poke around its [own repository](https://github.com/DariusMiu/Irbis)!  