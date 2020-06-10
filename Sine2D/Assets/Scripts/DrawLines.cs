/*
 *  Line Drawing
 *  
 *  Michael T. Miyoshi
 *  
 *  (school project)
 *  
 *  from: 
 *  https://www.codinblack.com/how-to-draw-lines-circles-or-anything-else-using-linerenderer/
 *  
 *  Learning to draw lines so can draw circles so can draw waves.
 *  
 *  Drawing Waves
 *  
 *  The lineRenderer only draws one set of lines at a time.  So you need to 
 *  make sure you have all your lines that you want to draw.  Or at least it
 *  seems to be doing that.  I would need to do more investigation to see if
 *  that is indeed the case.
 *  
 *  The static figures can just be drawn in Start().  The ones that need to 
 *  have time (the moving and standing waves) need to be updated.  They are
 *  started in Start() but must be called in Update().  Not sure if they 
 *  really need to be called in Start() or not.  The dynamic functions (those
 *  that need to be called in Update()) do not need to be called in Start().
 *  
 *  Added some public variables that can be changed when running the code
 *  in Unity.  They adjust the amplitude, waveLength, waveSpeed, and where
 *  the wave starts on the screen (x and y).  Just click on the LineRenderer
 *  object in the heirarchy and you can adjust any of these variables to see
 *  the visual effect.
 *  
 *  Put in a public variable to choose between travelling and standing wave.
 *  Again.  Just click on the LineRenderer object in the heirarchy and there
 *  it is.
 *  
 *  There are other public variables that could be added.  Color and lineWidth
 *  would be the obvious choices.
 *  
 *  Note:
 *  
 *  There are certainly some constants that are not explained in the tutorial.
 *  These were probably done with a little trial and error to get the desired
 *  results to show properly on the screen.  Or maybe it was done knowing the
 *  math of the screen and the plotting of the points and all that.  They can
 *  certainly be adjusted, but I am not sure they would give as pleasing 
 *  results as the writer of the tutorial and the code achieves.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLines : MonoBehaviour
{
    private LineRenderer lineRenderer;
    float lineWidth = 0.2f;
    public float amplitudeAdjustable;
    public float waveLengthAdjustable;
    public float waveSpeedAdjustable;
    public float xStartAdjustable;
    public float yStartAdjustable;
    public bool travelling;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.loop = true;
        lineRenderer.startWidth = lineWidth;
        //lineRenderer.endWidth = 0.05f;
        Vector3[] positions = new Vector3[3] { new Vector3(0, 0, 0), new Vector3(-1, 1, 0), new Vector3(1, 1, 0) };
        DrawTriangle(positions);
        DrawPolygon(25, 3.0f, new Vector3(1, 1, 0), lineWidth, lineWidth);
        DrawSineWave(new Vector3(-10, 0, 0), 3.0f, 3.0f);
        amplitudeAdjustable = 2.0f;
        waveLengthAdjustable = 3.0f;
        waveSpeedAdjustable = 4.0f;
        xStartAdjustable = -10;
        travelling = true;
        //DrawTravellingSineWave(new Vector3(-10, 0, 0), amplitudeAdjustable, waveLengthAdjustable, waveSpeedAdjustable);
        //DrawStandingSineWave(new Vector3(-10, 0, 0), 2.0f, 3.0f, 4.0f);
        //DrawStandingSineWave(new Vector3(xStartAdjustable, 0, 0), amplitudeAdjustable, waveLengthAdjustable, waveSpeedAdjustable);
    }

    void DrawTriangle(Vector3 [] vertexPositions)
    {
        lineRenderer.positionCount = 3;
        lineRenderer.SetPositions(vertexPositions);
    }

    void DrawPolygon(int vertexNumber, float radius, Vector3 centerPos, float startWidth, float endWidth)
    {
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;
        lineRenderer.loop = true;
        float angle = 2 * Mathf.PI / vertexNumber;  // interior angle between vertices
        lineRenderer.positionCount = vertexNumber;

        for (int i = 0; i < vertexNumber; i++)
        {
            Matrix4x4 rotationMatrix = new Matrix4x4(new Vector4(Mathf.Cos(angle * i), Mathf.Sin(angle * i), 0, 0),
                new Vector4(-1 * Mathf.Sin(angle * i), Mathf.Cos(angle * i), 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(0, 0, 0, 1));
            Vector3 initialRelativePosition = new Vector3(0, radius, 0);
            lineRenderer.SetPosition(i, centerPos + rotationMatrix.MultiplyPoint(initialRelativePosition));
        }
    }

    void DrawSineWave(Vector3 startPoint, float amplitude, float wavelength)
    {
        lineRenderer.loop = false;
        float x = 0.0f;
        float y;
        float k = 2 * Mathf.PI / wavelength;
        lineRenderer.positionCount = 200;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            x += i * 0.001f;
            y = amplitude * Mathf.Sin(k * x);
            lineRenderer.SetPosition(i, new Vector3(x, y, 0) + startPoint);
        }
    }
        
    void DrawTravellingSineWave(Vector3 startPoint, float amplitude, float wavelength, float waveSpeed)
    {
        lineRenderer.loop = false;
        float x = 0.0f;
        float y;
        float k = 2 * Mathf.PI / wavelength;
        float w = k * waveSpeed;
        lineRenderer.positionCount = 200;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            x += i * 0.001f;
            y = amplitude * Mathf.Sin(k * x + w * Time.time);
            lineRenderer.SetPosition(i, new Vector3(x, y, 0) + startPoint);
        }
    }

    void DrawStandingSineWave(Vector3 startPoint, float amplitude, float waveLength, float waveSpeed)
    {
        lineRenderer.loop = false;
        float x = 0.0f;
        float y;
        float k = 2 * Mathf.PI / waveLength;
        float w = k * waveSpeed;
        lineRenderer.positionCount = 200;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            x += i * 0.001f;
            y = amplitude * (Mathf.Sin(k * x + w * Time.time) + Mathf.Sin(k * x - w * Time.time));
            lineRenderer.SetPosition(i, new Vector3(x, y, 0) + startPoint);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (travelling)
        {
            //DrawTravellingSineWave(new Vector3(-10, 0, 0), 2.0f, 3.0f, 10.0f);
            DrawTravellingSineWave(new Vector3(xStartAdjustable, yStartAdjustable, 0), amplitudeAdjustable, waveLengthAdjustable, waveSpeedAdjustable);
        }
        else
        {
            //DrawStandingSineWave(new Vector3(-10, 0, 0), 2.0f, 3.0f, 4.0f);
            DrawStandingSineWave(new Vector3(xStartAdjustable, yStartAdjustable, 0), amplitudeAdjustable, waveLengthAdjustable, waveSpeedAdjustable);
        }
    }
}
