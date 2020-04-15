using System;
using System.Collections;
using System.IO;
class Solution
{
    // ===== Constant Definitions =====
    const int NUMBER_OF_SHAPES = 2;

    // ===== Main Method =====
    static void Main(String[] args)
    {
        // Get all the info about the shapes.
        int[][] shapes = IngestData();

        // Find all the points of intersection between the 
        // lines of the shapes.
        int[][] intersections = GetIntersectPoints(shapes);

    }

    // ===== Find points of overlap =====
    static int[][] GetIntersectPoints(int[][] shapes)
    {
        // ArrayList for all the points of overlap.
        ArrayList intersections = new ArrayList();

        // ArrayList for all the lines of overlap.
        ArrayList linesOfIntersection = new ArrayList();

        // Perform the opperation both ways round.
        for (int i = 0; i < 2; i++)
        {
            // Set up the two shapes.
            int[] shape1 = shapes[i%2];
            int[] shape2 = shapes[(i+1)%2];

            // Set up the lines.
            // 1st shape's vertical lines.
            // Format x, y1, y2
            int[][] shape1Lines = new int[][]{
                new int[]{shape1[0], shape1[1], shape1[1] + shape1[3]},
                new int[]{shape1[0] + shape1[2], shape1[1], shape1[1] + shape1[3]}
            };
            // 2nd shape's horizontal lines.
            // Format y, x1, x2
            int[][] shape2Lines = new int[][]{
                new int[]{shape2[1], shape2[0], shape2[0] + shape2[2]},
                new int[]{shape2[1] + shape1[3], shape2[0], shape2[0] + shape2[2]}
            };

            // Itterate over the shape's vertical lines.
            for (int j = 0; j < 2; j++)
            {
                // Itterate over the second shape's horizontal lines.
                for (int k = 0; k < 2; k++)
                {
                    // Add point to line if the lines intersect.
                    if(CheckIntersect(shape1Lines[j], shape2Lines[k])){
                        intersections.Add(
                            FindIntersect(shape1Lines[j], shape2Lines[k]));
                        
                        // Add the lines that intersected to the List in case 
                        // there are only 2 intersections.
                        if(i == 1){
                            linesOfIntersection.Add(
                                new int[][]{shape1Lines[j], shape2Lines[k]});
                        }else{
                            linesOfIntersection.Add(
                                new int[][]{shape2Lines[k], shape1Lines[j]});
                        }
                    }
                }
            }
        }

        if(intersections.Count == 2){
            // If there are only 2 intersection points work out where 
            // the other 2 bounding points lie.

            int[] point1 = (int[])intersections[0];
            int[] point2 = (int[])intersections[1];

            if(point1[0] == point2[0]){
                
            }else if(point1[1] == point2[1]){

            }else{
                intersections.Add(new int[]{point1[0], point2[1]});
                intersections.Add(new int[]{point2[0], point1[1]});
            }
        }

        // Return the arraylist of intersections.
        return (int[][])intersections.ToArray();
    }

    // Check for an intersection of 2 lines.
    static bool CheckIntersect(int[] line1, int[] line2){
        if(line1[0] > line2[1] && line1[0] < line2[2] 
        && line2[0] > line1[1] && line2[0] < line1[2]){
            return true;
        }
        return false;
    }

    // Find the points at which 2 lines intersect.
    static int[] FindIntersect(int[] line1, int[] line2){
        return new int[]{line1[0], line2[0]};
    }

    // ===== Data Ingestion =====
    // Gets all the data about the shapes into an array.
    static int[][] IngestData()
    {
        int[][] shapesInformation = new int[NUMBER_OF_SHAPES][];

        // Pull the data into the array for each shape.
        for (int i = 0; i < NUMBER_OF_SHAPES; i++)
        {
            shapesInformation[i] = GetShapeInformation();
        }

        return shapesInformation;
    }

    // Gets all the data about a single shape into an array.
    static int[] GetShapeInformation()
    {
        // Read in the data as strings then split it down.
        String rawInput = Console.ReadLine();
        String[] splitStrings = rawInput.Split(' ');

        //Work out the number of split strings.
        int numberOfDataPoints = splitStrings.Length;

        // Create an int array the same length as the split strings.
        int[] shapeInformation = new int[numberOfDataPoints];

        // Convert the strings to integer values.
        for (int i = 0; i < numberOfDataPoints; i++)
        {
            shapeInformation[i] = Int32.Parse(splitStrings[i]);
        }

        return shapeInformation;
    }
}