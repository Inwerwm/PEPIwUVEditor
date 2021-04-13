//////////////////////
//    ��`

// ViewProjection�ό`�s��
matrix ViewProjection;

// �e�N�X�`���摜
Texture2D diffuseTexture;
Texture2D signTexture;

// �e�N�X�`���̃T���v�����O�ݒ�
// ��Ԃ͋ߖT���_
SamplerState Sampler
{
	Filter = MIN_MAG_MIP_POINT;
};

// �W�����_�\��
struct VertexStruct
{
	float4 Position : SV_Position;
	float4 Color	: Color;
	float2 TexCoord : TEXCOORD;
};

// �e�N�X�`���p���_�\��
struct TexturePlateVertex
{
	float4 Position : SV_Position;
	float4 Color	: Color;
	float2 TexCoord : TEXCOORD;
	row_major float4x4 Offset : Offset;
	float AlphaRatio : Ratio;
	uint InstanceId : SV_InstanceID;
};

// �ʒu�l�p�`�p���_�\��
struct PositionSquareVertex
{
	float4 Position : SV_Position;
	float4 Color	: Color;
	float4 Offset   : Offset;
	uint InstanceId : SV_InstanceID;
};

// ��]���S�p
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
//    ���_�V�F�[�_

// ViewProjection�s��ɂ��ό`�݂̂�K�p����
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

// �e�N�X�`���p
// �C���X�^���X�ŗL�̈ʒu�ϊ��ƐF�ϊ���K�p�������ViewProjection�s����|����
TexturePlateVertex VS_FillTexturePlates(TexturePlateVertex input)
{
	TexturePlateVertex output = input;
	output.Position = mul(mul(input.Position, input.Offset), ViewProjection);
	output.Color.a *= output.AlphaRatio;
	return output;
}

// ���_�ʒu�l�p�`�p
PositionSquareVertex VS_PutPositionSquare(PositionSquareVertex input)
{
	PositionSquareVertex output = input;
	output.Position = float4(input.Position.xyz, 0) + mul(input.Offset, ViewProjection);
	output.Color = input.Color;
	return output;
}

// ��]���S�p
VectorOffset VS_PutRotationCenter(VectorOffset input)
{
	VectorOffset output = input;
	output.Position = mul(input.Position, ViewProjection) + float4(input.Offset, 0);
	return output;
}

//////////////////////
//    �s�N�Z���V�F�[�_

// ���_�F��F�Ƃ��Ďg�p
float4 PS_FromVertexColor(VertexStruct input) : SV_Target
{
	return input.Color;
}

// �e�N�X�`���̐F�ɒ��_�F���|�����F���g�p
float4 PS_FromVertexColorInfluencedTexture(TexturePlateVertex input) : SV_Target
{
	return diffuseTexture.Sample(Sampler, input.TexCoord) * input.Color;
}

// �ʒu�l�p�`�p�s�N�Z���V�F�[�_
float4 PS_FromVertexColorPSV(PositionSquareVertex input) : SV_Target
{
	return input.Color;
}

// ��]���S�p
float4 PS_RotationCenter(VectorOffset input) : SV_Target
{
	float4 texColor = signTexture.Sample(Sampler, input.TexCoord);
	return float4(texColor.rgb + input.Color.rgb, texColor.a * input.Color.a * input.AlphaRatio);
}

//////////////////////
//    �e�N�j�b�N

// VertexStruct�p
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

// TexturePlateVertex�p
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