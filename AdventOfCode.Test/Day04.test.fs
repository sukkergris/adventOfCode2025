module Day04.test

open Xunit
open AdventOfCode.PrintingDepartment

[<Fact>]
let ``Print the stuff`` () =
    let board = "TestData/day04.test.txt"
                |> getInput
                |> toBoard
    Assert.Equal(10,board.Length)

[<Theory>]
[<InlineData(0,0,3,3,2)>]
[<InlineData(1,0,3,3,3)>]
[<InlineData(1,1,3,3,4)>]
[<InlineData(0,2,3,3,2)>]
let ``Get adjacent coords to point`` (x: int, y:int, abscissa:int, ordinate:int, expected: int) =
    let p = {x=x;y=y; c='@'}
    let adjacentCoords = getAdjacentCoords p abscissa ordinate
    // printfn "%A" adjacentCoords
    Assert.Equal(expected, adjacentCoords.Length)
