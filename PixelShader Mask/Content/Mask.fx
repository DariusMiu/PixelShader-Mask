#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

// huge props to this answer on gamedev.stackexchange.com
// https://gamedev.stackexchange.com/a/38135s

sampler Texture : register(s0);
sampler MaskTexture : register(s1) {
	addressU = Clamp;
	addressV = Clamp;
};

// All of these variables are pixel values
// Feel free to replace with float2 variables
float MaskLocationX;
float MaskLocationY;
float MaskWidth;
float MaskHeight;
float MaskScale;
float TextureLocationX; // This is where your texture is to be drawn
float TextureLocationY; // pixelLocation is different, it is the current pixel
float TextureWidth;
float TextureHeight;
float TextureScale;

float4 main(float2 pixelLocation : TEXCOORD0) : COLOR0
{
	// We need to calculate where in terms of percentage to sample from the MaskTexture
	float maskPixelX = pixelLocation.x * TextureWidth * TextureScale + TextureLocationX;
	float maskPixelY = pixelLocation.y * TextureHeight * TextureScale + TextureLocationY;
	float2 maskCoord = float2((maskPixelX - MaskLocationX) / MaskWidth / MaskScale, (maskPixelY - MaskLocationY) / MaskHeight / MaskScale);
	float4 bitMask = tex2D(MaskTexture, maskCoord);

	float4 color = tex2D(Texture, pixelLocation);

	// It is a good idea to avoid conditional statements in a pixel shader if you can use math instead.
	return color * (bitMask.a);
	// Alternate calculation to invert the mask, you could make this a parameter too if you wanted
	//return tex * (1.0 - bitMask.a);
}

technique BasicColorDrawing
{
	pass Pass0
	{ PixelShader = compile PS_SHADERMODEL main(); }
};