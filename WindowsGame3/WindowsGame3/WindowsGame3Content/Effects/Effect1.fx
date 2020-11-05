float4x4 World;
float4x4 View;
float4x4 Projection;
Texture colorTexture;
Texture colorTexture2;
float time;


sampler colorTextureSampler = sampler_state
{
	texture = <colorTexture>;
	magfilter = LINEAR;
	minfilter = LINEAR;
	mipfilter = LINEAR;
};
sampler colorTextureSampler2 = sampler_state
{
	texture = <colorTexture2>;
	magfilter = LINEAR;
	minfilter = LINEAR;
	mipfilter = LINEAR;
};
struct VertexShaderInput
{
	float4 Position : POSITION0;
	float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
	float4 Position : POSITION0;
	float2 TexCoord : TEXCOORD0;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
	VertexShaderOutput output;

	float4 worldPosition = mul(input.Position, World);
	float4 viewPosition = mul(worldPosition, View);
	output.Position = mul(viewPosition, Projection);

	output.TexCoord = input.TexCoord;

	return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	//return float4(1,1,1,1);
	float4 color = tex2D(colorTextureSampler, input.TexCoord);
	float4 color2 = tex2D(colorTextureSampler2, input.TexCoord);

	float4 finalColor = lerp(color,color2,time);

	


	return finalColor;
}

technique Technique1
{
	pass Pass1
	{
		// TODO: set renderstates here.

		VertexShader = compile vs_2_0 VertexShaderFunction();
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}
