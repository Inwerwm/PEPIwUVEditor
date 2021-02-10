//////////////////////
//    ��`

// ViewProjection�ό`�s��
matrix ViewProjection;
// �e�N�X�`���摜
Texture2D diffuseTexture;

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

//////////////////////
//    ���_�V�F�[�_

// ViewProjection�s��ɂ��ό`�݂̂�K�p����
VertexStruct VS_ApplyViewProjectionOnly(VertexStruct input)
{
	VertexStruct output = input;
	output.Position = mul(output.Position, ViewProjection);
	return output;
}

// �e�N�X�`���p
// �C���X�^���X�ŗL�̈ʒu�ϊ��ƐF�ϊ���K�p�������ViewProjection�s����|����
TexturePlateVertex VS_FillTexturePlates(TexturePlateVertex input)
{
	TexturePlateVertex output = input;
	output.Position = mul(mul(output.Position , output.Offset), ViewProjection);
	output.Color.a *= output.AlphaRatio;
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