matrix ViewProjection;
Texture2D diffuseTexture;

SamplerState Sampler
{
};

struct VertexStruct
{
	float4 Position : SV_Position;
	float4 Color	: Color;
	float2 TexCoord : TEXCOORD;
};

VertexStruct VS(VertexStruct input)
{
	VertexStruct output = input;
	output.Position = mul(output.Position, ViewProjection);
	return output;
}

float4 PS_Texture(VertexStruct input) : SV_Target
{
	return diffuseTexture.Sample(Sampler, input.TexCoord);
}

float4 PS_VertexColor(VertexStruct input) : SV_Target
{
	return input.Color;
}

technique10 MainTechnique
{
	pass DrawTexturePass
	{
		SetVertexShader(CompileShader(vs_5_0, VS()));
		SetPixelShader(CompileShader(ps_5_0, PS_Texture()));
	}
	pass DrawVertexColorPass
	{
		SetVertexShader(CompileShader(vs_5_0, VS()));
		SetPixelShader(CompileShader(ps_5_0, PS_VertexColor()));
	}
}