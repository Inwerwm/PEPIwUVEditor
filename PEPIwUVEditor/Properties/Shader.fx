//////////////////////
//    定義

// ViewProjection変形行列
matrix ViewProjection;

// テクスチャ画像
Texture2D diffuseTexture;
Texture2D signTexture;

// テクスチャのサンプリング設定
// 補間は近傍頂点
SamplerState Sampler
{
	Filter = MIN_MAG_MIP_POINT;
};

// 標準頂点構造
struct VertexStruct
{
	float4 Position : SV_Position;
	float4 Color	: Color;
	float2 TexCoord : TEXCOORD;
};

// テクスチャ板用頂点構造
struct TexturePlateVertex
{
	float4 Position : SV_Position;
	float4 Color	: Color;
	float2 TexCoord : TEXCOORD;
	row_major float4x4 Offset : Offset;
	float AlphaRatio : Ratio;
	uint InstanceId : SV_InstanceID;
};

// 位置四角形用頂点構造
struct PositionSquareVertex
{
	float4 Position : SV_Position;
	float4 Color	: Color;
	float4 Offset   : Offset;
	uint InstanceId : SV_InstanceID;
};

// 回転中心用
struct VectorOffset
{
	float4 Position : SV_Position;
	float4 Color	: Color;
	float3 Offset   : Offset;
	float2 TexCoord : TEXCOORD;
	float AlphaRatio : Ratio;
	uint InstanceId : SV_InstanceID;
};

//////////////////////
//    頂点シェーダ

// ViewProjection行列による変形のみを適用する
VertexStruct VS_ApplyViewProjectionOnly(VertexStruct input)
{
	VertexStruct output = input;
	output.Position = mul(output.Position, ViewProjection);
	return output;
}

VertexStruct VS_Throw(VertexStruct input)
{
	return input;
}

// テクスチャ板用
// インスタンス固有の位置変換と色変換を適用した後にViewProjection行列を掛ける
TexturePlateVertex VS_FillTexturePlates(TexturePlateVertex input)
{
	TexturePlateVertex output = input;
	output.Position = mul(mul(input.Position, input.Offset), ViewProjection);
	output.Color.a *= output.AlphaRatio;
	return output;
}

// 頂点位置四角形用
PositionSquareVertex VS_PutPositionSquare(PositionSquareVertex input)
{
	PositionSquareVertex output = input;
	output.Position = float4(input.Position.xyz, 0) + mul(input.Offset, ViewProjection);
	output.Color = input.Color;
	return output;
}

// 回転中心用
VectorOffset VS_PutRotationCenter(VectorOffset input)
{
	VectorOffset output = input;
	output.Position = mul(input.Position, ViewProjection) + float4(input.Offset, 0);
	return output;
}

//////////////////////
//    ピクセルシェーダ

// 頂点色を色として使用
float4 PS_FromVertexColor(VertexStruct input) : SV_Target
{
	return input.Color;
}

// テクスチャの色に頂点色を掛けた色を使用
float4 PS_FromVertexColorInfluencedTexture(TexturePlateVertex input) : SV_Target
{
	return diffuseTexture.Sample(Sampler, input.TexCoord) * input.Color;
}

// 位置四角形用ピクセルシェーダ
float4 PS_FromVertexColorPSV(PositionSquareVertex input) : SV_Target
{
	return input.Color;
}

// 回転中心用
float4 PS_RotationCenter(VectorOffset input) : SV_Target
{
	float4 texColor = signTexture.Sample(Sampler, input.TexCoord);
	return float4(texColor.rgb + input.Color.rgb, texColor.a * input.Color.a * input.AlphaRatio);
}

//////////////////////
//    テクニック

// VertexStruct用
technique10 MainTechnique
{
	pass DrawVertexColorPass
	{
		SetVertexShader(CompileShader(vs_5_0, VS_ApplyViewProjectionOnly()));
		SetPixelShader(CompileShader(ps_5_0, PS_FromVertexColor()));
	}
	pass DrawByWorldLocate
	{
		SetVertexShader(CompileShader(vs_5_0, VS_Throw()));
		SetPixelShader(CompileShader(ps_5_0, PS_FromVertexColor()));
	}
}

// TexturePlateVertex用
technique10 TexturePlatesTechnique
{
	pass DrawTexturePlatesPass
	{
		SetVertexShader(CompileShader(vs_5_0, VS_FillTexturePlates()));
		SetPixelShader(CompileShader(ps_5_0, PS_FromVertexColorInfluencedTexture()));
	}
}

technique10 PositionSquaresTechnique
{
	pass DrawPositionSquaresPass
	{
		SetVertexShader(CompileShader(vs_5_0, VS_PutPositionSquare()));
		SetPixelShader(CompileShader(ps_5_0, PS_FromVertexColorPSV()));
	}
}

technique10 VectorOffsetTechnique
{
	pass DrawRotationCenterPass
	{
		SetVertexShader(CompileShader(vs_5_0, VS_PutRotationCenter()));
		SetPixelShader(CompileShader(ps_5_0, PS_RotationCenter()));
	}
}