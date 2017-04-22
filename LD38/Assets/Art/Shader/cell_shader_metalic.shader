// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Dave/CellShader - Metalic" {
	Properties{
		_Color("Prim Color", Color) = (1.0,1.0,1.0,1.0)
		_SecColor("Sec Color", Color) = (1.0,1.0,1.0,1.0)
		_ColorCut("Color Cut", Range(-1.0, 1.0)) = 0.5
		_MainTex("Diffuse Texture", 2D) = "white" {}
		_BumpMap("Normal Texture", 2D) = "bump" {}
		_BumpDepth("Bump Depth", Range(0.0, 10.0)) = 5
		_SpecColor("Specular Color", Color) = (1.0,1.0,1.0,1.0)
		_Shininess("Shininess", Float) = 10
		_PrimSpecMod("Prim Spec Mod", Float) = 5.0
		_SecSpecMod("Sec Spec Mod", Range(0.0, 1.0)) = 0.1
		_SuperSpecColor("Super Spec Color", Color) = (1.0,1.0,1.0,1.0)
		_SuperSpecCut("Super Spec Cut", Range(0.0, 1.0)) = 0.9
		_RimColor("RimColor", Color) = (1.0,1.0,1.0,1.0)
		_RimPower("RimPower", Range(0.1,10.0)) = 3.0
	}
		SubShader{
			Pass{
			Tags{
			"LightMode" = "ForwardBase"
		}
			CGPROGRAM
#pragma vertex vert
#pragma fragment frag

			//user defined variables
			uniform float4 _Color;
		uniform float4 _SecColor;
		uniform float _ColorCut;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform sampler2D _BumpMap;
		uniform float4 _BumpMap_ST;
		uniform float4 _SpecColor;
		uniform float _Shininess;
		uniform float _PrimSpecMod;
		uniform float _SecSpecMod;
		uniform float _SuperSpecCut;
		uniform float4 _SuperSpecColor;
		uniform float4 _RimColor;
		uniform float _RimPower;
		uniform float _BumpDepth;

		//unity  defined variables
		uniform float4 _LightColor0;

		struct vertexInput {
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			float4 texcoord : TEXCOORD0;
			float4 tangent : TANGENT;
		};

		struct vertexOutput {
			float4 pos : SV_POSITION;
			float4 tex : TEXCOORD0;
			float4 posWorld : TEXCOORD1;
			float3 normalWorld : TEXCOORD2;
			float3 tangentWorld : TEXCOORD3;
			float3 binormalWorld : TEXCOORD4;
		};

		vertexOutput vert(vertexInput v) {
			vertexOutput o;
			o.posWorld = mul(unity_ObjectToWorld, v.vertex);
			o.normalWorld = normalize(mul(float4(v.normal, 0.0), unity_WorldToObject).xyz);
			o.tangentWorld = normalize(mul(unity_ObjectToWorld, v.tangent).xyz);
			o.binormalWorld = normalize(cross(o.normalWorld, o.tangentWorld) * v.tangent.w);
			o.pos = UnityObjectToClipPos(v.vertex);
			o.tex = v.texcoord;
			return o;
		}

		float4 frag(vertexOutput i) : COLOR{
			float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
			float3 lightDirection;
			float atten;

			if (_WorldSpaceLightPos0.w == 0.0) {
				//DIRECTIONAL LIGHT
				atten = 1.0;
				lightDirection = normalize(_WorldSpaceLightPos0.xyz);
			}
			else {
				float3 fragmentToLightSource = _WorldSpaceLightPos0.xyz - i.posWorld.xyz;
				float distance = length(fragmentToLightSource);
				atten = 1.0 / distance;
				lightDirection = normalize(fragmentToLightSource);
			}
			float4 tex = tex2D(_MainTex, i.tex.xy * _MainTex_ST.xy + _MainTex_ST.zw);
			float4 texN = tex2D(_BumpMap, i.tex.xy * _BumpMap_ST.xy + _BumpMap_ST.zw);

			float3 localCoords = float3(2.0 * texN.ag - float2(1.0, 1.0), 0.0);
			localCoords.z = _BumpDepth * _BumpDepth; // = 1.0 - 0.5 * dot(localCoords, localCoords);

			float3x3 local2WorldTranspose = float3x3(
				i.tangentWorld,
				i.binormalWorld,
				i.normalWorld
				);
			float3 normalDirection = normalize(mul(localCoords, local2WorldTranspose));

			float3 diffuseReflection = atten * dot(normalDirection, lightDirection);
			float3 specularReflection = saturate(diffuseReflection) * pow(saturate(dot(reflect(-lightDirection, normalDirection), viewDirection)), _Shininess);

			float rim = 1 - saturate(dot(viewDirection, normalDirection));
			float3 rimLighting = saturate(dot(normalDirection, lightDirection)) * _LightColor0.xyz * pow(rim, _RimPower);
			rimLighting = round(float3(1.0, 1.0, 1.0) * length(rimLighting) * _PrimSpecMod) * _SecSpecMod;
			rimLighting *= _RimColor;

			float3 lightFinal = UNITY_LIGHTMODEL_AMBIENT.xyz + specularReflection * _SpecColor.xyz;
			// float lFLength = length(lightFinal);
			// lightFinal = round((lightFinal / 3) * _PrimSpecMod) / _PrimSpecMod;
			// lightFinal = (round(float3(1.0, 1.0, 1.0) * length(lightFinal)) / length(lightFinal)) * lightFinal * _PrimSpecMod * _SecSpecMod;
			lightFinal = round(float3(1.0, 1.0, 1.0) * length(lightFinal) * _PrimSpecMod) * _SecSpecMod;
			lightFinal *= _SpecColor;
			lightFinal += rimLighting;
			lightFinal *= 0.5;
			lightFinal += 0.5;

			if (any(specularReflection > _SuperSpecCut)) {
				return float4(_SuperSpecColor.xyz, 1.0);
			}

			return float4(tex.xyz * (diffuseReflection > _ColorCut ? _Color.xyz : _SecColor) * lightFinal * _LightColor0.xyz, 1.0);
		}
			ENDCG
		}

		Pass{
			Tags{
			"LightMode" = "ForwardAdd"
		}
			Blend One One
			CGPROGRAM
#pragma vertex vert
#pragma fragment frag

			//user defined variables
			uniform float4 _Color;
		uniform float4 _SecColor;
		uniform float _ColorCut;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform sampler2D _BumpMap;
		uniform float4 _BumpMap_ST;
		uniform float4 _SpecColor;
		uniform float _Shininess;
		uniform float _PrimSpecMod;
		uniform float _SecSpecMod;
		uniform float _SuperSpecCut;
		uniform float4 _SuperSpecColor;
		uniform float4 _RimColor;
		uniform float _RimPower;
		uniform float _BumpDepth;

		//unity  defined variables
		uniform float4 _LightColor0;

		struct vertexInput {
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			float4 texcoord : TEXCOORD0;
			float4 tangent : TANGENT;
		};

		struct vertexOutput {
			float4 pos : SV_POSITION;
			float4 tex : TEXCOORD0;
			float4 posWorld : TEXCOORD1;
			float3 normalWorld : TEXCOORD2;
			float3 tangentWorld : TEXCOORD3;
			float3 binormalWorld : TEXCOORD4;
		};

		vertexOutput vert(vertexInput v) {
			vertexOutput o;
			o.posWorld = mul(unity_ObjectToWorld, v.vertex);
			o.normalWorld = normalize(mul(float4(v.normal, 0.0), unity_WorldToObject).xyz);
			o.tangentWorld = normalize(mul(unity_ObjectToWorld, v.tangent).xyz);
			o.binormalWorld = normalize(cross(o.normalWorld, o.tangentWorld) * v.tangent.w);
			o.pos = UnityObjectToClipPos(v.vertex);
			o.tex = v.texcoord;
			return o;
		}

		float4 frag(vertexOutput i) : COLOR{
			float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
			float3 lightDirection;
			float atten;

			if (_WorldSpaceLightPos0.w == 0.0) {
				//DIRECTIONAL LIGHT
				atten = 1.0;
				lightDirection = normalize(_WorldSpaceLightPos0.xyz);
			}
			else {
				float3 fragmentToLightSource = _WorldSpaceLightPos0.xyz - i.posWorld.xyz;
				float distance = length(fragmentToLightSource);
				atten = 1.0 / distance;
				lightDirection = normalize(fragmentToLightSource);
			}
			float4 texN = tex2D(_BumpMap, i.tex.xy * _BumpMap_ST.xy + _BumpMap_ST.zw);

			float3 localCoords = float3(2.0 * texN.ag - float2(1.0, 1.0), 0.0);
			localCoords.z = _BumpDepth * _BumpDepth; // = 1.0 - 0.5 * dot(localCoords, localCoords);

			float3x3 local2WorldTranspose = float3x3(
				i.tangentWorld,
				i.binormalWorld,
				i.normalWorld
				);
			float3 normalDirection = normalize(mul(localCoords, local2WorldTranspose));

			float3 diffuseReflection = atten * dot(normalDirection, lightDirection);
			float3 specularReflection = saturate(diffuseReflection) * pow(saturate(dot(reflect(-lightDirection, normalDirection), viewDirection)), _Shininess);

			float rim = 1 - saturate(dot(viewDirection, normalDirection));
			float3 rimLighting = saturate(dot(normalDirection, lightDirection)) * _RimColor.xyz * _LightColor0.xyz * pow(rim, _RimPower);
			rimLighting = round(float3(1.0, 1.0, 1.0) * length(rimLighting) * _PrimSpecMod) * _SecSpecMod;
			rimLighting *= _RimColor;

			float3 lightFinal = specularReflection * _SpecColor.xyz;
			// float lFLength = length(lightFinal);
			// lightFinal = round((lightFinal / 3) * _PrimSpecMod) / _PrimSpecMod;
			// lightFinal = (round(float3(1.0, 1.0, 1.0) * length(lightFinal)) / length(lightFinal)) * lightFinal * _PrimSpecMod * _SecSpecMod;
			lightFinal = round(float3(1.0, 1.0, 1.0) * length(lightFinal) * _PrimSpecMod) * _SecSpecMod;
			lightFinal *= _SpecColor;
			lightFinal *= rimLighting;

			if (any(specularReflection > _SuperSpecCut)) {
				return float4(_SuperSpecColor.xyz, 1.0);
			}

			return float4((diffuseReflection > _ColorCut ? _Color.xyz : _SecColor) * lightFinal * _LightColor0.xyz, 1.0);
		}
			ENDCG
		}
	}
}
