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
float3 HighlightColor;

const float FadeoffDistance = 0.1;

struct VertexShaderInput
{
	float4 Position : POSITION0;
	int Index : TEXCOORD0;
};

struct VertexShaderColorOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
};

float4 ComputePointPosition(VertexShaderInput input) {
	float4 position = input.Position;

	position = mul(position, WorldViewProjection);

	/*if (output.Position.x != input.Position.x) {
		return output;
	}*/

	float offsetWidth = (float)PointPixels / ViewportWidth;
	float offsetHeight = (float)PointPixels / ViewportHeight;

	if (input.Index >= 2)
		offsetHeight = -offsetHeight;

	if (input.Index == 0 || input.Index == 3)
		offsetWidth = -offsetWidth;

	offsetWidth *= position.w;
	offsetHeight *= position.w;

	position = float4(position.x + offsetWidth, position.y + offsetHeight, position.z, position.w);

	return position;
}

VertexShaderColorOutput MainPointDrawingVS(in VertexShaderInput input)
{
	VertexShaderColorOutput output = (VertexShaderColorOutput)0;

	output.Position = ComputePointPosition(input);

	float fadeoff = FadeoffDistance / output.Position.z;
	output.Color = float4(1 - fadeoff, 1 - fadeoff, 1, 1);

	return output;
}

float4 MainPointDrawingPS(VertexShaderColorOutput input) : COLOR
{
	return input.Color;
}

technique PointDrawing
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL MainPointDrawingVS();
		PixelShader = compile PS_SHADERMODEL MainPointDrawingPS();
	}
};

struct PointHighlightVertexShaderOutput
{
	float4 Position : SV_POSITION;
};

PointHighlightVertexShaderOutput MainPointHighlightVS(in VertexShaderInput input)
{
	PointHighlightVertexShaderOutput output = (PointHighlightVertexShaderOutput)0;

	//move position infront of their usual position to make them override other points that would have the same position
	output.Position = ComputePointPosition(input) + float4(0, 0, -0.001, 0);

	return output;
}

float4 MainPointHighlightPS(PointHighlightVertexShaderOutput input) : COLOR
{
		return float4(HighlightColor, 1);
}

technique PointHighlight
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL MainPointHighlightVS();
		PixelShader = compile PS_SHADERMODEL MainPointHighlightPS();
	}
};

struct VertexShaderColorInput
{
	float4 Position : POSITION0;
	float4 Color : COLOR0;
	int Index : TEXCOORD0;
};

VertexShaderColorOutput MainPointColorDrawingVS(in VertexShaderColorInput input)
{
	VertexShaderColorOutput output = (VertexShaderColorOutput)0;

	VertexShaderInput positionInput = (VertexShaderInput)0;
	positionInput.Position = input.Position;
	positionInput.Index = input.Index;

	output.Position = ComputePointPosition(positionInput);
	output.Color = input.Color;

	return output;
}

float4 MainPointColorDrawingPS(VertexShaderColorOutput input) : COLOR
{
	return input.Color;
}

technique PointColorDrawing
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL MainPointColorDrawingVS();
		PixelShader = compile PS_SHADERMODEL MainPointColorDrawingPS();
	}
};