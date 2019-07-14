#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

matrix WorldViewProjection;

int ViewportWidth;
int ViewportHeight;
int PointPixels;

struct VertexShaderInput
{
	float4 Position : POSITION0;
	//float4 Color : COLOR0;
	int Index : TEXCOORD0;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
};

VertexShaderOutput MainVS(in VertexShaderInput input)
{
	VertexShaderOutput output = (VertexShaderOutput)0;

	output.Position = input.Position;

	output.Position = mul(output.Position, WorldViewProjection);

	/*if (output.Position.x != input.Position.x) {
		return output;
	}*/

	float offsetWidth = (float)PointPixels / ViewportWidth;
	float offsetHeight = (float)PointPixels / ViewportHeight;

	if (input.Index >= 2)
		offsetHeight = -offsetHeight;

	if (input.Index == 0 || input.Index == 3)
		offsetWidth = -offsetWidth;

	offsetWidth *= output.Position.w;
	offsetHeight *= output.Position.w;



	output.Position = float4(output.Position.x + offsetWidth, output.Position.y + offsetHeight, output.Position.z, output.Position.w);


	output.Color = float4(offsetWidth / 2.0 + 0.5, offsetHeight / 2.0 + 0.5, 0, 1);

	return output;
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
	return input.Color;//float4(1,0,0,1);
}

technique BasicColorDrawing
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};