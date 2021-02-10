//////////////////////
//    定義

// ViewProjection変形行列
matrix ViewProjection;
// テクスチャ画像
Texture2D diffuseTexture;

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

// 位置表示四角形用頂点構造
struct PositionSquareVertex
{
	float4 Position : SV_Position
	float4 Center : Center;
	float Radius : Radius;
	int Direction : Direction;
	float4 Color : Color;
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

// テクスチャ板用
// インスタンス固有の位置変換と色変換を適用した後にViewProjection行列を掛ける
TexturePlateVertex VS_FillTexturePlates(TexturePlateVertex input)
{
	TexturePlateVertex output = input;
	output.Position = mul(mul(output.Position, output.Offset), ViewProjection);
	output.Color.a *= output.AlphaRatio;
	return output;
}

// 位置表示四角形用
PositionSquareVertex VS_PutSquareVertex(PositionSquareVertex input)
{
	PositionSquareVertex output = input;
	output.Center = mul(input.Center, ViewProjection);

	switch (input.Direction)
	{
	case 0:
		output.Position = output.Center + float4(-1 * input.Radius, -1 * input.Radius, 0, 0);
		break;
	case 1:
		output.Position = output.Center + float4(input.Radius, -1 * input.Radius, 0, 0);
		break;
	case 2:
		output.Position = output.Center + float4(-1 * input.Radius, input.Radius, 0, 0);
		break;
	case 3:
		output.Position = output.Center + float4(input.Radius, input.Radius, 0, 0);
		break;
	default:
		break;
	}

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

// 頂点色を色として使用
float4 PS_PositionSquare(PositionSquareVertex input) : SV_Target
{
	return input.Color;
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

// PositionSquareVertex用
technique10 PositionSquareTechnique
{
	pass DrawPositionSquarePass
	{
		SetVertexShader(CompileShader(vs_5_0, VS_PutSquareVertex()));
		SetPixelShader(CompileShader(ps_5_0, PS_PositionSquare()));
	}
}