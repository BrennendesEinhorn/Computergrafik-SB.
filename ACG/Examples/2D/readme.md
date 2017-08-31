1. [HelloWorld.glsl](HelloWorld.glsl)
	1. Colors (line i)
		1. Invert the colors
		1. Make them brighter, darker
	1. Functions (line ii and iii) [detailed description](https://thebookofshaders.com/07/)
		1. Compare the result of lines ii and iii
		1. How can you rotate the result by 90°?
		1. Combine results from multiple step functions like in line iv
		1. Use the step function to draw a quad
		1. USe smoothstep to draw soft edged quads
		1. Can you do something that resembles a Piet Mondrian painting? ![Mondrian painting](mondrian.jpg "Mondrian painting")
1. [ShapingFunctions.glsl](ShapingFunctions.glsl)
	1. Uncomment the different functions
	1. How to draw the green line? Create such a function plotter!
	1. How can we map coordinates to a bigger range than [0..1]²?
	1. How are verticel/horizontal lines that keep their thickness realized?
	1. Choose a function with very steep parts. What happens?
	1. How can we control the thickness of general functions?
	1. Create your own fucntion using sin, smoothstep, mod, ...
1. [CircleDistanceField.glsl](CircleDistanceField.glsl)
	1. We look at code together
	1. Uncomment from top to bottom
	1. Create your own moving distance fields
1. [Polar.glsl](Polar.glsl)
	1. We look at code together
	1. Uncomment from top to bottom
	1. Make your own flowers, snowflakes, gears
1. [Pattern.glsl](Pattern.glsl)
	1. Do not look at the code
	1. Start the example without animation
	1. Code this brick pattern yourself
	1. Start the animation
	1. Code this animation yourself
	1. Look at the solution in the code
1. Combine [Pattern.glsl](Pattern.glsl) with [Polar.glsl](Polar.glsl)
	1. Code a hexagrid
	1. Code your own pattern
1. [PatternCircle.glsl](PatternCircle.glsl)
	1. Do not look at the code
	1. Start the animation
	1. Code this animation yourself
1. [PatternTruchet.glsl](PatternTruchet.glsl)
	1. Look at code together
	1. Other tiling systems 
		1. (Girih tiles)[https://en.wikipedia.org/wiki/Girih_tiles]
		1. (Penrose tiling)[https://en.wikipedia.org/wiki/Penrose_tiling]
		1. (Aperiodic tiling)[https://en.wikipedia.org/wiki/Aperiodic_tiling]
1. Creating randomness [ShapingFunctions.glsl](ShapingFunctions.glsl)
	1. Look at function y = fract(sin(x) * 1.0);
	1. Add zeros to factor
	1. One way of creating pseudo random numbers
1. [Random.glsl](Random.glsl)
	1. Play around with the magic numbers
	1. Try out random(vec2)
	1. Hook it to mouse movement
	1. Hook it to iGlobalTime
	1. Apply it to Truchet patterns
	1. Create your own effect [some examples](http://thebookofshaders.com/10/)
1. [Noise.glsl](Noise.glsl) [Noise2D.glsl](Noise2D.glsl)
	1. Analyse differences of rand() and noise() in [ShapingFunctions.glsl](ShapingFunctions.glsl)
	1. Look at code together
	1. When do rand() and noise() look the same?
	1. Try out different interpolations
	1. Look at quad manipulation code
1. [NoiseExamples.glsl](NoiseExamples.glsl)
	1. Uncomment and analyse
1. [ShapingFunctions.glsl](ShapingFunctions.glsl)
	1. Look at amplitude * sin(x * frequency + phase)
	1. Hook phase to iMouse.x * 0.1
	1. Add second sin() with much higher frequency and small amplitude
	1. Do the same with noise()
1. [fBm.glsl](fBm.glsl)
	1. Look at code together
	1. Increase the number of octaves
	1. Change the other parameters
1. [fBm2D.glsl](fBm2D.glsl)
	1. Look at code together
	1. Uncomment different types
