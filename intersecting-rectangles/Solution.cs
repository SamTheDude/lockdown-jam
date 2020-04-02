using System;
using System.Collections.Generic;
using System.IO;
class Solution {
    // ===== Constant Definitions =====
    const int NUMBER_OF_SHAPES = 2;

    // ===== Main Method =====
    static void Main(String[] args) {
        // Get all the info about the shapes.
        int[][] shapes = IngestData();

        // Output the result.
        Console.WriteLine(ShapesOverlap(shapes).ToString().ToLower());
    }

    // ===== Intersection Testing =====
    // Tests if any of the shapes it get's passed overlap.
    static bool ShapesOverlap(int[][] shapes){
        // Boolean for if overlap is detected.
        bool overlapDetected = false;

        // Test each pair of shapes.
        for (int i = 0; i < shapes.Length - 1; i++)
        {
            if(TwoShapesOverlap(shapes[i], shapes[(i+1)]))
            {
                overlapDetected = true;
            }
        }

        return overlapDetected;
    }

    // Detects if a target and a filter shape overlap one another.
    static bool TwoShapesOverlap(int[] filter, int[] target){
        // Represent the filter and target as a set of 2 points 
        // on opposite corners.
        int[] filterBL = new int[]{filter[0], filter[1]};
        int[] filterTR = new int[]{
            filter[0] + filter[2], 
            filter[1] + filter[3]
            };

        int[] targetBL = new int[]{target[0], target[1]};
        int[] targetTR = new int[]{
            target[0] + target[2], 
            target[1] + target[3]
            };

        // Retrun false if the bottom left corner of the target is above or to
        // the right of the filter's top right corner.
        if(targetBL[0] > filterTR[0] || targetBL[1] > filterTR[1]){
            return false;
        }

        // Return false if the top right corner of the target is below or to
        // the left of the filter's bottom left corner.
        if(targetTR[0] < filterBL[0] || targetTR[1] < filterBL[1]){
            return false;
        }

        // If neither of the above conditions are met then the shapes must
        // overlap.
        return true;
    }

    // ===== Data Ingestion =====
    // Gets all the data about the shapes into an array.
    static int[][] IngestData(){
        int[][] shapesInformation = new int[NUMBER_OF_SHAPES][];

        // Pull the data into the array for each shape.
        for (int i = 0; i < NUMBER_OF_SHAPES; i++)
        {
            shapesInformation[i] = GetShapeInformation();
        }

        return shapesInformation;
    }

    // Gets all the data about a single shape into an array.
    static int[] GetShapeInformation(){
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