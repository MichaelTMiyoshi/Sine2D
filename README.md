# Sine2D
Line drawing in Unity.  Including sine waves.

Below are the notes that I included as comments in my code.

Line Drawing

Michael T. Miyoshi

(school project)

from: 
https://www.codinblack.com/how-to-draw-lines-circles-or-anything-else-using-linerenderer/

Learning to draw lines so can draw circles so can draw waves.

## Drawing Waves

The lineRenderer only draws one set of lines at a time.  So you need to 
make sure you have all your lines that you want to draw.  Or at least it 
seems to be doing that.  I would need to do more investigation to see if 
that is indeed the case.

The static figures can just be drawn in Start().  The ones that need to 
have time (the moving and standing waves) need to be updated.  They are 
started in Start() but must be called in Update().  Not sure if they 
really need to be called in Start() or not.  The dynamic functions (those 
that need to be called in Update()) do not need to be called in Start().

## Unity Public Variables

Added some public variables that can be changed when running the code 
in Unity.  They adjust the amplitude, waveLength, waveSpeed, and where 
the wave starts on the screen (x and y).  Just click on the LineRenderer 
object in the heirarchy and you can adjust any of these variables to see 
the visual effect.

Put in a public variable to choose between travelling and standing wave.
Again.  Just click on the LineRenderer object in the heirarchy and there
it is.

There are other public variables that could be added.  Color and lineWidth
would be the obvious choices.

## Note:

There are certainly some constants that are not explained in the tutorial.  
These were probably done with a little trial and error to get the desired 
results to show properly on the screen.  Or maybe it was done knowing the 
math of the screen and the plotting of the points and all that.  They can 
certainly be adjusted, but I am not sure they would give as pleasing 
results as the writer of the tutorial and the code achieves.
