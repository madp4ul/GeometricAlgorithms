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
bool IsHighlighted;

const float FadeoffDistance = 0.1;

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

	if (IsHighlighted) {
		//No colors needed as highlight color will be used instead
		//Also move position infront of their usual position to make them override other points that would have the same position
		output.Position = output.Position + float4(0, 0, -0.001, 0);
	}
	else {

	}
	float fadeoff = FadeoffDistance / output.Position.z;
	output.Color = float4(1 - fadeoff, 1 - fadeoff, 1, 1);

	return output;
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
	if (IsHighlighted) {
		//Change to highlight color
		return float4(0, 1, 1, 1);
	}
	else {
		return input.Color;
	}
}

technique BasicColorDrawing
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};