Shader "Custom/Outline" {
	Properties {
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Range (.002, 0.03)) = .005
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		UsePass "Standard (Outlined)/OUTLINE"
	} 

	FallBack "Diffuse"
}
