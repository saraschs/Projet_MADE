#ifndef HASH_INCLUDED
#define HASH_INCLUDED

void hash22_float(float2 In, out float2 Out)
{
    float2 p = float2(
        dot(In, float2(127.1, 311.7)),
        dot(In, float2(269.5, 183.3))
    );

    Out = frac(sin(p) * 43758.5453);
}

#endif