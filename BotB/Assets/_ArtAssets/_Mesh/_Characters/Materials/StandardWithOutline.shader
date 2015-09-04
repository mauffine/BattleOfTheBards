Shader "Custom/StandardWithOutline" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0

		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Range (.002, 0.03)) = .005
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		//UsePass "StandardNoCull/FORWARD"
		//UsePass "StandardNoCull/FORWARD_DELTA"
		//UsePass "StandardNoCull/ShadowCaster"
		//UsePass "StandardNoCull/DEFERRED"
		//UsePass "StandardNoCull/META"
		UsePass "Diffuse/FORWARD"
		//UsePass "Standard (Outlined)/DEFERRED"
		UsePass "Toon/Basic Outline/OUTLINE"
		//UsePass "Standard (Outlined)/OUTLINE"
	} 

	FallBack "Diffuse"
}
