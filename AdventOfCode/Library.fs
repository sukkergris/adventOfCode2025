namespace AdventOfCode

module DeScrambler =
    let start = 50;
    let dialSize = 100;
    let fileName = "TestData/x.txt"

    let loadLines2Array = System.IO.File.ReadAllLines(fileName)

